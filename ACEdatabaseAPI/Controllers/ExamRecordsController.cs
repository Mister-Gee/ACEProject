using ACE.Domain.Abstract;
using ACE.Domain.Abstract.IControlledRepo;
using ACE.Domain.Entities;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.Data;
using ACEdatabaseAPI.Helpers.ExamGradeService;
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
    [Route("[controller]")]
    [ApiController]
    public class ExamRecordsController : ControllerBase
    {
        IExamRecordsRepo _examRecordsRepo;
        IGradingUnitRepo _gradingUnitRepo;
        IAcademicYearRepo _acadYearRepo;
        ISemesterRepo _semesterRepo;
        ICourseRepo _courseRepo;
        private readonly IExamGradingService _examGradingService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExamRecordsController(IExamRecordsRepo examRecordsRepo, IGradingUnitRepo gradingUnitRepo, IAcademicYearRepo acadYearRepo,
        ISemesterRepo semesterRepo, ICourseRepo courseRepo, UserManager<ApplicationUser> userManager, IExamGradingService examGradingService)
        {
            _examRecordsRepo = examRecordsRepo;
            _gradingUnitRepo = gradingUnitRepo;
            _acadYearRepo = acadYearRepo;
            _semesterRepo = semesterRepo;
            _courseRepo = courseRepo;
            _userManager = userManager;
            _examGradingService = examGradingService;
        }

        [HttpGet]
        [Route("CurrentAcademicYear/CurrentSemester/Get/{StudentID}")]
        public IActionResult Get(Guid StudentID)
        {
            try
            {
                var currentSemester = _semesterRepo.GetAll().LastOrDefault();
                var currentAcademicYear = _acadYearRepo.GetAll().LastOrDefault();

                var result = _examRecordsRepo.GetAll().Where(x => x.StudentID == StudentID && x.AcademicYearID == currentAcademicYear.Id && x.SemesterID == currentSemester.Id);
                
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [Route("{AcademicYearID}/{SemesterID}/Get/{StudentID}")]
        public IActionResult Get(Guid StudentID, Guid AcademicYearID, Guid SemesterID)
        {
            try
            {
                var result = _examRecordsRepo.GetAll().Where(x => x.StudentID == StudentID && x.AcademicYearID == AcademicYearID && x.SemesterID == SemesterID);
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [Route("Get/All/{SchoolID}")]
        public IActionResult GetBySchool( Guid SchoolID)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var result = _examRecordsRepo.GetAll().Where(x => x.SchoolID == SchoolID);

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
        [Route("Get/All/{SchoolID}/{DepartmentID}")]
        public IActionResult GetBySchoolAndDepartment(Guid SchoolID, Guid DepartmentID)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var result = _examRecordsRepo.GetAll().Where(x => x.SchoolID == SchoolID && x.DepartmentID == DepartmentID);
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
        [Route("Record/{CourseID}")]
        public IActionResult Create(Guid CourseID, CreateExamRecords model)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var course = _courseRepo.FindBy(x => x.Id == CourseID).FirstOrDefault();
                    if(course == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                                                   new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                                       "Course Does Not Exist"));
                    }
                    var student = _userManager.FindByIdAsync(model.StudentID.ToString()).Result;
                    if(student == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                                                    new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                                        "Student Does Not Exist"));
                    }

                    if (_userManager.IsInRoleAsync(student, "Student").Result)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                                                    new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                                        "You Grade a non student"));
                    }
                    if (ModelState.IsValid)
                    {
                        if(student.DepartmentID == null || student.SchoolID == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                                                                                new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                                                                    "Student Not fully registered"));
                        }

                        double studentTotalScore = model.ExamScore + model.OtherAssessmentScore;
                        if(studentTotalScore > 100)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                                                    new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                                        "Student Total Score cannot be greater than 100"));
                        }
                        else if(studentTotalScore < 0)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                                                    new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                                        "Student Total Score cannot be less than 0"));
                        }

                        var gradeVM = _examGradingService.GetLetterGradeAndGradePoint(studentTotalScore);

                        var examRecord = new ExamRecords();
                        examRecord.AcademicYearID = course.AcademicYearID;
                        examRecord.SemesterID = course.SemesterID;
                        examRecord.StudentID = model.StudentID;
                        examRecord.CourseID = CourseID;
                        examRecord.CourseUnit = course.CourseUnit;
                        examRecord.SemesterID = course.SemesterID;
                        examRecord.SchoolID = (Guid)student.SchoolID;
                        examRecord.DepartmentID = (Guid)student.DepartmentID;
                        examRecord.OtherAssessmentScore = model.OtherAssessmentScore;
                        examRecord.ExamScore = model.ExamScore;
                        examRecord.TotalScore = studentTotalScore;
                        examRecord.LetterGrade = gradeVM.LetterGrade;
                        examRecord.GradePoint = gradeVM.GradePoint;

                        _examRecordsRepo.Add(examRecord);
                        _examRecordsRepo.Save(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new
                        {
                            Message = "Student Graded Successfully"
                        });
                    }
                    return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Request"));
                }

                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Access Denied"));
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
        public IActionResult Edit(Guid ID, EditExamRecords model)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    var examRecord = _examRecordsRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    if (examRecord == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                                                   new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                                       "Exam Record Does Not Exist"));
                    }

                    if (ModelState.IsValid)
                    {


                        double studentTotalScore = model.ExamScore + model.OtherAssessmentScore;
                        if (studentTotalScore > 100)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                                                    new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                                        "Student Total Score cannot be greater than 100"));
                        }
                        else if (studentTotalScore < 0)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                                                    new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                                        "Student Total Score cannot be less than 0"));
                        }

                        var gradeVM = _examGradingService.GetLetterGradeAndGradePoint(studentTotalScore);

                        examRecord.OtherAssessmentScore = model.OtherAssessmentScore;
                        examRecord.ExamScore = model.ExamScore;
                        examRecord.TotalScore = studentTotalScore;
                        examRecord.LetterGrade = gradeVM.LetterGrade;
                        examRecord.GradePoint = gradeVM.GradePoint;

                        _examRecordsRepo.Edit(examRecord);
                        _examRecordsRepo.Save(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new
                        {
                            Message = "Student Graded Successfully"
                        });
                    }
                }
                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Access Denied"));
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
        public IActionResult Delete(Guid ID)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    if (ModelState.IsValid)
                    {
                        var gradingUnit = _examRecordsRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                        if (gradingUnit == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                           new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                               "Exam Record does not exist"));
                        }



                        _examRecordsRepo.Delete(gradingUnit);
                        _examRecordsRepo.Save(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new
                        {
                            Message = "Exam Record Deleted"
                        });
                    }
                    return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Request"));
                }

                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Access Denied"));
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
