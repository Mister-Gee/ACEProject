using ACE.Domain.Abstract;
using ACE.Domain.Abstract.IControlledRepo;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.Data;
using ACEdatabaseAPI.DTOModel;
using ACEdatabaseAPI.Helpers.FileUploadService;
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
    public class BiodataController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        IReligionRepo _religionRepo;
        IMaritalStatusRepo _maritalStatusRepo;
        ILevelRepo _levelRepo;
        ISchoolRepo _schoolRepo;
        IDepartmentRepo _deptRepo;
        IGenderRepo _genderRepo;
        IProgrammeRepo _progRepo;
        IStudentCategoryRepo _studentCategoryRepo;
        ICourseRegisterationRepo _courseRegRepo;
        ICurrentAcademicSessionRepo _currentSession;
        IMedicalRecordRepo _medRepo;
        private readonly IFileUploadService _fileUploadService;


        public BiodataController(UserManager<ApplicationUser> userManager, IReligionRepo religionRepo, IMaritalStatusRepo maritalStatusRepo,
            ILevelRepo levelRepo, ISchoolRepo schoolRepo, IDepartmentRepo deptRepo, IGenderRepo genderRepo, IProgrammeRepo progRepo, IMapper mapper,
            IStudentCategoryRepo studentCategoryRepo, IFileUploadService fileUploadService, ICourseRegisterationRepo courseRegRepo, ICurrentAcademicSessionRepo currentSession, IMedicalRecordRepo medRepo)
        {
            _userManager = userManager;
            _religionRepo = religionRepo;
            _maritalStatusRepo = maritalStatusRepo;
            _levelRepo = levelRepo;
            _schoolRepo = schoolRepo;
            _deptRepo = deptRepo;
            _genderRepo = genderRepo;
            _progRepo = progRepo;
            _studentCategoryRepo = studentCategoryRepo;
            _mapper = mapper;
            _fileUploadService = fileUploadService;
            _courseRegRepo = courseRegRepo;
            _currentSession = currentSession;
            _medRepo = medRepo;
        }

        [HttpPut]
        [Route("Student/Update/{Id}")]
        public async Task<IActionResult> UpdateStudentData(Guid Id, StudentBioData model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (User.IsInRole("Cafe") || User.IsInRole("MIS"))
                    {
                        var student = _userManager.FindByIdAsync(Id.ToString()).Result;

                        bool isStudent = _userManager.IsInRoleAsync(student, "Student").Result;
                        if (isStudent)
                        {
                            if(model.GenderID != null)
                            {
                                var gender = _genderRepo.FindBy(x => x.Id == model.GenderID).FirstOrDefault();
                                if (gender == null)
                                {
                                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                    HttpStatusCode.BadRequest.ToString(), "Gender Does Not Exist"));
                                }
                                student.GenderID = model.GenderID;
                            }

                            if(model.ProgrammeID != null)
                            {
                                var prog = _progRepo.FindBy(x => x.Id == model.ProgrammeID).FirstOrDefault();
                                if (prog == null)
                                {
                                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                    HttpStatusCode.BadRequest.ToString(), "Programme Does Not Exist"));
                                }
                                student.ProgrammeID = model.ProgrammeID;

                            }

                            if (model.MaritalStatusID != null)
                            {
                                var maritalStatus = _maritalStatusRepo.FindBy(x => x.Id == model.MaritalStatusID).FirstOrDefault();
                                if (maritalStatus == null)
                                {
                                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                    HttpStatusCode.BadRequest.ToString(), "Programme Does Not Exist"));
                                }
                                student.MaritalStatusID = model.MaritalStatusID;
                            }

                            if (model.CurrentLevelID != null)
                            {
                                var cLevel = _levelRepo.FindBy(x => x.Id == model.CurrentLevelID).FirstOrDefault();
                                if (cLevel == null)
                                {
                                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                    HttpStatusCode.BadRequest.ToString(), "Current Level Does Not Exist"));
                                }
                                student.CurrentLevelID = model.CurrentLevelID;
                            }

                            if(model.EntryLevelID != null)
                            {
                                var eLevel = _levelRepo.FindBy(x => x.Id == model.EntryLevelID).FirstOrDefault();
                                if (eLevel == null)
                                {
                                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                    HttpStatusCode.BadRequest.ToString(), "Entry Level Does Not Exist"));
                                }
                                student.EntryLevelID = model.EntryLevelID;
                            }

                            if (model.SchoolID != null && model.DepartmentID != null)
                            {
                                var school = _schoolRepo.FindBy(x => x.Id == model.SchoolID).FirstOrDefault();
                                var dept = _deptRepo.FindBy(x => x.Id == model.DepartmentID).FirstOrDefault();

                                if (school == null)
                                {
                                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                    HttpStatusCode.BadRequest.ToString(), "School Does Not Exist"));
                                }

                                if (dept != null)
                                {
                                    if (dept.SchoolID != model.SchoolID)
                                    {
                                        return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                    HttpStatusCode.BadRequest.ToString(), "Department is not in School"));
                                    }
                                }
                                else
                                {
                                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                    HttpStatusCode.BadRequest.ToString(), "Department Does Not Exist"));
                                }
                                student.SchoolID = model.SchoolID;
                                student.DepartmentID = model.DepartmentID;

                            }

                            if (model.ReligionID != null)
                            {
                                var religion = _religionRepo.FindBy(x => x.Id == model.ReligionID).FirstOrDefault();
                                if (religion == null)
                                {
                                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                    HttpStatusCode.BadRequest.ToString(), "Religion Does Not Exist"));
                                }
                                student.ReligionID = model.ReligionID;

                            }

                            if (model.StudentCategoryID != null)
                            {
                                var studentCategory = _studentCategoryRepo.FindBy(x => x.Id == model.StudentCategoryID).FirstOrDefault();
                                if (studentCategory == null)
                                {
                                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                    HttpStatusCode.BadRequest.ToString(), "Student Category Does Not Exist"));
                                }
                                student.StudentCategoryID = model.StudentCategoryID;
                            }

                            student.FormerName = model.FormerName;
                            student.Address = model.Address;
                            student.AdmissionDate = model.AdmissionDate.Value;
                            student.AlternatePhoneNumber = model.AlternatePhoneNumber;
                            student.DateOfBirth = model.DateOfBirth;
                            student.Disability = model.Disability;
                            student.FacebookID = model.FacebookID;
                            student.Hometown = model.Hometown;
                            student.InstagramID = model.InstagramID;
                            student.isDisabled = model.isDisabled;
                            student.isIndigenous = model.isIndigenous;
                            student.JambRegNumber = model.JambRegNumber;
                            student.LG = model.LG;
                            student.LinkedInID = model.LinkedInID;
                            student.MatricNumber = model.MatricNumber;
                            student.ModeOfAdmission = model.ModeOfAdmission;
                            student.Nationality = model.Nationality;
                            student.NIN = model.NIN;
                            student.OtherName = model.OtherName;
                            student.StateOfOrigin = model.StateOfOrigin;
                            student.TwitterID = model.TwitterID;
                            student.ZipPostalCode = model.ZipPostalCode;

                            var result = await _userManager.UpdateAsync(student);
                            if (result.Succeeded)
                            {
                                return Ok(new
                                {
                                    Message = "Student Data Updated Successfuly"
                                });
                            }
                            return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "Error Saving Data"));
                        }
                        else
                        {
                            return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                HttpStatusCode.BadRequest.ToString(), "User is not a student"));
                        }
                    }
                    return Unauthorized();
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "Invalid Field"));
            }
            catch(Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                   new ApiError((int)HttpStatusCode.InternalServerError,
                       HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [Route("Registeration/Progress/")]
        public async Task<IActionResult> RegProgress()
        {
            try
            {
                if (User.IsInRole("Student"))
                {
                    var regProgress = new RegProgressDTO();
                    int totalRegCount = 5;
                    int userRegCount = 0;
                    var usr = await _userManager.FindByNameAsync(User.Identity.Name);

                    if (usr.LeftThumbFingerBiometrics != null && usr.RightThumbFingerBiometrics != null)
                    {
                        userRegCount += 1;
                        regProgress.Biometrics = true;
                    }

                    if (usr.GenderID != null)
                    {
                        userRegCount += 1;
                        regProgress.Biodata = true;
                    }

                    if (usr.UserImageURL != null)
                    {
                        userRegCount += 1;
                        regProgress.UserImage = true;
                    }

                    var currentAcadSession = _currentSession.GetAll().FirstOrDefault();
                    var courseReg = _courseRegRepo.FindBy(x => x.StudentID == Guid.Parse(usr.Id) && x.SemesterID == currentAcadSession.SemesterID && x.AcademicYearID == currentAcadSession.AcademicYearID).FirstOrDefault();
                    if (courseReg != null)
                    {
                        userRegCount += 1;
                        regProgress.CourseReg = true;
                    }

                    var medRecord = _medRepo.FindBy(x => x.UserId == Guid.Parse(usr.Id)).FirstOrDefault();
                    if (medRecord != null)
                    {
                        userRegCount += 1;
                        regProgress.Medical = true;
                    }

                    int regPercent = (userRegCount / totalRegCount) * 100;
                    regProgress.ProgressPercent = regPercent;
                    regProgress.TotalRegProcess = totalRegCount;
                    regProgress.CompletedRegProcess = userRegCount;
                    return Ok(regProgress);
                }
                return Unauthorized();
            }
            catch(Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                   new ApiError((int)HttpStatusCode.InternalServerError,
                       HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }


        [HttpPut]
        [Route("Staff/Update/{Id}")]
        public async Task<IActionResult> UpdateStaffData(Guid Id, StaffBioData model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (User.IsInRole("Cafe") || User.IsInRole("MIS"))
                    {
                        var staff = _userManager.FindByIdAsync(Id.ToString()).Result;

                        bool isStaff = _userManager.IsInRoleAsync(staff, "Staff").Result;
                        if (isStaff)
                        {
                            if (model.GenderID != null)
                            {
                                var gender = _genderRepo.FindBy(x => x.Id == model.GenderID).FirstOrDefault();
                                if (gender == null)
                                {
                                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                    HttpStatusCode.BadRequest.ToString(), "Gender Does Not Exist"));
                                }
                                staff.GenderID = model.GenderID;
                            }

                            if (model.MaritalStatusID != null)
                            {
                                var maritalStatus = _maritalStatusRepo.FindBy(x => x.Id == model.MaritalStatusID).FirstOrDefault();
                                if (maritalStatus == null)
                                {
                                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                    HttpStatusCode.BadRequest.ToString(), "Programme Does Not Exist"));
                                }
                                staff.MaritalStatusID = model.MaritalStatusID;
                            }


                            if (model.SchoolID != null && model.DepartmentID != null)
                            {
                                var school = _schoolRepo.FindBy(x => x.Id == model.SchoolID).FirstOrDefault();
                                var dept = _deptRepo.FindBy(x => x.Id == model.DepartmentID).FirstOrDefault();

                                if (school == null)
                                {
                                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                    HttpStatusCode.BadRequest.ToString(), "School Does Not Exist"));
                                }

                                if (dept != null)
                                {
                                    if (dept.SchoolID != model.SchoolID)
                                    {
                                        return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                    HttpStatusCode.BadRequest.ToString(), "Department is not in School"));
                                    }
                                }
                                else
                                {
                                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                    HttpStatusCode.BadRequest.ToString(), "Department Does Not Exist"));
                                }
                                staff.SchoolID = model.SchoolID;
                                staff.DepartmentID = model.DepartmentID;

                            }

                            if (model.ReligionID != null)
                            {
                                var religion = _religionRepo.FindBy(x => x.Id == model.ReligionID).FirstOrDefault();
                                if (religion == null)
                                {
                                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                    HttpStatusCode.BadRequest.ToString(), "Religion Does Not Exist"));
                                }
                                staff.ReligionID = model.ReligionID;

                            }

                            staff.FormerName = model.FormerName;
                            staff.Address = model.Address;
                            staff.AlternatePhoneNumber = model.AlternatePhoneNumber;
                            staff.DateOfBirth = model.DateOfBirth;
                            staff.Disability = model.Disability;
                            staff.FacebookID = model.FacebookID;
                            staff.Hometown = model.Hometown;
                            staff.InstagramID = model.InstagramID;
                            staff.isDisabled = model.isDisabled;
                            staff.isIndigenous = model.isIndigenous;
                            staff.LG = model.LG;
                            staff.LinkedInID = model.LinkedInID;
                            staff.StaffID = model.StaffID;
                            staff.IPPISNo = model.IPPISNo;
                            staff.Nationality = model.Nationality;
                            staff.NIN = model.NIN;
                            staff.OtherName = model.OtherName;
                            staff.StateOfOrigin = model.StateOfOrigin;
                            staff.TwitterID = model.TwitterID;
                            staff.ZipPostalCode = model.ZipPostalCode;
                            staff.EmploymentDate = model.EmploymentDate.Value;

                            var result = await _userManager.UpdateAsync(staff);
                            if (result.Succeeded)
                                {
                                    return Ok(new
                                    {
                                        Message = "Staff Data Updated Successfuly"
                                    }); ;
                                }
                                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                               HttpStatusCode.BadRequest.ToString(), "Error Saving Data"));
                        }
                        else
                        {
                            return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                HttpStatusCode.BadRequest.ToString(), "User is not a Staff"));
                        }
                    }
                    return Unauthorized();
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "Invalid Field"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                   new ApiError((int)HttpStatusCode.InternalServerError,
                       HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPut]
        [Route("Image/Upload/{userID}")]
        public async Task<IActionResult> ImageUpload(Guid userID, [FromForm]ImageUpload model)
        {
            try
            {
                if(userID.ToString() != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (User.IsInRole("Cafe") || User.IsInRole("MIS"))
                        {
                            var user = await _userManager.FindByIdAsync(userID.ToString());
                            if (user == null)
                            {
                                user = await _userManager.FindByNameAsync(userID.ToString());
                                if (user != null)
                                {
                                    var resp = await _fileUploadService.AddPhotoAsync(model.Image, user.Id);
                                    user.UserImageURL = resp.SecureUrl.AbsoluteUri;
                                    user.UserImageData = resp.Bytes;

                                    await _userManager.UpdateAsync(user);
                                }
                                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                HttpStatusCode.BadRequest.ToString(), "User Not Found"));
                            }
                        }
                        return Unauthorized();
                    }
                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "Image Field is Empty"));
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "User ID cant be null"));
            }
            catch(Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                   new ApiError((int)HttpStatusCode.InternalServerError,
                       HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [Route("Biometrics/Status/{UserID}")]
        public async Task<IActionResult> BiometricStatus(Guid UserID)
        {
            try
            {
                if(User.IsInRole("MIS") || User.IsInRole("Medical") || User.IsInRole("Exams&Records") || User.IsInRole("Security"))
                {
                    var user = await _userManager.FindByIdAsync(UserID.ToString());
                    if((user.LeftThumbFingerBiometrics != null) && (user.RightThumbFingerBiometrics != null))
                    {
                        return Ok(new { 
                            IsBiometricEnrolled = true
                        });
                    }
                    return Ok(new
                    {
                        IsBiometricEnrolled = false
                    });
                }
                return BadRequest(new ApiError(StatusCodes.Status401Unauthorized,
                                            HttpStatusCode.BadRequest.ToString(), "Unauthorized Access"));
            }
            catch(Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                   new ApiError((int)HttpStatusCode.InternalServerError,
                       HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }
        
        [HttpPut]
        [Route("Biometrics/RightThumb/{userID}")]
        public async Task<IActionResult> BiometricRight(Guid userID, BiometricUpload model)
        {
            try
            {
                if (userID.ToString() != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (User.IsInRole("Cafe") || User.IsInRole("MIS"))
                        {
                            var user = await _userManager.FindByIdAsync(userID.ToString());
                            if (user == null)
                            {
                                user = await _userManager.FindByNameAsync(userID.ToString());
                                if (user != null)
                                {

                                    user.RightThumbFingerBiometrics = model.Biometric;
                                    await _userManager.UpdateAsync(user);
                                }
                                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                HttpStatusCode.BadRequest.ToString(), "User Not Found"));
                            }
                        }
                        return Unauthorized();
                    }
                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "Image Field is Empty"));
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "User ID cant be null"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                   new ApiError((int)HttpStatusCode.InternalServerError,
                       HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPut]
        [Route("Biometrics/LeftThumb/{userID}")]
        public async Task<IActionResult> BiometricLeft(Guid userID, BiometricUpload model)
        {
            try
            {
                if (userID.ToString() != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (User.IsInRole("Cafe") || User.IsInRole("MIS"))
                        {
                            var user = await _userManager.FindByIdAsync(userID.ToString());
                            if (user == null)
                            {
                                user = await _userManager.FindByNameAsync(userID.ToString());
                                if (user != null)
                                {

                                    user.LeftThumbFingerBiometrics = model.Biometric;
                                    await _userManager.UpdateAsync(user);
                                }
                                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                                HttpStatusCode.BadRequest.ToString(), "User Not Found"));
                            }
                        }
                        return Unauthorized();
                    }
                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "Image Field is Empty"));
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "User ID cant be null"));
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
