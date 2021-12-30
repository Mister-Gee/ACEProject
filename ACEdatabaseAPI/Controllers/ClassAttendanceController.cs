using ACE.Domain.Abstract;
using ACE.Domain.Abstract.IControlledRepo;
using ACE.Domain.Entities;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.Data;
using ACEdatabaseAPI.DTOModel;
using ACEdatabaseAPI.Model;
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
    public class ClassAttendanceController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        IClassAttendanceRepo _classAttendanceRepo;
        IAcademicYearRepo _acadYearRepo;
        ISemesterRepo _semesterRepo;
        ICourseRepo _courseRepo;
        ICurrentAcademicSessionRepo _currentAcadYearRepo;
        IStudentRegCourseRepo _courseRegItemRepo;

        public ClassAttendanceController(IClassAttendanceRepo classAttendanceRepo, UserManager<ApplicationUser> userManager, 
            IAcademicYearRepo acadYearRepo, ISemesterRepo semesterRepo, ICourseRepo courseRepo, ICurrentAcademicSessionRepo currentAcadYearRepo,
            IStudentRegCourseRepo courseRegItemRepo)
        {
            _classAttendanceRepo = classAttendanceRepo;
            _userManager = userManager;
            _acadYearRepo = acadYearRepo;
            _semesterRepo = semesterRepo;
            _courseRepo = courseRepo;
            _currentAcadYearRepo = currentAcadYearRepo;
            _courseRegItemRepo = courseRegItemRepo;
        }

        [HttpGet]
        [Route("TotalClassesHeld/{CourseID}/{AcademicYearID}/{SemesterID}")]
        public IActionResult TotalClassesHeld(Guid CourseID, Guid AcademicYearID, Guid SemesterID)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var result = _classAttendanceRepo.GetAll().Where(x => x.CourseID == CourseID && x.AcademicYearID == AcademicYearID && x.SemesterID == SemesterID).Count();
                    return Ok(new
                    {
                        TotalClassesHeld = result
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


        [HttpGet]
        [Route("TotalClassesHeld/CurrentAcademicYear/{CourseID}")]
        public IActionResult TotalClassesHeld(Guid CourseID)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var acadYear = _acadYearRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    var semester = _semesterRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    var result = _classAttendanceRepo.GetAll().Where(x => x.CourseID == CourseID && x.AcademicYearID == acadYear && x.SemesterID == semester).Count();
                    return Ok(new
                    {
                        TotalClassesHeld = result
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

        [HttpGet]
        [Route("Record/CurrentAcademicYear/{CourseID}")]
        public IActionResult AttendanceRecord(Guid CourseID)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var acadYear = _acadYearRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    var semester = _semesterRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    var result = _classAttendanceRepo.GetAll().Where(x => x.CourseID == CourseID && x.AcademicYearID == acadYear && x.SemesterID == semester).ToList();
                    List<AttendanceDTO> listDto = new List<AttendanceDTO>();
                    foreach(var itm in result)
                    {
                        var dto = new AttendanceDTO();
                        dto.Id = itm.Id;
                        dto.ClassWeek = itm.ClassWeek;

                        var studentsID = JsonConvert.DeserializeObject<List<Guid>>(itm.PresentStudent);
                        List<StudentAttendanceDetailDTO> presentStudentsDetails = new List<StudentAttendanceDetailDTO>();
                        foreach (var studentId in studentsID)
                        {
                            var studentDetail = new StudentAttendanceDetailDTO();
                            var student = _userManager.FindByIdAsync(studentId.ToString()).Result;
                            studentDetail.Name = student.FirstName + " " + student.LastName;
                            studentDetail.MatricNumber = student.MatricNumber;
                            presentStudentsDetails.Add(studentDetail);
                        }
                        dto.PresentStudents = presentStudentsDetails;
                    }
                    return Ok(listDto);
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
        [Route("Record/Student/Courses")]
        public IActionResult StudentCourseAttendance()
        {
            try
            {
                if (User.IsInRole("Student"))
                {
                    List<CourseAttendanceDTO> attendance = new List<CourseAttendanceDTO>();
                    var student = _userManager.FindByNameAsync(User.Identity.Name).Result;
                    var currentAcadYear = _currentAcadYearRepo.GetAll().FirstOrDefault();
                    var studentRegisteredCourses = _courseRegItemRepo.FindBy(x => x.StudentId == Guid.Parse(student.Id) 
                        && x.SemesterID == currentAcadYear.SemesterID && x.AcademicYearID == currentAcadYear.AcademicYearID).ToList();
                    if(studentRegisteredCourses.Count < 1)
                    {
                        return Ok(attendance);
                    }

                    foreach(var item in studentRegisteredCourses)
                    {
                        var studentAtt = new CourseAttendanceDTO();
                        var course = _courseRepo.FindBy(x => x.Id == item.CourseID).FirstOrDefault();
                        var courseAttendance = _classAttendanceRepo.FindBy(x => x.CourseID == item.CourseID
                            && x.SemesterID == currentAcadYear.SemesterID && x.AcademicYearID == currentAcadYear.AcademicYearID).ToList();
                        var studentCourseAtt = courseAttendance.Where(x => JsonConvert.DeserializeObject<List<Guid>>(x.PresentStudent).Contains(Guid.Parse(student.Id))).ToList();
                        int totalClassesHeld = courseAttendance.Count;
                        int totalClassesAttended = studentCourseAtt.Count;
                        double attPercent = (totalClassesAttended / totalClassesHeld) * 100;
                        studentAtt.AttendancePercent = attPercent;
                        studentAtt.Course = course.CourseTitle;
                        studentAtt.CourseUnit = item.CourseUnit;
                        studentAtt.Id = course.Id;
                        studentAtt.TotalClassesAttended = totalClassesAttended;
                        studentAtt.TotalClassesHeld = totalClassesHeld;

                        attendance.Add(studentAtt);
                        
                    }
                    return Ok(attendance);
                    
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
        [Route("Record/CurrentAcademicYear/{CourseID}/{ClassWeek}")]
        public IActionResult AttendanceRecord(Guid CourseID, int ClassWeek)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var acadYear = _acadYearRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    var semester = _semesterRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    var result = _classAttendanceRepo.GetAll().Where(x => x.CourseID == CourseID && x.AcademicYearID == acadYear && x.SemesterID == semester && x.ClassWeek == ClassWeek).FirstOrDefault();

                    var attendance = new AttendanceDTO();
                    attendance.Id = result.Id;
                    attendance.ClassWeek = result.ClassWeek;

                    var studentsID = JsonConvert.DeserializeObject<List<Guid>>(result.PresentStudent);
                    List<StudentAttendanceDetailDTO> presentStudentsDetails = new List<StudentAttendanceDetailDTO>();
                    foreach (var studentId in studentsID)
                    {
                        var studentDetail = new StudentAttendanceDetailDTO();
                        var student = _userManager.FindByIdAsync(studentId.ToString()).Result;
                        studentDetail.Name = student.FirstName + " " + student.LastName;
                        studentDetail.MatricNumber = student.MatricNumber;
                        presentStudentsDetails.Add(studentDetail);
                    }
                    attendance.PresentStudents = presentStudentsDetails;
                    
                    return Ok(attendance);
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
        public IActionResult CreateAttendance(CreateAttendance model)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var user = User.Identity.Name;
                    if (ModelState.IsValid)
                    {
                        var course = _courseRepo.FindBy(x => x.Id == model.CourseID).FirstOrDefault();
                        if(course == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Course"));
                        }

                        var currentAcademicmicYear = _acadYearRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                        if(model.AcademicYearID != currentAcademicmicYear)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "You cant Create course for previous academic year"));
                        }

                        if(model.ClassWeek < 1)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "ClassWeek cannot be less than 1"));
                        }
                        else
                        {
                            var classWeeks = _classAttendanceRepo.GetAll().Select(x => x.ClassWeek).ToList();
                            if (classWeeks.Contains(model.ClassWeek))
                            {
                                return StatusCode((int)HttpStatusCode.Unauthorized,
                                                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                                                "ClassWeek attendance already exist"));
                            }
                        }

                        var attendance = new ClassAttendance();
                        attendance.AcademicYearID = model.AcademicYearID;
                        attendance.ClassWeek = model.ClassWeek;
                        attendance.CourseID = model.CourseID;
                        attendance.SemesterID = model.SemesterID;
                        attendance.ClassDateTime = DateTime.UtcNow.AddHours(1);
                        attendance.PresentStudent = JsonConvert.SerializeObject(model.PresentStudent);
                        _classAttendanceRepo.Add(attendance);
                        _classAttendanceRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new
                        {
                            Message = "Attendance Marked"
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
        public IActionResult EditAttendance(Guid ID, CreateAttendance model)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var attendance = _classAttendanceRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    if(attendance == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Attedance Record does not exist"));
                    }
                    var user = User.Identity.Name;
                    if (ModelState.IsValid)
                    {
                      

                        attendance.AcademicYearID = model.AcademicYearID;
                        attendance.ClassWeek = model.ClassWeek;
                        attendance.CourseID = model.CourseID;
                        attendance.SemesterID = model.SemesterID;
                        attendance.PresentStudent = JsonConvert.SerializeObject(model.PresentStudent);
                        _classAttendanceRepo.Edit(attendance);
                        _classAttendanceRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new
                        {
                            Message = "Attendance Updated"
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
        public IActionResult DeleteAttendance(Guid ID)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var user = User.Identity.Name;
                    var result = _classAttendanceRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    if (result == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Course Does Not Exist"));
                    }
                    _classAttendanceRepo.Delete(result);
                    _classAttendanceRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(new
                    {
                        Message = "Attendance Deleted Successfully"
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
