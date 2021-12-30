using ACE.Domain.Abstract;
using ACE.Domain.Abstract.IControlledRepo;
using ACE.Domain.Entities;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.Data;
using ACEdatabaseAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ACEdatabaseAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExamAttendanceController : ControllerBase
    {
        IExamAttendanceRepo _examAttRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        IAcademicYearRepo _acadYearRepo;
        ISemesterRepo _semesterRepo;
        IExamTimetableRepo _examTTRepo;

        public ExamAttendanceController(IExamAttendanceRepo examAttRepo, UserManager<ApplicationUser> userManager,
                                    IAcademicYearRepo acadYearRepo,  ISemesterRepo semesterRepo, IExamTimetableRepo examTTRepo)
        {
            _userManager = userManager;
            _examAttRepo = examAttRepo;
            _acadYearRepo = acadYearRepo;
            _semesterRepo = semesterRepo;
            _examTTRepo = examTTRepo;
        }

        [HttpGet]
        [Route("Get/{CourseID}")]
        public IActionResult Get(Guid CourseID)
        {
            try
            {
                if (User.IsInRole("Exam&Records"))
                {
                    var acadYear = _acadYearRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    var semester = _semesterRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    var result = _examAttRepo.GetAll().Where(x => x.CourseID == CourseID && x.AcademicYearID == acadYear && x.SemesterID == semester);
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
        [Route("Get/{CourseID}/{AcademicYearID}/{SemesterID}")]
        public IActionResult Get(Guid CourseID, Guid AcademicYearID, Guid SemesterID)
        {
            try
            {
                if (User.IsInRole("Exam&Records"))
                {
                   
                    var result = _examAttRepo.GetAll().Where(x => x.CourseID == CourseID && x.AcademicYearID == AcademicYearID && x.SemesterID == SemesterID);
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
        [Route("Start/")]
        public IActionResult CreateAttendance(StartingExamAttendance model)
        {
            try
            {
                if (User.IsInRole("Exam&Records"))
                {
                    var user = User.Identity.Name;
                    if (ModelState.IsValid)
                    {

                        var currentAcademicmicYear = _acadYearRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                        var sentAcadYear = _acadYearRepo.FindBy(x => x.Id == model.AcademicYearID).FirstOrDefault();

                        if (model.AcademicYearID != currentAcademicmicYear)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Attendance is only valid for current academic year"));
                        }
                        else
                        {
                            if(sentAcadYear == null)
                            {
                                return StatusCode((int)HttpStatusCode.Unauthorized,
                                                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                                                "Invalid Academic Year"));
                            }
                        }

                        var timetable = _examTTRepo.FindBy(x => x.CourseID == model.CourseID).FirstOrDefault();
                        if(model.SupervisorID != timetable.SupervisorID)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "You are not the supervisor for this course"));
                        }

                        var semester = _semesterRepo.FindBy(x => x.Id == model.SemesterID).FirstOrDefault();
                        if (semester == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Semester"));
                        }

                        var examAtt = new ExamAttendance();
                        examAtt.AcademicYearID = model.AcademicYearID;
                        examAtt.CourseID = model.CourseID;
                        examAtt.ExamStartDateTime = DateTime.UtcNow.AddHours(1);
                        examAtt.ExamStartingStudents = JsonConvert.SerializeObject(model.ExamStartingStudents);
                        examAtt.SupervisorID = model.SupervisorID;
                        examAtt.SemesterID = model.SemesterID;

                        _examAttRepo.Add(examAtt);
                        _examAttRepo.Save(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
                        
                        return Ok(new
                        {
                            Message = "Starting Exam Attendance Marked"
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
        [Route("End/{AttendanceID}")]
        public IActionResult EditAttendance(Guid AttendanceID, EndingExamAttendance model)
        {
            try
            {
                if (User.IsInRole("Exam&Records"))
                {
                    var attendance = _examAttRepo.FindBy(x => x.Id == AttendanceID).FirstOrDefault();
                    if (attendance == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Exam Attedance Record not created"));
                    }
                    var user = User.Identity.Name;
                    if (ModelState.IsValid)
                    {
                        if(model.SupervisorID != attendance.SupervisorID)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Exam Supervisor not the same with exam end supervisor"));
                        }
                        attendance.ExamEndingStudents = JsonConvert.SerializeObject(model.ExamEndingStudents);
                        _examAttRepo.Edit(attendance);
                        _examAttRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
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
                if (User.IsInRole("Exam&Records"))
                {
                    var user = User.Identity.Name;
                    var result = _examAttRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    if (result == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Exam Attendance does not exist"));
                    }
                    _examAttRepo.Delete(result);
                    _examAttRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(new
                    {
                        Message = "Exam Attendance Deleted Successfully"
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
