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
        private readonly IMapper _mapper;

        public FlagController(UserManager<ApplicationUser> userManager, IFlagLevelRepo flagLevelRepo, IFlagRepo flagRepo, IMapper mapper)
        {
            _userManager = userManager;
            _flagLevelRepo = flagLevelRepo;
            _flagRepo = flagRepo;
            _mapper = mapper;
        }


        [Route("Students/All")]
        [HttpGet]
        public async Task<IActionResult> GetFlaggedStudents()
        {
            try
            {
               if(User.IsInRole("Security") || User.IsInRole("Staff"))
                {
                    var result = _flagRepo.GetAll().ToList();
                    List<FlagDTO> flags = new List<FlagDTO>();
                    foreach(var user in result)
                    {
                        string studentFirstName = _userManager.FindByIdAsync(user.StudentID.ToString()).Result.FirstName;
                        string studentLastName = _userManager.FindByIdAsync(user.StudentID.ToString()).Result.LastName;

                        string securityFirstName = _userManager.FindByIdAsync(user.SecurityID.ToString()).Result.FirstName;
                        string securityLastName = _userManager.FindByIdAsync(user.SecurityID.ToString()).Result.LastName;
                            
                        var flag = new FlagDTO();
                        flag.StudentName = studentFirstName + " " + studentLastName;
                        flag.FlagLevel = _flagLevelRepo.FindBy(x => x.Id == user.FlagLevelID).Select(x => x.Name).FirstOrDefault();
                        flag.SecurityName = securityFirstName + " " + securityLastName;

                        flags.Add(flag);
                    }
                    return Ok(flags);
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

        [Route("Student/Status/{ID}")]
        [HttpPost]
        public async Task<IActionResult> GetStudentStatus(Guid ID)
        {
            try
            {
                if (User.IsInRole("Security") || User.IsInRole("Staff"))
                {
                    var result = _flagRepo.FindBy(x => x.StudentID == ID).FirstOrDefault();
                    if(result == null)
                    {
                        return Ok(new {
                            Message= "Student Not Flagged"
                        });
                    }
                    string studentFirstName = _userManager.FindByIdAsync(result.StudentID.ToString()).Result.FirstName;
                    string studentLastName = _userManager.FindByIdAsync(result.StudentID.ToString()).Result.LastName;

                    string securityFirstName = _userManager.FindByIdAsync(result.SecurityID.ToString()).Result.FirstName;
                    string securityLastName = _userManager.FindByIdAsync(result.SecurityID.ToString()).Result.LastName;

                    var flagDto = new FlagDTO();
                    flagDto.StudentName = studentFirstName + " " + studentLastName;
                    flagDto.FlagLevel = _flagLevelRepo.FindBy(x => x.Id == result.FlagLevelID).Select(x => x.Name).FirstOrDefault();
                    flagDto.SecurityName = securityFirstName + " " + securityLastName;
                    return Ok(flagDto);
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
        public IActionResult FlagStudent(FlagStudent model)
        {
            try
            {
                var user = User.Identity.Name;
                if (User.IsInRole("Security"))
                {
                    var result = _flagRepo.FindBy(x => x.StudentID == model.StudentID).FirstOrDefault();
                    var flag = new Flag();
                    var flagResult = _mapper.Map(model, flag);
                    if (result == null)
                    {
                        _flagRepo.Add(flagResult);
                        _flagRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                    }
                    _flagRepo.Edit(flagResult);
                    _flagRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
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
