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
    public class CourseRegisterationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        IStudentRegCourseRepo _courseRegRepo;
        IAcademicYearRepo _acadYearRepo;
        ICourseRepo _courseRepo;
        ISemesterRepo _semesterRepo;
        IMapper _mapper;
        public CourseRegisterationController(IStudentRegCourseRepo courseRegRepo, IAcademicYearRepo acadYearRepo, ICourseRepo courseRepo, 
            ISemesterRepo semesterRepo, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _courseRegRepo = courseRegRepo;
            _acadYearRepo = acadYearRepo;
            _courseRepo = courseRepo;
            _semesterRepo = semesterRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public IActionResult StudentRegisteredAllCourse()
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var result = _courseRegRepo.GetAll().ToList();
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
        [Route("Student/CourseRegisterationHistory/All/{StudentID}")]
        public IActionResult StudentRegisteredCourseHistory(Guid StudentID)
        {
            try
            {
                if (User.IsInRole("Staff") || User.IsInRole("Student"))
                {
                    var result = _courseRegRepo.GetAll().Where(x =>  x.StudentId == StudentID).ToList();
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
        [Route("Student/CurrentAcademicYear/All/{StudentID}")]
        public IActionResult StudentRegisteredCourse(Guid StudentID)
        {
            try
            {
                if (User.IsInRole("Staff") || User.IsInRole("Student"))
                {
                    var currentAcadYear = _acadYearRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    var result = _courseRegRepo.GetAll().Where(x => x.AcademicYearID == currentAcadYear && x.StudentId == StudentID).ToList();
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
        [Route("Student/CurrentAcademicYear/All/{StudentID}/{SemesterID}")]
        public IActionResult StudentRegisteredCourseBySemester(Guid StudentID, Guid SemesterID)
        {
            try
            {
                if (User.IsInRole("Staff") || User.IsInRole("Student"))
                {
                    var currentAcadYear = _acadYearRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    var result = _courseRegRepo.GetAll().Where(x => x.AcademicYearID == currentAcadYear && x.SemesterID == SemesterID && StudentID == StudentID).ToList();
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
        [Route("Student/{StudentID}/{AcademicYearID}/{SemesterID}")]
        public IActionResult StudentRegisteredCourseBySemesterAndAcademicYear(Guid StudentID,  Guid SemesterID, Guid AcademicYearID)
        {
            try
            {
                if (User.IsInRole("Staff") || User.IsInRole("Student"))
                {
                    var result = _courseRegRepo.GetAll().Where(x => x.StudentId == StudentID && x.AcademicYearID == AcademicYearID && x.SemesterID == SemesterID).ToList();
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
        [Route("Register")]
        public IActionResult RegisterCourse(RegisterCourse model)
        {
            try
            {
                if (User.IsInRole("Staff") || User.IsInRole("Student"))
                {
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
                        else
                        {
                            var currentAcadYear = _acadYearRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                            if(currentAcadYear != model.AcademicYearID)
                            {
                                return StatusCode((int)HttpStatusCode.Unauthorized,
                                new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "You cant Register a Course for Previous Academic Session"));
                            }
                        }

                        var semester = _semesterRepo.FindBy(x => x.Id == model.SemesterID).FirstOrDefault();
                        if (semester == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Semester"));
                        }

                        var course = _courseRepo.FindBy(x => x.Id == model.CourseID).FirstOrDefault();
                        if (course == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Course"));
                        }

                        var student = _userManager.FindByIdAsync(model.StudentId.ToString()).Result;
                        if (student == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Student does not exist"));
                        }
                        else
                        {
                            if (student.Status != "Active") { }
                            {
                                return StatusCode((int)HttpStatusCode.Unauthorized,
                                    new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                    "Student is no Longer Active"));
                            }
                        }

                        var newCourse = new StudentRegisteredCourse();
                        var regCourse = _mapper.Map(model, newCourse);
                        _courseRegRepo.Add(regCourse);
                        _courseRegRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new
                        {
                            Message = "Course Registered Successfully"
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
        [Route("Register/Edit/{ID}")]
        public IActionResult EditRegisterCourse(Guid ID, RegisterCourse model)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var user = User.Identity.Name;
                    var registeredCourse = _courseRegRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    if(registeredCourse == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Registered Course Does Not Exist"));
                    }

                    if (ModelState.IsValid)
                    {
                        var acadYear = _acadYearRepo.FindBy(x => x.Id == model.AcademicYearID).FirstOrDefault();
                        if (acadYear == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Academic Year"));
                        }
                        else
                        {
                            var currentAcadYear = _acadYearRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                            if (currentAcadYear != model.AcademicYearID)
                            {
                                return StatusCode((int)HttpStatusCode.Unauthorized,
                                new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "You cant Register a Course for Previous Academic Session"));
                            }
                        }

                        var semester = _semesterRepo.FindBy(x => x.Id == model.SemesterID).FirstOrDefault();
                        if (semester == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Semester"));
                        }

                        var course = _courseRepo.FindBy(x => x.Id == model.CourseID).FirstOrDefault();
                        if (course == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Course"));
                        }

                        var student = _userManager.FindByIdAsync(model.StudentId.ToString()).Result;
                        if (student == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Student does not exist"));
                        }
                        else
                        {
                            if (student.Status != "Active") { }
                            {
                                return StatusCode((int)HttpStatusCode.Unauthorized,
                                    new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                    "Student is no Longer Active"));
                            }
                        }

                        var regCourse = _mapper.Map(model, registeredCourse);
                        _courseRegRepo.Add(regCourse);
                        _courseRegRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new
                        {
                            Message = "Registered Course Edited Successfully"
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
        public IActionResult DeleteRegisteredCourse(Guid ID)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var user = User.Identity.Name;
                    var result = _courseRegRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    if (result == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Course Does Not Exist"));
                    }
                    _courseRegRepo.Delete(result);
                    _courseRegRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(new
                    {
                        Message = "Registered Course Deleted Successfully"
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
