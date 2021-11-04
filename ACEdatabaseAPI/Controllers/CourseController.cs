using ACE.Domain.Abstract;
using ACE.Domain.Abstract.IControlledRepo;
using ACE.Domain.Entities;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.Data;
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
    public class CourseController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        ICourseRepo _courseRepo;
        IAcademicYearRepo _acadYearRepo;
        ISemesterRepo _semesterRepo;
        ISchoolRepo _schoolRepo;
        IDepartmentRepo _deptRepo;
        IMapper _mapper;
        public CourseController(UserManager<ApplicationUser> userManager, ICourseRepo courseRepo, IAcademicYearRepo acadYearRepo,
            ISemesterRepo semesterRepo, ISchoolRepo schoolRepo, IDepartmentRepo deptRepo, IMapper mapper)
        {
            _userManager = userManager;
            _courseRepo = courseRepo;
            _acadYearRepo = acadYearRepo;
            _semesterRepo = semesterRepo;
            _schoolRepo = schoolRepo;
            _deptRepo = deptRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public IActionResult GetAllCourses()
        {
            try
            {
                if (User.IsInRole("Staff") || User.IsInRole("Student"))
                {
                    var result = _courseRepo.GetAll().ToList();
                    return Ok(result);
                }
                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Unauthorized Access"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [Route("CurrentYear/All")]
        public IActionResult GetAllCurrentAcademicYearCourses()
        {
            try
            {
                if (User.IsInRole("Staff") || User.IsInRole("Student"))
                {
                    var currentAcademicYearYear = _acadYearRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    var result = _courseRepo.GetAll().Where(x => x.AcademicYearID == currentAcademicYearYear).ToList();
                    return Ok(result);
                }
                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Unauthorized Access"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [Route("Get/{AcademicYearID}")]
        public IActionResult GetAllAcademicYearCourses(Guid AcademicYearID)
        {
            try
            {
                if (User.IsInRole("Staff") || User.IsInRole("Student"))
                {
                    var result = _courseRepo.GetAll().Where(x => x.AcademicYearID == AcademicYearID).ToList();
                    return Ok(result);
                }
                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Unauthorized Access"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [Route("Get/{AcademicYearID}/{SemesterID}")]
        public IActionResult GetAllAcademicYearSemesterCourses(Guid AcademicYearID, Guid SemesterID)
        {
            try
            {
                if (User.IsInRole("Staff") || User.IsInRole("Student"))
                {
                    var result = _courseRepo.GetAll().Where(x => x.AcademicYearID == AcademicYearID && x.SemesterID == SemesterID).ToList();
                    return Ok(result);
                }
                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Unauthorized Access"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [Route("Get/{SchoolID}")]
        public IActionResult GetAllSchoolCourses(Guid SchoolID)
        {
            try
            {
                if (User.IsInRole("Staff") || User.IsInRole("Student"))
                {
                    var result = _courseRepo.GetAll().Where(x => x.SchoolID == SchoolID).ToList();
                    return Ok(result);
                }
                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Unauthorized Access"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [Route("Get/{SchoolID}/{DepartmentID}")]
        public IActionResult GetAllSchoolDepartmentCourses(Guid SchoolID, Guid DepartmentID)
        {
            try
            {
                if (User.IsInRole("Staff") || User.IsInRole("Student"))
                {
                    var result = _courseRepo.GetAll().Where(x => x.SchoolID == SchoolID && x.DepartmentID == DepartmentID).ToList();
                    return Ok(result);
                }
                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Unauthorized Access"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [Route("Get/{LecturerID}")]
        public IActionResult GetAllLecturerCourses(Guid LecturerID)
        {
            try
            {
                if (User.IsInRole("Staff") || User.IsInRole("Student"))
                {
                    var result = _courseRepo.GetAll().Where(x => x.LeadLecturerID == LecturerID).ToList();
                    return Ok(result);
                }
                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Unauthorized Access"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [Route("Get/{ID}")]
        public IActionResult GetCoursesByID(Guid ID)
        {
            try
            {
                if (User.IsInRole("Staff") || User.IsInRole("Student"))
                {
                    var result = _courseRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    return Ok(result);
                }
                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Unauthorized Access"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateCourse(CreateCourse model)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var user = User.Identity.Name;
                    if (ModelState.IsValid)
                    {
                        var acadYear = _acadYearRepo.FindBy(x => x.Id == model.AcademicYearID).FirstOrDefault();
                        if(acadYear == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Academic Year"));
                        }

                        var semester = _semesterRepo.FindBy(x => x.Id == model.SemesterID).FirstOrDefault();
                        if (semester == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Semester"));
                        }

                        var school = _schoolRepo.FindBy(x => x.Id == model.SchoolID).FirstOrDefault();
                        if (school == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid School"));
                        }

                        var dept = _deptRepo.FindBy(x => x.Id == model.DepartmentID).FirstOrDefault();
                        if (dept == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Department"));
                        }

                        var lecturer = _userManager.FindByIdAsync(model.LeadLecturerID.ToString()).Result;
                        if (lecturer == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Lead Lecturer does not exist"));
                        }
                        else
                        {
                            if (!_userManager.IsInRoleAsync(lecturer, "Staff").Result) { }
                            {
                                return StatusCode((int)HttpStatusCode.Unauthorized,
                                    new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                    "Lecturer is not a Staff"));
                            }
                        }

                        if (!String.IsNullOrEmpty(model.AssistantLecturerID.ToString()))
                        {
                            var asstLecturer = _userManager.FindByIdAsync(model.AssistantLecturerID.ToString()).Result;
                            if (asstLecturer == null)
                            {
                                return StatusCode((int)HttpStatusCode.Unauthorized,
                                new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                    "Assistant Lecturer"));
                            }
                            else
                            {
                                if (!_userManager.IsInRoleAsync(asstLecturer, "Staff").Result) { }
                                {
                                    return StatusCode((int)HttpStatusCode.Unauthorized,
                                        new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                        "Assistant Lecturer is not a Staff"));
                                }
                            }
                        }

                        var newCourse = new Course();
                        var course = _mapper.Map(model, newCourse);
                        _courseRepo.Add(course);
                        _courseRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new
                        {
                            Message = "Course Created Successfully"
                        });

                    }
                    return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Request"));
                }
                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Unauthorized Access"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPut]
        [Route("Edit/{ID}")]
        public IActionResult EditCourse(Guid ID, CreateCourse model)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var existingCourse = _courseRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    if (existingCourse == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Course Does Not Exist"));
                    }
                    var user = User.Identity.Name;
                    if (ModelState.IsValid)
                    {
                        var acadYear = _acadYearRepo.FindBy(x => x.Id == model.AcademicYearID).FirstOrDefault();
                        if (acadYear == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Academic Year"));
                        }

                        var semester = _semesterRepo.FindBy(x => x.Id == model.SemesterID).FirstOrDefault();
                        if (semester == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Semester"));
                        }

                        var school = _schoolRepo.FindBy(x => x.Id == model.SchoolID).FirstOrDefault();
                        if (school == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid School"));
                        }

                        var dept = _deptRepo.FindBy(x => x.Id == model.DepartmentID).FirstOrDefault();
                        if (dept == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Department"));
                        }

                        var lecturer = _userManager.FindByIdAsync(model.LeadLecturerID.ToString()).Result;
                        if (lecturer == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Lead Lecturer does not exist"));
                        }
                        else
                        {
                            if (!_userManager.IsInRoleAsync(lecturer, "Staff").Result) { }
                            {
                                return StatusCode((int)HttpStatusCode.Unauthorized,
                                    new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                    "Lecturer is not a Staff"));
                            }
                        }

                        if (!String.IsNullOrEmpty(model.AssistantLecturerID.ToString()))
                        {
                            var asstLecturer = _userManager.FindByIdAsync(model.AssistantLecturerID.ToString()).Result;
                            if (asstLecturer == null)
                            {
                                return StatusCode((int)HttpStatusCode.Unauthorized,
                                new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                    "Assistant Lecturer"));
                            }
                            else
                            {
                                if (!_userManager.IsInRoleAsync(asstLecturer, "Staff").Result) { }
                                {
                                    return StatusCode((int)HttpStatusCode.Unauthorized,
                                        new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                        "Assistant Lecturer is not a Staff"));
                                }
                            }
                        }

                        var course = _mapper.Map(model, existingCourse);
                        _courseRepo.Edit(course);
                        _courseRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new
                        {
                            Message = "Course Updated Successfully"
                        });

                    }
                    return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Request"));
                }
                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Unauthorized Access"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpDelete]
        [Route("Delete/{ID}")]
        public IActionResult DeleteCourse(Guid ID)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var user = User.Identity.Name;
                    var result = _courseRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    if(result == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Course Does Not Exist"));
                    }
                    _courseRepo.Delete(result);
                    _courseRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(new { 
                        Message="Course Deleted Successfully"
                    });
                }
                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Unauthorized Access"));
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
