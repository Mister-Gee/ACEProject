using ACE.Domain.Abstract.IControlledRepo;
using ACEdatabaseAPI.Data;
using ACEdatabaseAPI.DTOModel;
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


        public BiodataController(UserManager<ApplicationUser> userManager, IReligionRepo religionRepo, IMaritalStatusRepo maritalStatusRepo,
            ILevelRepo levelRepo, ISchoolRepo schoolRepo, IDepartmentRepo deptRepo, IGenderRepo genderRepo, IProgrammeRepo progRepo, IMapper mapper,
            IStudentCategoryRepo studentCategoryRepo)
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
        }

        [HttpPut]
        [Route("/Student/Update/{Id}")]
        public async Task<IActionResult> UpdateStudentData(Guid Id, StudentBioData model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var student = _userManager.FindByIdAsync(Id.ToString()).Result;

                    bool isStudent = _userManager.IsInRoleAsync(student, "Student").Result;
                    if (isStudent)
                    {
                        var gender = _genderRepo.FindBy(x => x.Id == model.Gender).FirstOrDefault();
                        var prog = _progRepo.FindBy(x => x.Id == model.Programme).FirstOrDefault();
                        var maritalStatus = _maritalStatusRepo.FindBy(x => x.Id == model.MaritalStatus).FirstOrDefault();
                        var cLevel = _levelRepo.FindBy(x => x.Id == model.CurrentLevel).FirstOrDefault();
                        var eLevel = _levelRepo.FindBy(x => x.Id == model.EntryLevel).FirstOrDefault();
                        var school = _schoolRepo.FindBy(x => x.Id == model.School).FirstOrDefault();
                        var dept = _deptRepo.FindBy(x => x.Id == model.Department).FirstOrDefault();
                        var religion = _religionRepo.FindBy(x => x.Id == model.Department).FirstOrDefault();


                        if (gender == null)
                        {
                            return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "Gender Does Not Exist"));
                        }

                        if (religion == null)
                        {
                            return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "Religion Does Not Exist"));
                        }

                        if (prog == null)
                        {
                            return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "Programme Does Not Exist"));
                        }

                        if (maritalStatus == null)
                        {
                            return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "Programme Does Not Exist"));
                        }

                        if (cLevel == null)
                        {
                            return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "Current Level Does Not Exist"));
                        }

                        if (eLevel == null)
                        {
                            return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "Entry Level Does Not Exist"));
                        }

                        if (school == null)
                        {
                            return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "School Does Not Exist"));
                        }

                        if (dept != null)
                        {
                            if (dept.SchoolID != model.School)
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

                        var updatedStudent = _mapper.Map(model, student);
                        if (updatedStudent != null)
                        {
                            var result = await _userManager.UpdateAsync(updatedStudent);
                            if (result.Succeeded)
                            {
                                return Ok(new
                                {
                                    Message = "Student Data Updated Successfuly"
                                }); ;
                            }
                            return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                           HttpStatusCode.BadRequest.ToString(), "Error Saving Data"));
                        }
                        return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                           HttpStatusCode.BadRequest.ToString(), "An Error Occured"));
                    }
                    else
                    {
                        return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "You are not a student"));
                    }
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


        [HttpPut]
        [Route("/Staff/Update/{Id}")]
        public async Task<IActionResult> UpdateStaffData(Guid Id, StaffBioData model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var staff = _userManager.FindByIdAsync(Id.ToString()).Result;

                    bool isStaff = _userManager.IsInRoleAsync(staff, "Staff").Result;
                    if (isStaff)
                    {
                        var gender = _genderRepo.FindBy(x => x.Id == model.Gender).FirstOrDefault();
                        var maritalStatus = _maritalStatusRepo.FindBy(x => x.Id == model.MaritalStatus).FirstOrDefault();
                        var school = _schoolRepo.FindBy(x => x.Id == model.School).FirstOrDefault();
                        var dept = _deptRepo.FindBy(x => x.Id == model.Department).FirstOrDefault();
                        var religion = _religionRepo.FindBy(x => x.Id == model.Department).FirstOrDefault();


                        if (gender == null)
                        {
                            return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "Gender Does Not Exist"));
                        }

                        if (religion == null)
                        {
                            return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "Religion Does Not Exist"));
                        }


                        if (maritalStatus == null)
                        {
                            return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "Programme Does Not Exist"));
                        }

                        if (school == null)
                        {
                            return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "School Does Not Exist"));
                        }

                        if (dept != null)
                        {
                            if (dept.SchoolID != model.School)
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

                        var updatedStaff = _mapper.Map(model, staff);
                        if (updatedStaff != null)
                        {
                            var result = await _userManager.UpdateAsync(updatedStaff);
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
                        return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                           HttpStatusCode.BadRequest.ToString(), "An Error Occured"));
                    }
                    else
                    {
                        return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                            HttpStatusCode.BadRequest.ToString(), "You are not a Staff"));
                    }
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
    }
}
