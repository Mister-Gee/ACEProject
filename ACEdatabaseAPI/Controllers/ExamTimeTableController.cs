using ACE.Domain.Abstract;
using ACE.Domain.Abstract.IControlledRepo;
using ACE.Domain.Entities;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.Data;
using ACEdatabaseAPI.Model;
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
    [Route("api/[controller]")]
    [ApiController]
    public class ExamTimeTableController : ControllerBase
    {
        IExamTimetableRepo _examTTRepo;
        ICourseRepo _courseRepo;
        IAcademicYearRepo _acadYearRepo;
        ISemesterRepo _semesterRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        public ExamTimeTableController(IExamTimetableRepo examTTRepo,
                                    ICourseRepo courseRepo,
                                    IAcademicYearRepo acadYearRepo,
                                    ISemesterRepo semesterRepo,
                                    UserManager<ApplicationUser> userManager)
        {
            _examTTRepo = examTTRepo;
            _courseRepo = courseRepo;
            _acadYearRepo = acadYearRepo;
            _semesterRepo = semesterRepo;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("CurrentAcademicYear/CurrentSemester/All")]
        public IActionResult GetAllExams()
        {
            try
            {
                if (User.IsInRole("Staff"))
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
                if (User.IsInRole("Staff") || User.IsInRole("Student"))
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

        [HttpPost]
        [Route("CurrentAcademicYear/CurrentSemester/Create")]
        public IActionResult Create(CreateExamTimetable model)
        {
            try
            {
                if (User.IsInRole("Staff"))
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
                if (User.IsInRole("Staff"))
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
                if (User.IsInRole("Staff"))
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
