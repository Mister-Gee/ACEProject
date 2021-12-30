using ACE.Domain.Abstract;
using ACE.Domain.Abstract.IControlledRepo;
using ACE.Domain.Entities;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.Data;
using ACEdatabaseAPI.Helpers;
using ACEdatabaseAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExamTimeTableController : ControllerBase
    {
        IExamTimetableRepo _examTTRepo;
        IExcelHelper _excelHelper;
        ICourseRepo _courseRepo;
        IAcademicYearRepo _acadYearRepo;
        ISemesterRepo _semesterRepo;
        ICurrentAcademicSessionRepo _currentAcadYear;
        IStudentRegCourseRepo _courseRegItemRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        public ExamTimeTableController(IExamTimetableRepo examTTRepo, ICourseRepo courseRepo, IAcademicYearRepo acadYearRepo,
                                    ISemesterRepo semesterRepo, UserManager<ApplicationUser> userManager, IExcelHelper excelHelper, 
                                    ICurrentAcademicSessionRepo currentAcadYear, IStudentRegCourseRepo courseRegItemRepo)
        {
            _examTTRepo = examTTRepo;
            _courseRepo = courseRepo;
            _acadYearRepo = acadYearRepo;
            _semesterRepo = semesterRepo;
            _userManager = userManager;
            _excelHelper = excelHelper;
            _currentAcadYear = currentAcadYear;
            _courseRegItemRepo = courseRegItemRepo;
        }

        [HttpGet]
        [Route("CurrentAcademicYear/CurrentSemester/All")]
        public IActionResult GetAllExams()
        {
            try
            {
                if (User.IsInRole("Exam&Records") || User.IsInRole("MIS"))
                {
                    var acadYear = _acadYearRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    var semester = _semesterRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    var result = _examTTRepo.GetAll().Where(x => x.AcademicYearID == acadYear && x.SemesterID == semester);
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
        [Route("CurrentAcademicYear/CurrentSemester/{CourseID}")]
        public IActionResult GetAllExamByCourse(Guid CourseID)
        {
            try
            {
                if (User.IsInRole("Exam&Records") || User.IsInRole("Student") || User.IsInRole("MIS"))
                {
                    var acadYear = _acadYearRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    var semester = _semesterRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    var result = _examTTRepo.GetAll().Where(x => x.AcademicYearID == acadYear && x.SemesterID == semester && x.CourseID == CourseID).FirstOrDefault();
                    
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
        [Route("Student/Current")]
        public IActionResult StudentTimeTable()
        {
            try
            {
                if ( User.IsInRole("Student") )
                {
                    List<ExamTimetable> timetables = new List<ExamTimetable>();
                    var student = _userManager.FindByNameAsync(User.Identity.Name).Result;
                    var currentAcadYear = _currentAcadYear.GetAll().FirstOrDefault();
                    var studentRegCourses = _courseRegItemRepo.FindBy(x => x.StudentId == Guid.Parse(student.Id)
                        && x.AcademicYearID == currentAcadYear.AcademicYearID && x.SemesterID == currentAcadYear.SemesterID).ToList();
                    foreach(var item in studentRegCourses)
                    {
                        var timetable = _examTTRepo.FindBy(x => x.CourseID == item.CourseID
                            && x.AcademicYearID == currentAcadYear.AcademicYearID && x.SemesterID == currentAcadYear.SemesterID)
                            .OrderBy(x => x.ExamDateTime).FirstOrDefault();
                        timetables.Add(timetable);
                    }
                    return Ok(timetables);
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
        [Route("Upload")]
        public async Task<IActionResult> Create([FromForm] UploadFile model)
        {
            try
            {
                if (User.IsInRole("MIS"))
                {
                    if (model.ExcelSheet != null)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await model.ExcelSheet.CopyToAsync(stream);
                            var result = _excelHelper.ExamTimeTable(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString(), stream).Result;
                            if (result.Item1 == Status.Success)
                            {
                                return Ok(new
                                {
                                    Message = "Success",
                                    Failed = result.Item1
                                });
                            }
                            return BadRequest();
                        }
                    }
                    return BadRequest();
                }
                return Unauthorized();
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("CurrentAcademicYear/CurrentSemester/Create")]
        public IActionResult Create(CreateExamTimetable model)
        {
            try
            {
                if (User.IsInRole("MIS"))
                {
                    if (ModelState.IsValid)
                    {
                        var acadYear = _acadYearRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;
                        var semester = _semesterRepo.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id;

                        var supervisor = _userManager.FindByIdAsync(model.SupervisorID.ToString()).Result;
                        if(supervisor == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Supervisor does not exist"));
                        }
                        else
                        {
                            if(!_userManager.IsInRoleAsync(supervisor, "Staff").Result)
                            {
                                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Intended Supervisor is not a staff"));
                            }
                        }


                        var timetable = new ExamTimetable();
                        timetable.AcademicYearID = acadYear;
                        timetable.CourseID = model.CourseID;
                        timetable.ExamDateTime = model.ExamDateTime;
                        timetable.SemesterID = semester;
                        timetable.SupervisorID = model.SupervisorID;
                        _examTTRepo.Add(timetable);
                        _examTTRepo.Save(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new
                        {
                            Message = "Exam Timetable Created"
                        });
                    }
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
        [Route("CurrentAcademicYear/CurrentSemester/Edit/{ID}")]
        public IActionResult Edit(Guid ID, CreateExamTimetable model)
        {
            try
            {
                if (User.IsInRole("MIS"))
                {
                    if (ModelState.IsValid)
                    {
                        var timetable = _examTTRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                        if (timetable == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Exam Timetable doesnt exist"));
                        }
                        var supervisor = _userManager.FindByIdAsync(model.SupervisorID.ToString()).Result;
                        if (supervisor == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Supervisor does not exist"));
                        }
                        else
                        {
                            if (!_userManager.IsInRoleAsync(supervisor, "Staff").Result)
                            {
                                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Intended Supervisor is not a staff"));
                            }
                        }


                        timetable.CourseID = model.CourseID;
                        timetable.ExamDateTime = model.ExamDateTime;
                        timetable.SupervisorID = model.SupervisorID;
                        _examTTRepo.Edit(timetable);
                        _examTTRepo.Save(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new
                        {
                            Message = "Exam Timetable Updated"
                        });
                    }
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
        [Route("CurrentAcademicYear/CurrentSemester/Delete/{ID}")]
        public IActionResult Delete(Guid ID)
        {
            try
            {
                if (User.IsInRole("MIS"))
                {
                    
                    var timetable = _examTTRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    if (timetable == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                        new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                            "Exam Timetable doesnt exist"));
                    }
                        
                    _examTTRepo.Delete(timetable);
                    _examTTRepo.Save(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(new
                    {
                        Message = "Exam Timetable Updated"
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
