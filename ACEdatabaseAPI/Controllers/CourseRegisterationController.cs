using ACE.Domain.Abstract;
using ACE.Domain.Abstract.IControlledRepo;
using ACE.Domain.Entities;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.Data;
using ACEdatabaseAPI.DTOModel;
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
    public class CourseRegisterationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        IStudentRegCourseRepo _courseRegItemRepo;
        ICourseRegisterationRepo _courseRegRepo;
        IAcademicYearRepo _acadYearRepo;
        ICourseRepo _courseRepo;
        IvCourseRepo _vCourseRepo;
        ISemesterRepo _semesterRepo;
        ICurrentAcademicSessionRepo _currentAcadRepo;
        IvCourseRegisterationRepo _vCourseRegRepo;
        IvStudentRegisteredCourseRepo _vCourseRegItemRepo;
        IDepartmentRepo _deptRepo;
        public CourseRegisterationController(IStudentRegCourseRepo courseRegItemRepo, 
            IAcademicYearRepo acadYearRepo, ICourseRepo courseRepo, 
            ISemesterRepo semesterRepo, UserManager<ApplicationUser> userManager,
            IvCourseRegisterationRepo vCourseRegRepo, ICurrentAcademicSessionRepo currentAcadRepo, IDepartmentRepo deptRepo,
            ICourseRegisterationRepo courseRegRepo, IvStudentRegisteredCourseRepo vCourseRegItemRepo, IvCourseRepo vCourseRepo)
        {
            _userManager = userManager;
            _courseRegItemRepo = courseRegItemRepo;
            _acadYearRepo = acadYearRepo;
            _deptRepo = deptRepo;
            _courseRepo = courseRepo;
            _vCourseRepo = vCourseRepo;
            _semesterRepo = semesterRepo;
            _vCourseRegRepo = vCourseRegRepo;
            _currentAcadRepo = currentAcadRepo;
            _courseRegRepo = courseRegRepo;
            _vCourseRegItemRepo = vCourseRegItemRepo;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<vCourseRegisteration>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("All")]
        public IActionResult StudentRegisteredAllCourse()
        {
            try
            {
                if (User.IsInRole("MIS"))
                {
                    var result = _vCourseRegRepo.GetAll().ToList();
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
        [ProducesResponseType(typeof(List<vCourseRegisteration>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Student/CourseRegisterationHistory/All/{StudentID}")]
        public IActionResult StudentRegisteredCourseHistory(Guid StudentID)
        {
            try
            {
                if (User.IsInRole("MIS") || User.IsInRole("Student"))
                {
                    var result = _vCourseRegRepo.GetAll().Where(x => x.StudentID == StudentID).ToList();
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
        [ProducesResponseType(typeof(List<vStudentRegisteredCourse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Student/RegisteredCourses/Detail/{ID}")]
        public IActionResult StudentRegisteredCourseDetail(Guid ID)
        {
            try
            {
                if (User.IsInRole("MIS") || User.IsInRole("Student"))
                {
                    var result = _vCourseRegItemRepo.GetAll().Where(x => x.CourseRegisterationID == ID).ToList();
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
        [ProducesResponseType(typeof(List<vStudentRegisteredCourse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Student/CurrentAcademicYear/{StudentID}")]
        public IActionResult StudentRegisteredCourse(Guid StudentID)
        {
            try
            {
                if (User.IsInRole("MIS") || User.IsInRole("Student"))
                {
                    var currentAcadYear = _currentAcadRepo.GetAll().FirstOrDefault();
                    var result = _vCourseRegItemRepo.GetAll().Where(x => x.AcademicYearID == currentAcadYear.AcademicYearID && x.StudentId == StudentID).ToList();
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
        [ProducesResponseType(typeof(List<vStudentRegisteredCourse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Student/CurrentAcademicYear/CurrentSemester/{StudentID}")]
        public IActionResult CurrentRegisteredCourses(Guid StudentID)
        {
            try
            {
                if (User.IsInRole("MIS") || User.IsInRole("Student"))
                {
                    var currentAcadYear = _currentAcadRepo.GetAll().FirstOrDefault();
                    var result = _vCourseRegItemRepo.GetAll().Where(x => x.AcademicYearID == currentAcadYear.AcademicYearID && x.SemesterID == currentAcadYear.SemesterID && x.StudentId == StudentID).ToList();
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
        [ProducesResponseType(typeof(List<vStudentRegisteredCourse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Student/CurrentAcademicYear/All/{StudentID}/{SemesterID}")]
        public IActionResult StudentRegisteredCourseBySemester(Guid StudentID, Guid SemesterID)
        {
            try
            {
                if (User.IsInRole("MIS") || User.IsInRole("Student"))
                {
                    var currentAcadYear = _currentAcadRepo.GetAll().FirstOrDefault();
                    var result = _vCourseRegItemRepo.GetAll().Where(x => x.AcademicYearID == currentAcadYear.AcademicYearID && x.SemesterID == SemesterID && x.StudentId == StudentID).ToList();
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
        [ProducesResponseType(typeof(List<StudentNameDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Student/Registered/All/{CourseID}")]
        public IActionResult RegisteredStudent(Guid CourseID)
        {
            try
            {
                if (User.IsInRole("MIS") || User.IsInRole("Lecturer") || User.IsInRole("Exam&Records"))
                {
                    List<StudentNameDTO> studentDetails = new List<StudentNameDTO>();
                    var currentAcadYear = _currentAcadRepo.GetAll().FirstOrDefault();
                    var studentIDs = _courseRegItemRepo.FindBy(x => x.CourseID == CourseID
                        && x.AcademicYearID == currentAcadYear.AcademicYearID && x.SemesterID == currentAcadYear.SemesterID).Select(x => x.StudentId).ToList();
                    foreach (var id in studentIDs)
                    {
                        var student = _userManager.FindByIdAsync(id.ToString()).Result;
                        var studentDetail = new StudentNameDTO();
                        studentDetail.Id = id;
                        studentDetail.Name = student.FirstName + " " + student.LastName;
                        studentDetail.MatricNumber = student.MatricNumber;
                        studentDetails.Add(studentDetail);
                    }
                    return Ok(studentDetails);

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
        [ProducesResponseType(typeof(List<CourseEligibleStudentByDepartmentDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Student/Department/Registered/All/{CourseID}")]
        public IActionResult RegisteredStudentByDepartment(Guid CourseID)
        {
            try
            {
                if (User.IsInRole("MIS") || User.IsInRole("Lecturer") || User.IsInRole("Exam&Records"))
                {
                    List<CourseEligibleStudentByDepartmentDTO> result = new List<CourseEligibleStudentByDepartmentDTO>();

                    var currentAcadYear = _currentAcadRepo.GetAll().FirstOrDefault();
                    var course = _vCourseRepo.FindBy(x => x.Id == CourseID).FirstOrDefault();

                    if(course == null)
                    {
                        return BadRequest(new { 
                            Message = "Course Doesn't Exist"
                        });
                    }

                    if (course.isDepartmental)
                    {
                        var eligibleStudent = new CourseEligibleStudentByDepartmentDTO();
                        eligibleStudent.Department = course.Department;
                       
                        var studentIDs = _vCourseRegItemRepo.FindBy(x => x.CourseID == CourseID && x.StudentDepartmentId == course.DepartmentID
                        && x.AcademicYearID == currentAcadYear.AcademicYearID && x.SemesterID == currentAcadYear.SemesterID).Select(x => x.StudentId).ToList();
                        List<StudentNameDTO> studentDetails = new List<StudentNameDTO>();
                        foreach (var id in studentIDs)
                        {
                            var student = _userManager.FindByIdAsync(id.ToString()).Result;
                            var studentDetail = new StudentNameDTO();
                            studentDetail.Id = id;
                            studentDetail.Name = student.FirstName + " " + student.LastName;
                            studentDetail.MatricNumber = student.MatricNumber;
                            studentDetails.Add(studentDetail);
                        }
                        eligibleStudent.Students = studentDetails;
                        result.Add(eligibleStudent);
                        return Ok(result);
                    }

                    if (course.isGeneral)
                    {
                        var depts = _deptRepo.GetAll().ToList();
                        foreach(var dept in depts)
                        {
                            var eligibleStudent = new CourseEligibleStudentByDepartmentDTO();
                            eligibleStudent.Department = dept.Name;
                            var studentIDs = _vCourseRegItemRepo.FindBy(x => x.CourseID == CourseID && x.StudentDepartmentId == dept.Id
                            && x.AcademicYearID == currentAcadYear.AcademicYearID && x.SemesterID == currentAcadYear.SemesterID).Select(x => x.StudentId).ToList();

                            List<StudentNameDTO> studentDetails = new List<StudentNameDTO>();
                            foreach (var id in studentIDs)
                            {
                                var student = _userManager.FindByIdAsync(id.ToString()).Result;
                                var studentDetail = new StudentNameDTO();
                                studentDetail.Id = id;
                                studentDetail.Name = student.FirstName + " " + student.LastName;
                                studentDetail.MatricNumber = student.MatricNumber;
                                studentDetails.Add(studentDetail);
                            }

                            eligibleStudent.Students = studentDetails;
                            result.Add(eligibleStudent);
                        }
                        return Ok(result);
                    }
                    else
                    {
                        var eligibleDepts = JsonConvert.DeserializeObject<List<Guid>>(course.EligibleDepartments);
                        foreach (var deptID in eligibleDepts)
                        {
                            var eligibleStudent = new CourseEligibleStudentByDepartmentDTO();
                            var dept = _deptRepo.FindBy(x => x.Id == deptID).FirstOrDefault();
                            eligibleStudent.Department = dept.Name;
                            var studentIDs = _vCourseRegItemRepo.FindBy(x => x.CourseID == CourseID && x.StudentDepartmentId == deptID
                            && x.AcademicYearID == currentAcadYear.AcademicYearID && x.SemesterID == currentAcadYear.SemesterID).Select(x => x.StudentId).ToList();

                            List<StudentNameDTO> studentDetails = new List<StudentNameDTO>();
                            foreach (var id in studentIDs)
                            {
                                var student = _userManager.FindByIdAsync(id.ToString()).Result;
                                var studentDetail = new StudentNameDTO();
                                studentDetail.Id = id;
                                studentDetail.Name = student.FirstName + " " + student.LastName;
                                studentDetail.MatricNumber = student.MatricNumber;
                                studentDetails.Add(studentDetail);
                            }

                            eligibleStudent.Students = studentDetails;
                            result.Add(eligibleStudent);
                        }
                        return Ok(result);
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

        [HttpGet]
        [ProducesResponseType(typeof(List<StudentNameDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Student/Registered/{AcademicYearID}/{CourseID}")]
        public IActionResult RegisteredStudent(Guid CourseID, Guid AcademicYearID)
        {
            try
            {
                if (User.IsInRole("MIS") || User.IsInRole("Lecturer"))
                {
                    List<StudentNameDTO> studentDetails = new List<StudentNameDTO>();
                    var studentIDs = _courseRegItemRepo.FindBy(x => x.CourseID == CourseID 
                        && x.AcademicYearID == AcademicYearID).Select(x => x.StudentId).ToList();
                    foreach(var id in studentIDs)
                    {
                        var student = _userManager.FindByIdAsync(id.ToString()).Result;
                        var studentDetail = new StudentNameDTO();
                        studentDetail.Id = id;
                        studentDetail.Name = student.FirstName + " " + student.LastName;
                        studentDetail.MatricNumber = student.MatricNumber;
                        studentDetails.Add(studentDetail);
                    }
                    return Ok(studentDetails);

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
        [ProducesResponseType(typeof(List<vCourseRegisteration>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Student/Pack/{StudentID}")]
        public IActionResult StudentRegisteredCourseBySemesterAndAcademicYear(Guid StudentID, [FromQuery] Guid? AcademicYearID, [FromQuery] Guid? SemesterID)
        {
            try
            {
                if (User.IsInRole("MIS") || User.IsInRole("Student"))
                {
                    var currentAcadYear = _currentAcadRepo.GetAll().FirstOrDefault();
                    var session = AcademicYearID ?? currentAcadYear.AcademicYearID;
                    var semester = SemesterID ?? currentAcadYear.SemesterID;

                    var result = _vCourseRegRepo.GetAll().Where(x => x.StudentID == StudentID && x.AcademicYearID == session && x.SemesterID == semester).ToList();
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
        [ProducesResponseType(typeof(List<vCourseRegisteration>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Student/List/{StudentID}")]
        public IActionResult StudentRegisteredCourseListBySemesterAndAcademicYear(Guid StudentID, [FromQuery] Guid? AcademicYearID, [FromQuery] Guid? SemesterID)
        {
            try
            {
                if (User.IsInRole("MIS") || User.IsInRole("Student"))
                {
                    var currentAcadYear = _currentAcadRepo.GetAll().FirstOrDefault();
                    var session = AcademicYearID ?? currentAcadYear.AcademicYearID;
                    var semester = SemesterID ?? currentAcadYear.SemesterID;

                    var result = _vCourseRegItemRepo.GetAll().Where(x => x.StudentId == StudentID && x.AcademicYearID == session && x.SemesterID == semester).ToList();
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
                if (User.IsInRole("MIS") || User.IsInRole("Student"))
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
                            var currentAcadYear = _currentAcadRepo.GetAll().FirstOrDefault();
                            if(currentAcadYear.AcademicYearID != model.AcademicYearID)
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

                        var student = _userManager.FindByIdAsync(model.StudentId.ToString()).Result;
                        if (student == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Student does not exist"));
                        }

                        if (student.Status.ToUpper() != "ACTIVE")
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                                new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Student is no Longer Active"));
                        }

                        int totalUnit = 0;
                        int totalCourses = 0;

                        Guid CourseRegID = Guid.NewGuid();
                        var courseReg = new CourseRegisteration();
                        courseReg.Id = CourseRegID;
                        courseReg.AcademicYearID = model.AcademicYearID;
                        courseReg.RegDate = DateTime.UtcNow.AddHours(1);
                        courseReg.SemesterID = model.SemesterID;
                        courseReg.StudentID = model.StudentId;

                        foreach (var item in model.Courses)
                        {
                            var course = _courseRepo.FindBy(x => x.Id == item.CourseID).FirstOrDefault();
                            if (course == null)
                            {
                                return StatusCode((int)HttpStatusCode.Unauthorized,
                                new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                    "Invalid Course"));
                            }
                            totalCourses += 1;
                            totalUnit += course.CourseUnit;

                            var courseRegItem = new StudentRegisteredCourse();
                            courseRegItem.AcademicYearID = model.AcademicYearID;
                            courseRegItem.CourseID = item.CourseID;
                            courseRegItem.CourseRegisterationDate = DateTime.UtcNow.AddHours(1);
                            courseRegItem.CourseRegisterationID = CourseRegID;
                            courseRegItem.CourseUnit = course.CourseUnit;
                            courseRegItem.SemesterID = model.SemesterID;
                            courseRegItem.StudentId = model.StudentId;

                            _courseRegItemRepo.Add(courseRegItem);
                            _courseRegItemRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());

                        }

                        courseReg.Courses = totalCourses;
                        courseReg.TotalUnit = totalUnit;
                        _courseRegRepo.Add(courseReg);
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
        public IActionResult EditRegisterCourse(Guid ID, EditRegisteredCourse model)
        {
            try
            {
                if (User.IsInRole("MIS"))
                {
                    var user = User.Identity.Name;
                    var registeredCourse = _courseRegRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    
                    if(registeredCourse == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Registered Course Does Not Exist"));
                    }

                    int totalCourse = registeredCourse.Courses;
                    int totalUnits = registeredCourse.TotalUnit;

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

                        var student = _userManager.FindByIdAsync(model.StudentId.ToString()).Result;
                        if (student == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Student does not exist"));
                        }

                        if (student.Status.ToUpper() != "ACTIVE")
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                                new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Student is no Longer Active"));
                        }
                        

                        foreach(var item in model.Courses)
                        {
                            if(item.Id != null)
                            {
                                var courseRegItem = _courseRegItemRepo.FindBy(x => x.Id == item.Id).FirstOrDefault();
                                totalUnits -= courseRegItem.CourseUnit;
                                var course = _courseRepo.FindBy(x => x.Id == item.CourseID).FirstOrDefault();

                                if (courseRegItem != null)
                                {
                                    courseRegItem.CourseID = item.CourseID;
                                    courseRegItem.CourseUnit = course.CourseUnit;
                                    courseRegItem.CourseRegisterationDate = DateTime.UtcNow.AddHours(1);

                                    _courseRegItemRepo.Edit(courseRegItem);
                                    _courseRegItemRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());

                                    totalUnits += course.CourseUnit;
                                }
                                else
                                {
                                    var newCourseRegItem = new StudentRegisteredCourse();
                                    newCourseRegItem.AcademicYearID = registeredCourse.AcademicYearID;
                                    newCourseRegItem.CourseID = item.CourseID;
                                    newCourseRegItem.CourseRegisterationDate = DateTime.UtcNow.AddHours(1);
                                    newCourseRegItem.CourseRegisterationID = registeredCourse.Id;
                                    newCourseRegItem.CourseUnit = course.CourseUnit;
                                    newCourseRegItem.SemesterID = registeredCourse.SemesterID;
                                    newCourseRegItem.StudentId = registeredCourse.StudentID;

                                    _courseRegItemRepo.Add(newCourseRegItem);
                                    _courseRegItemRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());

                                    totalUnits += course.CourseUnit;
                                    totalCourse += 1;
                                }
                            }
                        }

                        registeredCourse.TotalUnit = totalUnits;
                        registeredCourse.Courses = totalCourse;
                        registeredCourse.RegDate = DateTime.UtcNow.AddHours(1);


                        _courseRegRepo.Edit(registeredCourse);
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
                if (User.IsInRole("MIS"))
                {
                    var user = User.Identity.Name;
                    var result = _courseRegRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    if (result == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Course Does Not Exist"));
                    }

                    var items = _courseRegItemRepo.FindBy(x => x.CourseRegisterationID == ID).ToList();
                    foreach(var item in items)
                    {
                        _courseRegItemRepo.Delete(item);
                        _courseRegItemRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
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
