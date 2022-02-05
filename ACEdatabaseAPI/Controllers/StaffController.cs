using ACE.Domain.Abstract;
using ACE.Domain.Abstract.IControlledRepo;
using ACE.Domain.Entities;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.Data;
using ACEdatabaseAPI.DTOModel;
using ACEdatabaseAPI.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class StaffController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        IDepartmentRepo _deptRepo;
        private readonly IMapper _mapper;
        IvStaffRepo _vStaffRepo;

        public StaffController(UserManager<ApplicationUser> userManager, IMapper mapper, ApplicationDbContext context, IDepartmentRepo deptRepo, IvStaffRepo vStaffRepo)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _deptRepo = deptRepo;
            _vStaffRepo = vStaffRepo;
        }



        [HttpGet]
        [ProducesResponseType(typeof(List<vStaff>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Get/All")]
        public IActionResult GetStaffs()
        {
            try
            {
                var result = _vStaffRepo.FindBy(x => x.StaffID != null && x.Status == "Active").ToList();
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PartialUserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
           StatusCodes.Status500InternalServerError)]
        [Route("Partial/Get/All")]
        public async Task<IActionResult> GetPartiallyRegisteredStaff()
        {
            try
            {
                List<PartialUserDTO> result = new List<PartialUserDTO>();
                var users = _userManager.Users.Where(x => x.Status.ToUpper() == "ACTIVE" && x.StaffID == null).ToList();
                foreach (var item in users)
                {
                    if (await _userManager.IsInRoleAsync(item, "Staff"))
                    {
                        var staff = new PartialUserDTO();
                        staff.Id = Guid.Parse(item.Id);
                        staff.Email = item.Email;
                        staff.FullName = item.FirstName + " " + item.LastName;
                        staff.PhoneNumber = item.PhoneNumber;

                        result.Add(staff);
                    }
                }
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<vStaff>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Get/Staff/{DepartmentID}/All")]
        public IActionResult GetLecturersByDepartment(Guid DepartmentID)
        {
            try
            {
                var result = _vStaffRepo.FindBy(x => x.StaffID != null && x.Status == "Active" && x.DepartmentID == DepartmentID).ToList();
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApplicationUser), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Cafe/Get")]
        public IActionResult CafeGetStaff(SearchByEmail model)
        {
            try
            {
                var result = _userManager.Users.Where(x => x.Email == model.Email).FirstOrDefault();
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(vStaff), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Get/ID/{ID}")]
        public IActionResult GetStaffByID(Guid ID)
        {
            try
            {
                var staff = _vStaffRepo.FindBy(x => x.Id == ID.ToString()).FirstOrDefault();
                return Ok(staff);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(vStaff), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Get/Email")]
        public IActionResult GetStaffByEmail(SearchByEmail model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Bad Request"));
                }

                var staff = _vStaffRepo.FindBy(x => x.Email.ToUpper() == model.Email.ToUpper()).FirstOrDefault();
                return Ok(staff);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(vStaff), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Get/StaffID")]
        public IActionResult GetStudentsByMatricNumber(SearchByStaffID model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Bad Request"));
                }

                var staff = _vStaffRepo.FindBy(x => x.StaffID.ToUpper() == model.StaffID.ToUpper()).FirstOrDefault();
                return Ok(staff);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(vStaff), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Get/Biometric")]
        public IActionResult GetStaffByBiometric(SearchByBiometrics model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Bad Request"));
                }

                var staff = _userManager.Users.Where(x => x.RightThumbFingerBiometrics == model.Biometric || x.LeftThumbFingerBiometrics == model.Biometric)
                    .FirstOrDefault();
                if(staff == null)
                {
                    return NotFound(new { 
                        Message = "User Not Found"
                    });
                }

                var result = _vStaffRepo.FindBy(x => x.Id == staff.Id).FirstOrDefault();
                return Ok(result);
                
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<vStaff>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Department/Get/{DepartmentID}")]
        public IActionResult GetStaffsInDepartMent(Guid DepartmentID)
        {
            try
            {
                var dept = _deptRepo.FindBy(x => x.Id == DepartmentID).FirstOrDefault();
                if (dept == null)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Department does not Exist"));
                }

                var result = _vStaffRepo.FindBy(x => x.DepartmentID == DepartmentID && x.StaffID != null && x.Status == "Active").ToList();

                return Ok(result);
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
