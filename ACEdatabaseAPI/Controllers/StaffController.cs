using ACE.Domain.Abstract.IControlledRepo;
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
    public class StaffController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        IDepartmentRepo _deptRepo;
        private readonly IMapper _mapper;

        public StaffController(UserManager<ApplicationUser> userManager, IMapper mapper, ApplicationDbContext context, IDepartmentRepo deptRepo)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _deptRepo = deptRepo;
        }



        [HttpGet]
        [Route("Get/All")]
        public async Task<IActionResult> GetStaffs()
        {
            try
            {
                var users = _userManager.Users.ToList();
                List<StaffDTO> staffDto = new List<StaffDTO>();
                foreach (var user in users)
                {
                    var isStaff = await _userManager.IsInRoleAsync(user, "Staff");
                    if (isStaff)
                    {
                        //await _context.Entry(user).Reference(x => x.Gender).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Religion).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.School).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Departments).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.MaritalStatus).LoadAsync();


                        var staff = new StaffDTO();
                        var newDTO = _mapper.Map(user, staff);
                        staffDto.Add(newDTO);
                    }
                }

                return Ok(staffDto);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [Route("Get/Lecturers/All")]
        public async Task<IActionResult> GetLecturers()
        {
            try
            {
                var users = _userManager.Users.ToList();
                List<StaffDTO> staffDto = new List<StaffDTO>();
                foreach (var user in users)
                {
                    var isStaff = await _userManager.IsInRoleAsync(user, "Lecturer");
                    if (isStaff)
                    {
                        //await _context.Entry(user).Reference(x => x.Gender).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Religion).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.School).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Departments).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.MaritalStatus).LoadAsync();


                        var staff = new StaffDTO();
                        var newDTO = _mapper.Map(user, staff);
                        staffDto.Add(newDTO);
                    }
                }

                return Ok(staffDto);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [Route("Get/Lecturers/{DepartmentID}/All")]
        public async Task<IActionResult> GetLecturersByDepartment(Guid DepartmentID)
        {
            try
            {
                var users = _userManager.Users.Where(x => x.DepartmentID == DepartmentID).ToList();
                List<StaffDTO> staffDto = new List<StaffDTO>();
                foreach (var user in users)
                {
                    var isStaff = await _userManager.IsInRoleAsync(user, "Lecturer");
                    if (isStaff)
                    {
                        //await _context.Entry(user).Reference(x => x.Gender).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Religion).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.School).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Departments).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.MaritalStatus).LoadAsync();


                        var staff = new StaffDTO();
                        var newDTO = _mapper.Map(user, staff);
                        staffDto.Add(newDTO);
                    }
                }

                return Ok(staffDto);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [Route("Get/ID/{ID}")]
        public async Task<IActionResult> GetStaffByID(Guid ID)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(ID.ToString());
                if (user != null)
                {
                    var isStaff = await _userManager.IsInRoleAsync(user, "Staff");
                    if (isStaff)
                    {
                        //await _context.Entry(user).Reference(x => x.Gender).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Religion).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.School).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Departments).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.MaritalStatus).LoadAsync();

                        var staff = new StaffDTO();
                        var newDTO = _mapper.Map(user, staff);
                        return Ok(staff);
                    }
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Staff Not Found"));
                }
                return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Staff Not Found"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("Get/Email")]
        public async Task<IActionResult> GetStaffByEmail(SearchByEmail model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Bad Request"));
                }

                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    var isStaff = await _userManager.IsInRoleAsync(user, "Staff");
                    if (isStaff)
                    {
                        //await _context.Entry(user).Reference(x => x.Gender).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Religion).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.School).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Departments).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.MaritalStatus).LoadAsync();

                        var staff = new StaffDTO();
                        var newDTO = _mapper.Map(user, staff);
                        return Ok(staff);
                    }
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Staff Not Found"));
                }
                return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Staff Not Found"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("Get/StaffID")]
        public async Task<IActionResult> GetStudentsByMatricNumber(SearchByStaffID model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Bad Request"));
                }

                var user = _userManager.Users.Where(x => x.StaffID.ToUpper() == model.StaffID.ToUpper()).FirstOrDefault();
                if (user != null)
                {
                    var isStaff = await _userManager.IsInRoleAsync(user, "Staff");
                    if (isStaff)
                    {
                        //await _context.Entry(user).Reference(x => x.Gender).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Religion).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.School).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Departments).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.MaritalStatus).LoadAsync();

                        var staff = new StaffDTO();
                        var newDTO = _mapper.Map(user, staff);
                        return Ok(staff);
                    }
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Staff Not Found"));
                }
                return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Staff Not Found"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("Get/Biometric")]
        public async Task<IActionResult> GetStaffByBiometric(SearchByBiometrics model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Bad Request"));
                }

                var user = _userManager.Users.Where(x => x.RightThumbFingerBiometrics == model.Biometric).FirstOrDefault();
                if (user == null)
                {
                    user = _userManager.Users.Where(x => x.LeftThumbFingerBiometrics == model.Biometric).FirstOrDefault();

                    if (user != null)
                    {
                        var isStaff = await _userManager.IsInRoleAsync(user, "Staff");
                        if (isStaff)
                        {
                            //await _context.Entry(user).Reference(x => x.Gender).LoadAsync();
                            //await _context.Entry(user).Reference(x => x.Religion).LoadAsync();
                            //await _context.Entry(user).Reference(x => x.School).LoadAsync();
                            //await _context.Entry(user).Reference(x => x.Departments).LoadAsync();
                            //await _context.Entry(user).Reference(x => x.MaritalStatus).LoadAsync();

                            var staff = new StaffDTO();
                            var newDTO = _mapper.Map(user, staff);
                            return Ok(staff);
                        }
                        return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Staff Not Found"));
                    }
                }
                return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Staff Not Found"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [Route("Department/Get/{DepartmentID}")]
        public async Task<IActionResult> GetStaffsInDepartMent(Guid DepartmentID)
        {
            try
            {
                var dept = _deptRepo.FindBy(x => x.Id == DepartmentID).FirstOrDefault();
                if (dept == null)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Department does not Exist"));
                }

                var users = _userManager.Users.Where(x => x.DepartmentID == DepartmentID).ToList();

                List<StaffDTO> staffDto = new List<StaffDTO>();
                foreach (var user in users)
                {
                    var isStaff = await _userManager.IsInRoleAsync(user, "Staff");
                    if (isStaff)
                    {
                        //await _context.Entry(user).Reference(x => x.Gender).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Religion).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.School).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Departments).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.MaritalStatus).LoadAsync();


                        var staff = new StaffDTO();
                        var newDTO = _mapper.Map(user, staff);
                        staffDto.Add(newDTO);
                    }
                }

                return Ok(staffDto);
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
