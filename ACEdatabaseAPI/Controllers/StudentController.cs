using ACE.Domain.Abstract.IControlledRepo;
using ACE.Domain.Entities;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.Data;
using ACEdatabaseAPI.DTOModel;
using ACEdatabaseAPI.Model;
using AutoMapper;
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
    public class StudentController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        IDepartmentRepo _deptRepo;
        private readonly IMapper _mapper;

        public StudentController(UserManager<ApplicationUser> userManager, IMapper mapper, ApplicationDbContext context, IDepartmentRepo deptRepo)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _deptRepo = deptRepo;
        }



        [HttpGet]
        [Route("Get/All")]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var users = _userManager.Users.ToList();
                List<StudentDTO> studentDto = new List<StudentDTO>();
                foreach(var user in users)
                {
                    var isStudent = await _userManager.IsInRoleAsync(user, "Student");
                    if (isStudent)
                    {
                        //await _context.Entry(user).Reference(x => x.Gender).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.StudentCategory).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Level).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Religion).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.School).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Departments).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.MaritalStatus).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Programme).LoadAsync();


                        var student = new StudentDTO();
                        var newDTO = _mapper.Map(user, student);
                        studentDto.Add(newDTO);
                    }
                }
                
                return Ok(studentDto);
            }
            catch(Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [Route("Get/ID/{ID}")]
        public async Task<IActionResult> GetStudentByID(Guid ID)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(ID.ToString());
                if (user != null)
                {
                    var isStudent = await _userManager.IsInRoleAsync(user, "Student");
                    if (isStudent)
                    {
                        //await _context.Entry(user).Reference(x => x.Gender).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.StudentCategory).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Level).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Religion).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.School).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Departments).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.MaritalStatus).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Programme).LoadAsync();

                        var student = new StudentDTO();
                        var newDTO = _mapper.Map(user, student);
                        return Ok(student);
                    }
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Student Not Found"));
                }
                return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Student Not Found"));
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
        public async Task<IActionResult> GetStudentByEmail(SearchByEmail model)
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
                    var isStudent = await _userManager.IsInRoleAsync(user, "Student");
                    if (isStudent)
                    {
                        //await _context.Entry(user).Reference(x => x.Gender).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.StudentCategory).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Level).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Religion).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.School).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Departments).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.MaritalStatus).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Programme).LoadAsync();

                        var student = new StudentDTO();
                        var newDTO = _mapper.Map(user, student);
                        return Ok(student);
                    }
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Student Not Found"));
                }
                return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Student Not Found"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("Get/MatricNumber")]
        public async Task<IActionResult> GetStudentByMatricNumber(SearchByMatricNumber model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Bad Request"));
                }

                var user = _userManager.Users.Where(x => x.MatricNumber.ToUpper() == model.MatricNumber.ToUpper()).FirstOrDefault();
                if(user != null)
                {
                    var isStudent = await _userManager.IsInRoleAsync(user, "Student");
                    if (isStudent)
                    {
                        //await _context.Entry(user).Reference(x => x.Gender).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.StudentCategory).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Level).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Religion).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.School).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Departments).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.MaritalStatus).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Programme).LoadAsync();

                        var student = new StudentDTO();
                        var newDTO = _mapper.Map(user, student);
                        return Ok(student);
                    }
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Student Not Found"));
                }
                return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Student Not Found"));
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
        public async Task<IActionResult> GetStudentByBiometric(SearchByBiometrics model)
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
                        var isStudent = await _userManager.IsInRoleAsync(user, "Student");
                        if (isStudent)
                        {
                            //await _context.Entry(user).Reference(x => x.Gender).LoadAsync();
                            //await _context.Entry(user).Reference(x => x.StudentCategory).LoadAsync();
                            //await _context.Entry(user).Reference(x => x.Level).LoadAsync();
                            //await _context.Entry(user).Reference(x => x.Religion).LoadAsync();
                            //await _context.Entry(user).Reference(x => x.School).LoadAsync();
                            //await _context.Entry(user).Reference(x => x.Departments).LoadAsync();
                            //await _context.Entry(user).Reference(x => x.MaritalStatus).LoadAsync();
                            //await _context.Entry(user).Reference(x => x.Programme).LoadAsync();

                            var student = new StudentDTO();
                            var newDTO = _mapper.Map(user, student);
                            return Ok(student);
                        }
                        return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Student Not Found"));
                    }
                }
                return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Student Not Found"));
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
        public async Task<IActionResult> GetStudentsInDepartMent(Guid DepartmentID)
        {
            try
            {
                var dept = _deptRepo.FindBy(x => x.Id == DepartmentID).FirstOrDefault();
                if(dept == null)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Department does not Exist"));
                }

                var users = _userManager.Users.Where(x => x.DepartmentID == DepartmentID).ToList();

                List<StudentDTO> studentDto = new List<StudentDTO>();
                foreach (var user in users)
                {
                    var isStudent = await _userManager.IsInRoleAsync(user, "Student");
                    if (isStudent)
                    {
                        //await _context.Entry(user).Reference(x => x.Gender).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.StudentCategory).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Level).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Religion).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.School).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Departments).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.MaritalStatus).LoadAsync();
                        //await _context.Entry(user).Reference(x => x.Programme).LoadAsync();


                        var student = new StudentDTO();
                        var newDTO = _mapper.Map(user, student);
                        studentDto.Add(newDTO);
                    }
                }

                return Ok(studentDto);
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
