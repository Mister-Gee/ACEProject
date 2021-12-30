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
using Newtonsoft.Json;
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
        ISchoolRepo _schoolRepo;
        IDepartmentRepo _deptRepo;
        IvCourseRepo _vCourseRepo;
        public CourseController(UserManager<ApplicationUser> userManager, ICourseRepo courseRepo, IvCourseRepo vCourseRepo,
             ISchoolRepo schoolRepo, IDepartmentRepo deptRepo)
        {
            _userManager = userManager;
            _courseRepo = courseRepo;
            _vCourseRepo = vCourseRepo;
            _schoolRepo = schoolRepo;
            _deptRepo = deptRepo;
        }

        [HttpGet]
        [Route("All")]
        public IActionResult GetAllCourses()
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var result = _vCourseRepo.GetAll().ToList();
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
        [Route("Get/School/{SchoolID}")]
        public IActionResult GetAllSchoolCourses(Guid SchoolID)
        {
            try
            {
                if (User.IsInRole("Staff") || User.IsInRole("Student"))
                {
                    var result = _vCourseRepo.GetAll().Where(x => x.SchoolID == SchoolID).ToList();
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
        [Route("Get/Department/{DepartmentID}")]
        public IActionResult GetAllSchoolDepartmentCourses(Guid DepartmentID)
        {
            try
            {
                if (User.IsInRole("Staff") || User.IsInRole("Student"))
                {
                    var result = _vCourseRepo.GetAll().Where(x => x.DepartmentID == DepartmentID).ToList();
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
        [Route("Get/Student/EligibleCourses/{DepartmentID}")]
        public IActionResult GetAllStudentEligibleCourses(Guid DepartmentID)
        {
            try
            {
                if (User.IsInRole("Student"))
                {
                    List<vCourse> studentCourseList = new List<vCourse>();
                    var result = _vCourseRepo.GetAll().ToList();
                    foreach(var course in result)
                    {
                        var eligibleDepartment = JsonConvert.DeserializeObject<List<Guid>>(course.EligibleDepartments);

                        if((course.isDepartmental && course.DepartmentID == DepartmentID) || course.isGeneral || eligibleDepartment.Contains(DepartmentID))
                        {
                            studentCourseList.Add(course);
                        }
                    }
                    return Ok(studentCourseList);
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
        [Route("Get/ByLecturer/{LecturerID}")]
        public IActionResult GetAllLecturerCourses(Guid LecturerID)
        {
            try
            {
                if (User.IsInRole("Staff") || User.IsInRole("Student"))
                {
                    var result = _vCourseRepo.GetAll().Where(x => x.LeadLecturerID == LecturerID).ToList();
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

        [HttpGet]
        [Route("Search/Lecturer/{LecturerID}")]
        public IActionResult GetCoursesByLecturer(Guid LecturerID)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var result = _vCourseRepo.FindBy(x => x.AssistantLecturerID == LecturerID || x.LeadLecturerID == LecturerID || x.OtherCourseLecturerID == LecturerID).FirstOrDefault();
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
                if (User.IsInRole("MIS"))
                {
                    var user = User.Identity.Name;
                    if (ModelState.IsValid)
                    {
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
                            //if (!_userManager.IsInRoleAsync(lecturer, "Staff").Result) { }
                            //{
                            //    return StatusCode((int)HttpStatusCode.Unauthorized,
                            //        new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                            //        "Lecturer is not a Staff"));
                            //}
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
                                //if (!_userManager.IsInRoleAsync(asstLecturer, "Staff").Result) { }
                                //{
                                //    return StatusCode((int)HttpStatusCode.Unauthorized,
                                //        new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                //        "Assistant Lecturer is not a Staff"));
                                //}
                            }
                        }

                        var course = new ACE.Domain.Entities.Course();
                        course.AssistantLecturerID = model.AssistantLecturerID.Value;
                        course.CourseCode = model.CourseCode;
                        course.CourseDescription = model.CourseDescription;
                        course.CourseTitle = model.CourseTitle;
                        course.CourseUnit = model.CourseUnit;
                        course.DepartmentID = model.DepartmentID;
                        course.EligibleDepartments = JsonConvert.SerializeObject(model.EligibleDepartments);
                        course.isDepartmental = model.isDepartmental;
                        course.isGeneral = model.isGeneral;
                        course.isOptional = model.isOptional;
                        course.LeadLecturerID = model.LeadLecturerID;
                        course.OtherCourseLecturer = model.OtherCourseLecturer.Value;
                        course.SchoolID = model.SchoolID;

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
        public IActionResult EditCourse(Guid ID, EditCreatedCourse model)
        {
            try
            {
                if (User.IsInRole("MIS"))
                {
                    var course = _courseRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    if (course == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Course Does Not Exist"));
                    }
                    var user = User.Identity.Name;
                    if (ModelState.IsValid)
                    {
                       
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
                            //if (!_userManager.IsInRoleAsync(lecturer, "Staff").Result) { }
                            //{
                            //    return StatusCode((int)HttpStatusCode.Unauthorized,
                            //        new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                            //        "Lecturer is not a Staff"));
                            //}
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
                                //if (!_userManager.IsInRoleAsync(asstLecturer, "Staff").Result) { }
                                //{
                                //    return StatusCode((int)HttpStatusCode.Unauthorized,
                                //        new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                //        "Assistant Lecturer is not a Staff"));
                                //}
                            }
                        }

                        course.AssistantLecturerID = model.AssistantLecturerID.Value;
                        course.CourseCode = model.CourseCode;
                        course.CourseDescription = model.CourseDescription;
                        course.CourseTitle = model.CourseTitle;
                        course.CourseUnit = model.CourseUnit;
                        course.DepartmentID = model.DepartmentID;
                        course.EligibleDepartments = JsonConvert.SerializeObject(model.EligibleDepartments);
                        course.isDepartmental = model.isDepartmental;
                        course.isGeneral = model.isGeneral;
                        course.isOptional = model.isOptional;
                        course.LeadLecturerID = model.LeadLecturerID;
                        course.OtherCourseLecturer = model.OtherCourseLecturer.Value;
                        course.SchoolID = model.SchoolID;

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
                if (User.IsInRole("MIS"))
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
