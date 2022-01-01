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
        IvExamAttendanceRepo _vExamAttRepo;
        ICurrentAcademicSessionRepo _currentAcadRepo;
        IvStudentRegisteredCourseRepo _vStudentRegCourseRepo;
        ICourseRepo _courseRepo;
        public ExamAttendanceController(IExamAttendanceRepo examAttRepo, UserManager<ApplicationUser> userManager,
                                    IAcademicYearRepo acadYearRepo,  ISemesterRepo semesterRepo, IExamTimetableRepo examTTRepo,
                                    IvExamAttendanceRepo vExamAttRepo, ICurrentAcademicSessionRepo currentAcadRepo,
                                    IvStudentRegisteredCourseRepo vStudentRegCourseRepo, ICourseRepo courseRepo)
        {
            _userManager = userManager;
            _examAttRepo = examAttRepo;
            _acadYearRepo = acadYearRepo;
            _semesterRepo = semesterRepo;
            _examTTRepo = examTTRepo;
            _vExamAttRepo = vExamAttRepo;
            _currentAcadRepo = currentAcadRepo;
            _vStudentRegCourseRepo = vStudentRegCourseRepo;
            _courseRepo = courseRepo;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<vExamAttendance>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("CurrentSession/Get/{CourseID}")]
        public IActionResult Get(Guid CourseID)
        {
            try
            {
                if (User.IsInRole("Exam&Records"))
                {
                    var currentAcad = _currentAcadRepo.GetAll().FirstOrDefault();
                    var result = _vExamAttRepo.GetAll().Where(x => x.CourseID == CourseID && x.AcademicYearID == currentAcad.AcademicYearID && x.SemesterID == currentAcad.SemesterID).ToList();
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
                   
                    var result = _vExamAttRepo.GetAll().Where(x => x.CourseID == CourseID && x.AcademicYearID == AcademicYearID && x.SemesterID == SemesterID).ToList();
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
        [ProducesResponseType(typeof(List<CourseExamAttendanceDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Record/Student")]
        public IActionResult StudentAttendanceRecord()
        {
            try
            {
                if (User.IsInRole("Student"))
                {
                    List<CourseExamAttendanceDTO> att = new List<CourseExamAttendanceDTO>();
                    var currentAcadYear = _currentAcadRepo.GetAll().FirstOrDefault();
                    var student = _userManager.FindByNameAsync(User.Identity.Name).Result;
                    var studentRegCourses = _vStudentRegCourseRepo.FindBy(x => x.StudentId == Guid.Parse(student.Id)
                        && x.AcademicYearID == currentAcadYear.AcademicYearID && x.SemesterID == currentAcadYear.SemesterID);

                    foreach(var item in studentRegCourses)
                    {
                        var courseAtt = new CourseExamAttendanceDTO();
                        var examAtt = _vExamAttRepo.FindBy(x => x.CourseID == item.CourseID).FirstOrDefault();
                        if(examAtt == null)
                        {
                            courseAtt.IsCommence = false;
                            courseAtt.Course = item.Course;
                            courseAtt.EndAttendance = false;
                            courseAtt.EndTime = null;
                            courseAtt.Id = Guid.NewGuid();
                            courseAtt.StartAttendance = false;
                            courseAtt.StartTime = null;
                            courseAtt.Supervisor = "";
                        }
                        else
                        {
                            var startingStudents = JsonConvert.DeserializeObject<List<Guid>>(examAtt.ExamStartingStudents);
                            var endingStudents = JsonConvert.DeserializeObject<List<Guid>>(examAtt.ExamEndingStudents);

                            courseAtt.IsCommence = true;
                            courseAtt.Course = item.Course;
                            if (endingStudents.Contains(Guid.Parse(student.Id)))
                            {
                                courseAtt.EndAttendance = true;
                            }
                            else
                            {
                                courseAtt.EndAttendance = false;
                            }
                            courseAtt.EndTime = examAtt.ExamEndDateTime;
                            courseAtt.Id = examAtt.Id;
                            if (startingStudents.Contains(Guid.Parse(student.Id)))
                            {
                                courseAtt.StartAttendance = true;
                            }
                            else
                            {
                                courseAtt.StartAttendance = false;
                            }
                            courseAtt.StartTime = examAtt.ExamStartDateTime;
                            courseAtt.Supervisor = examAtt.Supervisor;
                        }
                        att.Add(courseAtt);
                    }
                    return Ok(att);
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
        [ProducesResponseType(typeof(StudentExamAttendance), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Course/{CourseID}")]
        public IActionResult CourseAttendanceRecord(Guid CourseID)
        {
            try
            {
                if (User.IsInRole("Exam&Records") || User.IsInRole("Lecturer"))
                {
                    var examAtt = new StudentExamAttendance();
                    List<ExamStudents> studentList = new List<ExamStudents>();
                    var currentAcadYear = _currentAcadRepo.GetAll().FirstOrDefault();
                    var staff = _userManager.FindByNameAsync(User.Identity.Name).Result;
                    var course = _courseRepo.FindBy(x => x.Id == CourseID).FirstOrDefault();
                    if (User.IsInRole("Lecturer"))
                    {
                        if(course.AssistantLecturerID != Guid.Parse(staff.Id))
                        {
                            if(course.LeadLecturerID != Guid.Parse(staff.Id))
                            {
                                if(course.OtherCourseLecturer != Guid.Parse(staff.Id))
                                {
                                    return StatusCode((int)HttpStatusCode.Unauthorized,
                                        new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                            "You dont have access to this Course Exam Attendance"));
                                }
                            }
                        }
                    }


                    var examAttDetail = _vExamAttRepo.FindBy(x => x.CourseID == CourseID && x.AcademicYearID == currentAcadYear.AcademicYearID
                        && x.SemesterID == currentAcadYear.SemesterID).FirstOrDefault();
                    if(examAttDetail == null)
                    {
                        return Ok(examAtt);
                    }

                    var courseStudent = _vStudentRegCourseRepo.FindBy(x => x.CourseID == CourseID).ToList();

                    examAtt.Course = examAttDetail.Course;
                    examAtt.EndTime = examAttDetail.ExamEndDateTime;
                    examAtt.Id = examAttDetail.Id;
                    examAtt.StartTime = examAttDetail.ExamStartDateTime;
                    examAtt.Supervisor = examAttDetail.Supervisor;

                    foreach(var item in courseStudent)
                    {
                        var startingStudents = JsonConvert.DeserializeObject<List<Guid>>(examAttDetail.ExamStartingStudents);
                        var endingStudents = JsonConvert.DeserializeObject<List<Guid>>(examAttDetail.ExamEndingStudents);

                        var student = new ExamStudents();
                        student.StudentMatricNumber = item.StudentMatricNumber;
                        if (startingStudents.Contains(item.StudentId))
                        {
                            student.StartAttendance = true;
                        }
                        else
                        {
                            student.StartAttendance = false;
                        }

                        if (endingStudents.Contains(item.StudentId))
                        {
                            student.EndAttendance = true;
                        }
                        else
                        {
                            student.EndAttendance = false;
                        }
                        studentList.Add(student);
                    }
                    examAtt.Students = studentList;

                    return Ok(examAtt);
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

                        var currentAcademicYear = _currentAcadRepo.GetAll().FirstOrDefault();

                        var timetable = _examTTRepo.FindBy(x => x.CourseID == model.CourseID).FirstOrDefault();
                        if(model.SupervisorID != timetable.SupervisorID)
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest,
                            new ApiError((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(),
                                "You are not the supervisor for this course"));
                        }

                        var examAtt = new ExamAttendance();
                        examAtt.AcademicYearID = currentAcademicYear.AcademicYearID;
                        examAtt.CourseID = model.CourseID;
                        examAtt.ExamStartDateTime = DateTime.UtcNow.AddHours(1);
                        examAtt.ExamStartingStudents = JsonConvert.SerializeObject(model.ExamStartingStudents);
                        examAtt.SupervisorID = model.SupervisorID;
                        examAtt.SemesterID = currentAcademicYear.SemesterID;

                        _examAttRepo.Add(examAtt);
                        _examAttRepo.Save(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
                        
                        return Ok(new
                        {
                            Message = "Starting Exam Attendance Marked"
                        });

                    }
                    return StatusCode((int)HttpStatusCode.BadRequest,
                            new ApiError((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(),
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
                        return StatusCode((int)HttpStatusCode.BadRequest,
                            new ApiError((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(),
                                "Exam Attedance Record not created"));
                    }

                    var user = User.Identity.Name;
                    if (ModelState.IsValid)
                    {
                        if(model.SupervisorID != attendance.SupervisorID)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Exam Start Supervisor not the same with Exam End supervisor"));
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
