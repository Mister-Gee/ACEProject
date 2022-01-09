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
        IvExamRecordsRepo _vExamRecordsRepo;
        ICurrentAcademicSessionRepo _currentAcadYearRepo;
        private readonly IExamGradingService _examGradingService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExamRecordsController(IExamRecordsRepo examRecordsRepo, IGradingUnitRepo gradingUnitRepo, IAcademicYearRepo acadYearRepo,
        ISemesterRepo semesterRepo, ICourseRepo courseRepo, UserManager<ApplicationUser> userManager, IExamGradingService examGradingService,
        IvExamRecordsRepo vExamRecordsRepo, ICurrentAcademicSessionRepo currentAcadYearRepo)
        {
            _examRecordsRepo = examRecordsRepo;
            _gradingUnitRepo = gradingUnitRepo;
            _acadYearRepo = acadYearRepo;
            _semesterRepo = semesterRepo;
            _courseRepo = courseRepo;
            _userManager = userManager;
            _examGradingService = examGradingService;
            _vExamRecordsRepo = vExamRecordsRepo;
            _currentAcadYearRepo = currentAcadYearRepo;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<vExamRecords>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Student/Records/Get")]
        public IActionResult Get()
        {
            try
            {
                if (User.IsInRole("Student"))
                {
                    var currentAcadYear = _currentAcadYearRepo.GetAll().FirstOrDefault();
                    var student = _userManager.FindByNameAsync(User.Identity.Name).Result;
                    var result = _vExamRecordsRepo.GetAll().Where(x => x.StudentID == Guid.Parse(student.Id)
                        && x.AcademicYearID == currentAcadYear.AcademicYearID && x.SemesterID == currentAcadYear.SemesterID).ToList();

                    return Ok(result);
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

        [HttpGet]
        [ProducesResponseType(typeof(List<vExamRecords>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("CurrentAcademicYear/CurrentSemester/Get/{StudentID}")]
        public IActionResult Get(Guid StudentID)
        {
            try
            {
                if(User.IsInRole("Student") || User.IsInRole("Exam&Records"))
                {
                    var currentAcadYear = _currentAcadYearRepo.GetAll().FirstOrDefault();
                    var result = _vExamRecordsRepo.GetAll().Where(x => x.StudentID == StudentID
                        && x.AcademicYearID == currentAcadYear.AcademicYearID && x.SemesterID == currentAcadYear.SemesterID).ToList();

                    return Ok(result);
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

        [HttpGet]
        [ProducesResponseType(typeof(List<vExamRecords>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("{AcademicYearID}/{SemesterID}/Get/{StudentID}")]
        public IActionResult Get(Guid StudentID, Guid AcademicYearID, Guid SemesterID)
        {
            try
            {
                if(User.IsInRole("Student") || User.IsInRole("Exam&Records"))
                {
                    var result = _vExamRecordsRepo.GetAll().Where(x => x.StudentID == StudentID
                    && x.AcademicYearID == AcademicYearID && x.SemesterID == SemesterID).ToList();
                    return Ok(result);
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

        [HttpGet]
        [ProducesResponseType(typeof(List<vExamRecords>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Get/All/{SchoolID}")]
        public IActionResult GetBySchool( Guid SchoolID)
        {
            try
            {
                if (User.IsInRole("Exam&Records"))
                {
                    var result = _vExamRecordsRepo.GetAll().Where(x => x.SchoolID == SchoolID).ToList();

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
        [ProducesResponseType(typeof(List<vExamRecords>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
           StatusCodes.Status500InternalServerError)]
        [Route("{AcademicYearID}/{SemesterID}/Get/All/{SchoolID}")]
        public IActionResult GetBySchool(Guid SchoolID, Guid AcademicYearID, Guid SemesterID)
        {
            try
            {
                if (User.IsInRole("Exam&Records"))
                {
                    var result = _vExamRecordsRepo.GetAll().Where(x => x.SchoolID == SchoolID 
                        && x.SemesterID == SemesterID && x.AcademicYearID == AcademicYearID).ToList();

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
        [ProducesResponseType(typeof(List<vExamRecords>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
           StatusCodes.Status500InternalServerError)]
        [Route("CurrentAcademicYear/Get/All/{SchoolID}")]
        public IActionResult GetCurrentYearBySchool(Guid SchoolID)
        {
            try
            {
                if (User.IsInRole("Exam&Records"))
                {
                    var currentAcadYear = _currentAcadYearRepo.GetAll().FirstOrDefault();
                    var result = _vExamRecordsRepo.GetAll().Where(x => x.SchoolID == SchoolID
                        && x.SemesterID == currentAcadYear.SemesterID && x.AcademicYearID == currentAcadYear.AcademicYearID).ToList();

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
        [ProducesResponseType(typeof(List<vExamRecords>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
           StatusCodes.Status500InternalServerError)]
        [Route("Get/All/{DepartmentID}")]
        public IActionResult GetByDepartment(Guid DepartmentID)
        {
            try
            {
                if (User.IsInRole("Exam&Records"))
                {
                    var result = _vExamRecordsRepo.GetAll().Where(x => x.DepartmentID == DepartmentID).ToList();
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
        [ProducesResponseType(typeof(List<vExamRecords>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
           StatusCodes.Status500InternalServerError)]
        [Route("CurrentAcademicYear/Get/All/{DepartmentID}")]
        public IActionResult GetCurrentYearByDepartment(Guid DepartmentID)
        {
            try
            {
                if (User.IsInRole("Exam&Records"))
                {
                    var currenAcadYear = _currentAcadYearRepo.GetAll().FirstOrDefault();
                    var result = _vExamRecordsRepo.GetAll().Where(x => x.DepartmentID == DepartmentID 
                        && x.AcademicYearID == currenAcadYear.AcademicYearID && x.SemesterID == currenAcadYear.SemesterID ).ToList();
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
        [ProducesResponseType(typeof(List<vExamRecords>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
           StatusCodes.Status500InternalServerError)]
        [Route("{AcademicYearID}/{SemesterID}/Get/All/{DepartmentID}")]
        public IActionResult GetByDepartment(Guid DepartmentID, Guid AcademicYearID, Guid SemesterID)
        {
            try
            {
                if (User.IsInRole("Exam&Records"))
                {
                    var result = _vExamRecordsRepo.GetAll().Where(x => x.DepartmentID == DepartmentID
                        && x.AcademicYearID == AcademicYearID && x.SemesterID == SemesterID).ToList();
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
                if (User.IsInRole("Exam&Records"))
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
                        //examRecord.AcademicYearID = course.AcademicYearID;
                        //examRecord.SemesterID = course.SemesterID;
                        examRecord.StudentID = model.StudentID;
                        examRecord.CourseID = CourseID;
                        examRecord.CourseUnit = course.CourseUnit;
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
                if (User.IsInRole("Exam&Records"))
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
                if (User.IsInRole("Exam&Records"))
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
