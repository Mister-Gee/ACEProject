using ACE.Domain.Abstract;
using ACE.Domain.Abstract.IControlledRepo;
using ACE.Domain.Entities;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.Data;
using ACEdatabaseAPI.DTOModel;
using ACEdatabaseAPI.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlagController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        IFlagLevelRepo _flagLevelRepo;
        IFlagRepo _flagRepo;
        IvFlagRepo _vFlagRepo;
        private readonly IMapper _mapper;

        public FlagController(UserManager<ApplicationUser> userManager, IFlagLevelRepo flagLevelRepo, IFlagRepo flagRepo, 
            IMapper mapper, IvFlagRepo vFlagRepo)
        {
            _userManager = userManager;
            _vFlagRepo = vFlagRepo;
            _flagLevelRepo = flagLevelRepo;
            _flagRepo = flagRepo;
            _mapper = mapper;
        }


        [Route("Students/All")]
        [ProducesResponseType(typeof(List<vFlag>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult GetFlaggedStudents()
        {
            try
            {
               if(User.IsInRole("Security") || User.IsInRole("MIS"))
                {
                    var result = _vFlagRepo.GetAll().ToList();
                    return Ok(result);
                }
                return StatusCode(400, new ApiError(400, "Unauthorized Access"));
            }
            catch(Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [Route("Students/Get/{ID}")]
        [ProducesResponseType(typeof(vFlag), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult GetFlaggedStudentsByID(Guid ID)
        {
            try
            {
                if (User.IsInRole("Security") || User.IsInRole("MIS"))
                {
                    var result = _vFlagRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    return Ok(result);
                }
                return StatusCode(400, new ApiError(400, "Unauthorized Access"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [Route("Students/Get/StudentID/{StudentID}")]
        [ProducesResponseType(typeof(vFlag), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult GetFlagByStudentID(Guid StudentID)
        {
            try
            {
                if (User.IsInRole("Security") || User.IsInRole("MIS") || User.IsInRole("Student"))
                {
                    var result = _vFlagRepo.FindBy(x => x.StudentID == StudentID).FirstOrDefault();
                    return Ok(result);
                }
                return StatusCode(400, new ApiError(400, "Unauthorized Access"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [Route("Security/Get/Flags/{SecurityID}")]
        [ProducesResponseType(typeof(vFlag), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult SecurityFlags(Guid SecurityID)
        {
            try
            {
                if (User.IsInRole("Security") || User.IsInRole("MIS"))
                {
                    var result = _vFlagRepo.FindBy(x => x.SecurityID == SecurityID).ToList();
                    return Ok(result);
                }
                return StatusCode(400, new ApiError(400, "Unauthorized Access"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [Route("Student/Status/{StudentID}")]
        [HttpGet]
        public async Task<IActionResult> GetStudentStatus(Guid StudentID)
        {
            try
            {
                if (User.IsInRole("Security") || User.IsInRole("Staff"))
                {
                    var result = _vFlagRepo.FindBy(x => x.StudentID == StudentID).FirstOrDefault();
                    if(result == null)
                    {
                        return Ok(new {
                            Message= "Student Not Flagged"
                        });
                    }
                    return Ok(result); 
                }
                return StatusCode(400, new ApiError(400, "Unauthorized Access"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [Route("Student/Flag/")]
        [HttpPost]
        public IActionResult FlagStudent(FlagByID model)
        {
            try
            {
                var user = User.Identity.Name;
                if (User.IsInRole("Security"))
                {
                    var flag = _flagRepo.FindBy(x => x.StudentID == model.StudentID).FirstOrDefault();
                    if(flag == null)
                    {
                        var newFlag = new Flag();
                        newFlag.FlagLevelID = model.FlagLevelID;
                        newFlag.SecurityID = model.SecurityID;
                        newFlag.StudentID = model.StudentID;

                        _flagRepo.Add(newFlag);
                        _flagRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                    }
                    else
                    {
                        flag.FlagLevelID = model.FlagLevelID;
                        flag.SecurityID = model.SecurityID;
                        _flagRepo.Edit(flag);
                        _flagRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                    }

                }
                return StatusCode(400, new ApiError(400, "Unauthorized Access"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [Route("Student/Flag/")]
        [HttpPost]
        public IActionResult FlagStudentBymatricNumber(FlagByMatricNumber model)
        {
            try
            {
                var user = User.Identity.Name;
                if (User.IsInRole("Security"))
                {
                    var student = _userManager.Users.Where(x => x.MatricNumber.ToUpper() == model.MatricNumber.ToUpper()).FirstOrDefault();
                    if(student == null)
                    {
                        return BadRequest(new { 
                            Message = "Student Not Found"
                        });
                    }

                    var flag = _flagRepo.FindBy(x => x.StudentID == Guid.Parse(student.Id)).FirstOrDefault();
                    if (flag == null)
                    {
                        var newFlag = new Flag();
                        newFlag.FlagLevelID = model.FlagLevelID;
                        newFlag.SecurityID = model.SecurityID;
                        newFlag.StudentID = Guid.Parse(student.Id);

                        _flagRepo.Add(newFlag);
                        _flagRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                    }
                    else
                    {
                        flag.FlagLevelID = model.FlagLevelID;
                        flag.SecurityID = model.SecurityID;
                        _flagRepo.Edit(flag);
                        _flagRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                    }

                }
                return StatusCode(400, new ApiError(400, "Unauthorized Access"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [Route("Student/Flag/")]
        [HttpPost]
        public IActionResult FlagStudentBymatricNumber(FlagByBiometric model)
        {
            try
            {
                var user = User.Identity.Name;
                if (User.IsInRole("Security"))
                {
                    var student = _userManager.Users.Where(x => x.LeftThumbFingerBiometrics == model.Biometric || x.RightThumbFingerBiometrics == model.Biometric).FirstOrDefault();
                    if (student == null)
                    {
                        return BadRequest(new
                        {
                            Message = "Student Not Found"
                        });
                    }

                    var flag = _flagRepo.FindBy(x => x.StudentID == Guid.Parse(student.Id)).FirstOrDefault();
                    if (flag == null)
                    {
                        var newFlag = new Flag();
                        newFlag.FlagLevelID = model.FlagLevelID;
                        newFlag.SecurityID = model.SecurityID;
                        newFlag.StudentID = Guid.Parse(student.Id);

                        _flagRepo.Add(newFlag);
                        _flagRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                    }
                    else
                    {
                        flag.FlagLevelID = model.FlagLevelID;
                        flag.SecurityID = model.SecurityID;
                        _flagRepo.Edit(flag);
                        _flagRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                    }

                }
                return StatusCode(400, new ApiError(400, "Unauthorized Access"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }
    }
}
