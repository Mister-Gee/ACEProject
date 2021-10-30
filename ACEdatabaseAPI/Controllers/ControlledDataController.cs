﻿using ACE.Domain.Abstract.IControlledRepo;
using ACE.Domain.Entities.ControlledEntities;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.Model;
using Microsoft.AspNetCore.Http;
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
    public class ControlledDataController : ControllerBase
    {
        IReligionRepo _religionRepo;
        IMaritalStatusRepo _maritalStatusRepo;
        ILevelRepo _levelRepo;
        ISchoolRepo _schoolRepo;
        IDepartmentRepo _deptRepo;
        IGenderRepo _genderRepo;
        IProgrammeRepo _progRepo;
        IStudentCategoryRepo _studentCategoryRepo;
        IBloodGroupRepo _bloodGroupRepo;
        IGenotypeRepo _genotypeRepo;
        IFlagLevelRepo _flagLevelRepo;
        IMedicalConditionsRepo _medConRepo;



        public ControlledDataController(IReligionRepo religionRepo, IMaritalStatusRepo maritalStatusRepo, IStudentCategoryRepo studentCategoryRepo,
            ILevelRepo levelRepo, ISchoolRepo schoolRepo, IDepartmentRepo deptRepo, IGenderRepo genderRepo, IProgrammeRepo progRepo,
            IBloodGroupRepo bloodGroupRepo, IGenotypeRepo genotypeRepo, IFlagLevelRepo flagLevelRepo, IMedicalConditionsRepo medConRepo)
        {
            _religionRepo = religionRepo;
            _maritalStatusRepo = maritalStatusRepo;
            _levelRepo = levelRepo;
            _schoolRepo = schoolRepo;
            _deptRepo = deptRepo;
            _genderRepo = genderRepo;
            _progRepo = progRepo;
            _studentCategoryRepo = studentCategoryRepo;
            _bloodGroupRepo = bloodGroupRepo;
            _genotypeRepo = genotypeRepo;
            _flagLevelRepo = flagLevelRepo;
            _medConRepo = medConRepo;
        }

        [HttpGet]
        [Route("Religion/All")]
        public IActionResult GetAllReligion(){
            try
            {
                var result = _religionRepo.GetAll().ToList();
                return Ok(result);
            }
            catch(Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("Religion/Create/")]
        public IActionResult CreateReligion(CreateControlledData Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string username = User.Identity.Name;
                    var religion = new Religion()
                    {
                        Id = Guid.NewGuid(),
                        Name = Model.Name
                    };
                    _religionRepo.Add(religion);
                    _religionRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Religion Created Successfully"
                        }
                    );
                }
                return BadRequest();
             }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPut]
        [Route("Religion/Edit/{Id}")]
        public IActionResult EditReligion(Guid Id, CreateControlledData Model)
        {
            try
            {
                var religion = _religionRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if(religion != null)
                {
                    if (ModelState.IsValid)
                    {
                        string username = User.Identity.Name;
                        religion.Name = Model.Name;
                        _religionRepo.Edit(religion);
                        _religionRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(
                            new
                            {
                                Message = "Religion Edit Successfully"
                            }
                        );
                    }
                   return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                    HttpStatusCode.BadRequest.ToString(), "Name Field is Required"));
                }
               return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                    HttpStatusCode.BadRequest.ToString(), "Religion Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpDelete]
        [Route("Religion/Delete/{Id}")]
        public IActionResult DeleteReligion(Guid Id)
        {
            try
            {
                var religion = _religionRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (religion != null)
                {
                    string username = User.Identity.Name;
                    _religionRepo.Delete(religion);
                    _religionRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Religion Deleted Successfully"
                        }
                    );
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Religion Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }



        [HttpGet]
        [Route("StudentCategory/All")]
        public IActionResult GetAllStudentCategories()
        {
            try
            {
                var result = _studentCategoryRepo.GetAll().ToList();
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("StudentCategory/Create/")]
        public IActionResult CreateStudentCategory(CreateControlledData Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string username = User.Identity.Name;
                    var studentCategory = new StudentCategory()
                    {
                        Id = Guid.NewGuid(),
                        Name = Model.Name
                    };
                    _studentCategoryRepo.Add(studentCategory);
                    _studentCategoryRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Student Category Created Successfully"
                        }
                    );
                }
                return BadRequest();
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPut]
        [Route("StudentCategory/Edit/{Id}")]
        public IActionResult EditStudentCategory(Guid Id, CreateControlledData Model)
        {
            try
            {
                var studentCategory = _studentCategoryRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (studentCategory != null)
                {
                    if (ModelState.IsValid)
                    {
                        string username = User.Identity.Name;
                        studentCategory.Name = Model.Name;
                        _studentCategoryRepo.Edit(studentCategory);
                        _studentCategoryRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(
                            new
                            {
                                Message = "Student Category Edit Successfully"
                            }
                        );
                    }
                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Name Field is Required"));
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Student Category Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpDelete]
        [Route("StudentCategory/Delete/{Id}")]
        public IActionResult DeleteStudentCategory(Guid Id)
        {
            try
            {
                var religion = _religionRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (religion != null)
                {
                    string username = User.Identity.Name;
                    _religionRepo.Delete(religion);
                    _religionRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Student Category Deleted Successfully"
                        }
                    );
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Religion Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }


        [HttpGet]
        [Route("Gender/All")]
        public IActionResult GetAllGender()
        {
            try
            {
                var result = _genderRepo.GetAll().ToList();
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("Gender/Create/")]
        public IActionResult CreateGender(CreateControlledData Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string username = User.Identity.Name;
                    var gender = new Gender()
                    {
                        Name = Model.Name
                    };
                    _genderRepo.Add(gender);
                    _genderRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Gender Created Successfully"
                        }
                    );
                }
                return BadRequest();
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPut]
        [Route("Gender/Edit/{Id}")]
        public IActionResult EditGender(Guid Id, CreateControlledData Model)
        {
            try
            {
                var gender = _genderRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (gender != null)
                {
                    if (ModelState.IsValid)
                    {
                        string username = User.Identity.Name;
                        gender.Name = Model.Name;
                        _genderRepo.Edit(gender);
                        _religionRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(
                            new
                            {
                                Message = "Gender Edit Successfully"
                            }
                        );
                    }
                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Name Field is Required"));
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Gender Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpDelete]
        [Route("Gender/Delete/{Id}")]
        public IActionResult DeleteGender(Guid Id)
        {
            try
            {
                var gender = _genderRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (gender != null)
                {
                    string username = User.Identity.Name;
                    _genderRepo.Delete(gender);
                    _genderRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Gender Deleted Successfully"
                        }
                    );
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Gender Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }




        [HttpGet]
        [Route("MaritalStatus/All")]
        public IActionResult GetAllMaritalStatus()
        {
            try
            {
                var result = _maritalStatusRepo.GetAll().ToList();
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("MaritalStatus/Create/")]
        public IActionResult CreateMaritalStatus(CreateControlledData Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string username = User.Identity.Name;
                    var maritalStatus = new MaritalStatus()
                    {
                        Name = Model.Name
                    };
                    _maritalStatusRepo.Add(maritalStatus);
                    _maritalStatusRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Status Created Successfully"
                        }
                    );
                }
                return BadRequest();
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPut]
        [Route("MaritalStatus/Edit/{Id}")]
        public IActionResult EditMaritalStatus(Guid Id, CreateControlledData Model)
        {
            try
            {
                var maritalStatus = _maritalStatusRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (maritalStatus != null)
                {
                    if (ModelState.IsValid)
                    {
                        string username = User.Identity.Name;
                        maritalStatus.Name = Model.Name;
                        _maritalStatusRepo.Edit(maritalStatus);
                        _maritalStatusRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(
                            new
                            {
                                Message = "Status Edit Successfully"
                            }
                        );
                    }
                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Name Field is Required"));
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Marital Status Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpDelete]
        [Route("MaritalStatus/Delete/{Id}")]
        public IActionResult DeleteMaritalStatus(Guid Id)
        {
            try
            {
                var maritalStatus = _maritalStatusRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (maritalStatus != null)
                {
                    string username = User.Identity.Name;
                    _maritalStatusRepo.Delete(maritalStatus);
                    _maritalStatusRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Religion Deleted Successfully"
                        }
                    );
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Religion Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }




        [HttpGet]
        [Route("Level/All")]
        public IActionResult GetAllLevel()
        {
            try
            {
                var result = _levelRepo.GetAll().ToList();
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("Level/Create/")]
        public IActionResult CreateLevel(CreateControlledData Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string username = User.Identity.Name;
                    var level = new Level()
                    {
                        Name = Model.Name
                    };
                    _levelRepo.Add(level);
                    _levelRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Level Created Successfully"
                        }
                    );
                }
                return BadRequest();
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPut]
        [Route("Level/Edit/{Id}")]
        public IActionResult EditLevel(Guid Id, CreateControlledData Model)
        {
            try
            {
                var level = _levelRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (level != null)
                {
                    if (ModelState.IsValid)
                    {
                        string username = User.Identity.Name;
                        level.Name = Model.Name;
                        _levelRepo.Edit(level);
                        _levelRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(
                            new
                            {
                                Message = "Level Edit Successfully"
                            }
                        );
                    }
                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Name Field is Required"));
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Level Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpDelete]
        [Route("Level/Delete/{Id}")]
        public IActionResult DeleteLevel(Guid Id)
        {
            try
            {
                var level = _levelRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (level != null)
                {
                    string username = User.Identity.Name;
                    _levelRepo.Delete(level);
                    _levelRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Level Deleted Successfully"
                        }
                    );
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Level Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }



        [HttpGet]
        [Route("Programme/All")]
        public IActionResult GetAllProgramme()
        {
            try
            {
                var result = _progRepo.GetAll().ToList();
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("Programme/Create/")]
        public IActionResult CreateProgramme(CreateControlledData Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string username = User.Identity.Name;
                    var prog = new Programme()
                    {
                        Name = Model.Name
                    };
                    _progRepo.Add(prog);
                    _progRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Programme Created Successfully"
                        }
                    );
                }
                return BadRequest();
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPut]
        [Route("Programme/Edit/{Id}")]
        public IActionResult EditProgramme(Guid Id, CreateControlledData Model)
        {
            try
            {
                var prog = _progRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (prog != null)
                {
                    if (ModelState.IsValid)
                    {
                        string username = User.Identity.Name;
                        prog.Name = Model.Name;
                        _progRepo.Edit(prog);
                        _progRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(
                            new
                            {
                                Message = "Programme Edit Successfully"
                            }
                        );
                    }
                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Name Field is Required"));
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Programme Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpDelete]
        [Route("Programme/Delete/{Id}")]
        public IActionResult DeleteProgramme(Guid Id)
        {
            try
            {
                var prog = _progRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (prog != null)
                {
                    string username = User.Identity.Name;
                    _progRepo.Delete(prog);
                    _progRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Programe Deleted Successfully"
                        }
                    );
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Programme Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }



        [HttpGet]
        [Route("School/All")]
        public IActionResult GetAllSchool()
        {
            try
            {
                var result = _schoolRepo.GetAll().ToList();
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("School/Create/")]
        public IActionResult CreateSchool(CreateControlledData Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string username = User.Identity.Name;
                    var school = new School()
                    {
                        Name = Model.Name
                    };
                    _schoolRepo.Add(school);
                    _schoolRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "School Created Successfully"
                        }
                    );
                }
                return BadRequest();
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPut]
        [Route("School/Edit/{Id}")]
        public IActionResult EditSchool(Guid Id, CreateControlledData Model)
        {
            try
            {
                var school = _schoolRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (school != null)
                {
                    if (ModelState.IsValid)
                    {
                        string username = User.Identity.Name;
                        school.Name = Model.Name;
                        _schoolRepo.Edit(school);
                        _schoolRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(
                            new
                            {
                                Message = "School Edit Successfully"
                            }
                        );
                    }
                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Name Field is Required"));
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "School Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpDelete]
        [Route("School/Delete/{Id}")]
        public IActionResult DeleteSchool(Guid Id)
        {
            try
            {
                var school = _schoolRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (school != null)
                {
                    string username = User.Identity.Name;
                    _schoolRepo.Delete(school);
                    _schoolRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "School Deleted Successfully"
                        }
                    );
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "School Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }



        [HttpGet]
        [Route("Department/All")]
        public IActionResult GetAllDepartment()
        {
            try
            {
                var result = _deptRepo.GetAll().ToList();
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
        [Route("Department/{SchoolID}")]
        public IActionResult GetAllDepartmentInSchool(Guid SchoolID)
        {
            try
            {
                var result = _deptRepo.GetAll().Where(x => x.SchoolID == SchoolID).ToList();
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("Department/Create/")]
        public IActionResult CreateDepartment(CreateDept Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var school = _schoolRepo.FindBy(x => x.Id == Model.SchoolId).FirstOrDefault();
                    if(school != null)
                    {
                        string username = User.Identity.Name;
                        var dept = new Department()
                        {
                            Name = Model.Name,
                            SchoolID = Model.SchoolId
                        };
                        _deptRepo.Add(dept);
                        _deptRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(
                            new
                            {
                                Message = "Department Created Successfully"
                            }
                        );
                    }
                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "School Does Not Exist"));

                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "All Fields Are Required"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPut]
        [Route("Department/Edit/{Id}")]
        public IActionResult EditDepartment(Guid Id, CreateDept Model)
        {
            try
            {
                var dept = _deptRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (dept != null)
                {
                    if (ModelState.IsValid)
                    {
                        string username = User.Identity.Name;
                        dept.Name = Model.Name;
                        dept.SchoolID = Model.SchoolId;
                        _deptRepo.Edit(dept);
                        _deptRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(
                            new
                            {
                                Message = "Department Edit Successfully"
                            }
                        );
                    }
                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Name Field is Required"));
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Department Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpDelete]
        [Route("Department/Delete/{Id}")]
        public IActionResult DeleteDepartment(Guid Id)
        {
            try
            {
                var dept = _deptRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (dept != null)
                {
                    string username = User.Identity.Name;
                    _deptRepo.Delete(dept);
                    _deptRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Religion Deleted Successfully"
                        }
                    );
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Religion Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }


        [HttpGet]
        [Route("BloodGroup/All")]
        public IActionResult GetAllBloodGroup()
        {
            try
            {
                var result = _bloodGroupRepo.GetAll().ToList();
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("BloodGroup/Create/")]
        public IActionResult CreateBloodGroup(CreateControlledData Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string username = User.Identity.Name;
                    var bloodGroup = new BloodGroup()
                    {
                        Id = Guid.NewGuid(),
                        Name = Model.Name
                    };
                    _bloodGroupRepo.Add(bloodGroup);
                    _bloodGroupRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Blood Group Created Successfully"
                        }
                    );
                }
                return BadRequest();
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPut]
        [Route("BloodGroup/Edit/{Id}")]
        public IActionResult EditBloodGroup(Guid Id, CreateControlledData Model)
        {
            try
            {
                var bloodGroup = _bloodGroupRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (bloodGroup != null)
                {
                    if (ModelState.IsValid)
                    {
                        string username = User.Identity.Name;
                        bloodGroup.Name = Model.Name;
                        _bloodGroupRepo.Edit(bloodGroup);
                        _bloodGroupRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(
                            new
                            {
                                Message = "Blood Group Edit Successfully"
                            }
                        );
                    }
                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Name Field is Required"));
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Blood Group Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpDelete]
        [Route("BloodGroup/Delete/{Id}")]
        public IActionResult DeleteBloodGroup(Guid Id)
        {
            try
            {
                var bloodGroup = _bloodGroupRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (bloodGroup != null)
                {
                    string username = User.Identity.Name;
                    _bloodGroupRepo.Delete(bloodGroup);
                    _bloodGroupRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Blood Group Deleted Successfully"
                        }
                    );
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Blood Group Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }


        [HttpGet]
        [Route("Genotype/All")]
        public IActionResult GetAllGenotype()
        {
            try
            {
                var result = _genotypeRepo.GetAll().ToList();
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("Genotype/Create/")]
        public IActionResult CreateGenotype(CreateControlledData Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string username = User.Identity.Name;
                    var genotype = new Genotype()
                    {
                        Id = Guid.NewGuid(),
                        Name = Model.Name
                    };
                    _genotypeRepo.Add(genotype);
                    _genotypeRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Genotype Created Successfully"
                        }
                    );
                }
                return BadRequest();
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPut]
        [Route("Genotype/Edit/{Id}")]
        public IActionResult EditGenotype(Guid Id, CreateControlledData Model)
        {
            try
            {
                var genotype = _genotypeRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (genotype != null)
                {
                    if (ModelState.IsValid)
                    {
                        string username = User.Identity.Name;
                        genotype.Name = Model.Name;
                        _genotypeRepo.Edit(genotype);
                        _genotypeRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(
                            new
                            {
                                Message = "Genotype Edit Successfully"
                            }
                        );
                    }
                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Name Field is Required"));
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Genotype Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpDelete]
        [Route("Genotype/Delete/{Id}")]
        public IActionResult DeleteGenotype(Guid Id)
        {
            try
            {
                var genotype = _genotypeRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (genotype != null)
                {
                    string username = User.Identity.Name;
                    _genotypeRepo.Delete(genotype);
                    _genotypeRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Genotype Deleted Successfully"
                        }
                    );
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Genotype Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }



        [HttpGet]
        [Route("FlagLevel/All")]
        public IActionResult GetAllFlagLevel()
        {
            try
            {
                var result = _flagLevelRepo.GetAll().ToList();
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("FlagLevel/Create/")]
        public IActionResult CreateFlagLevel(CreateControlledData Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string username = User.Identity.Name;
                    var flagLevel = new FlagLevel()
                    {
                        Id = Guid.NewGuid(),
                        Name = Model.Name
                    };
                    _flagLevelRepo.Add(flagLevel);
                    _flagLevelRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Flag Level Created Successfully"
                        }
                    );
                }
                return BadRequest();
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPut]
        [Route("FlagLevel/Edit/{Id}")]
        public IActionResult EditFlagLevel(Guid Id, CreateControlledData Model)
        {
            try
            {
                var flagLevel = _flagLevelRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (flagLevel != null)
                {
                    if (ModelState.IsValid)
                    {
                        string username = User.Identity.Name;
                        flagLevel.Name = Model.Name;
                        _flagLevelRepo.Edit(flagLevel);
                        _flagLevelRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(
                            new
                            {
                                Message = "Flag Level Edit Successfully"
                            }
                        );
                    }
                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Name Field is Required"));
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Flag Level Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpDelete]
        [Route("FlagLevel/Delete/{Id}")]
        public IActionResult DeleteFlagLevel(Guid Id)
        {
            try
            {
                var flagLevel = _flagLevelRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (flagLevel != null)
                {
                    string username = User.Identity.Name;
                    _flagLevelRepo.Delete(flagLevel);
                    _flagLevelRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Flag Level Deleted Successfully"
                        }
                    );
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Flag Level Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }



        [HttpGet]
        [Route("MeedicalCondition/All")]
        public IActionResult GetAllMeedicalCondition()
        {
            try
            {
                var result = _medConRepo.GetAll().ToList();
                return Ok(result);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("MedicalCondition/Create/")]
        public IActionResult CreateMedicalCondition(CreateControlledData Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string username = User.Identity.Name;
                    var medCon = new MedicalCondition()
                    {
                        Id = Guid.NewGuid(),
                        Name = Model.Name
                    };
                    _medConRepo.Add(medCon);
                    _medConRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Medical Condition Created Successfully"
                        }
                    );
                }
                return BadRequest();
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPut]
        [Route("MedicalCondition/Edit/{Id}")]
        public IActionResult EditMedicalCondition(Guid Id, CreateControlledData Model)
        {
            try
            {
                var medCon = _medConRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (medCon != null)
                {
                    if (ModelState.IsValid)
                    {
                        string username = User.Identity.Name;
                        medCon.Name = Model.Name;
                        _medConRepo.Edit(medCon);
                        _medConRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(
                            new
                            {
                                Message = "Medical Condition Edit Successfully"
                            }
                        );
                    }
                    return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Name Field is Required"));
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Medical Condition Does Not Exist"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpDelete]
        [Route("MedicalConditin/Delete/{Id}")]
        public IActionResult DeleteMedicalConditin(Guid Id)
        {
            try
            {
                var medCon = _medConRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                if (medCon != null)
                {
                    string username = User.Identity.Name;
                    _medConRepo.Delete(medCon);
                    _medConRepo.Save(username, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(
                        new
                        {
                            Message = "Medical Condition Deleted Successfully"
                        }
                    );
                }
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
                                     HttpStatusCode.BadRequest.ToString(), "Medical Condition Does Not Exist"));
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
