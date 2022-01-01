﻿using ACE.Domain.Abstract;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Controllers
{
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        IDepartmentRepo _deptRepo;
        private readonly IMapper _mapper;
        IvStudentRepo _vStudentRepo;

        public StudentController(UserManager<ApplicationUser> userManager, IMapper mapper, ApplicationDbContext context, IDepartmentRepo deptRepo,
            IvStudentRepo vStudentRepo)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _deptRepo = deptRepo;
            _vStudentRepo = vStudentRepo;
        }



        [HttpGet]
        [ProducesResponseType(typeof(List<vStudent>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Get/All")]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var result = _vStudentRepo.FindBy(x => x.MatricNumber != null && x.Status == "Active").ToList();
                return Ok(result);
            }
            catch(Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApplicationUser), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Cafe/Get")]
        public async Task<IActionResult> CafeGetStudent(SearchByEmail model)
        {
            try
            {
                var result = _userManager.Users.Where(x => x.Email == model.Email).FirstOrDefault();
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
        [ProducesResponseType(typeof(vStudent), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Get/ID/{ID}")]
        public async Task<IActionResult> GetStudentByID(Guid ID)
        {
            try
            {
                var result = _vStudentRepo.FindBy(x => x.Id == ID.ToString()).FirstOrDefault();
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
        [ProducesResponseType(typeof(vStudent), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Get/Email")]
        public async Task<IActionResult> GetStudentByEmail(SearchByEmail model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Bad Request"));
                }

                var result = _vStudentRepo.FindBy(x => x.Email.ToUpper() == model.Email.ToUpper()).FirstOrDefault();
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
        [ProducesResponseType(typeof(vStudent), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Get/MatricNumber")]
        public async Task<IActionResult> GetStudentByMatricNumber(SearchByMatricNumber model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Bad Request"));
                }

                var result = _vStudentRepo.FindBy(x => x.MatricNumber.ToUpper() == model.MatricNumber.ToUpper()).FirstOrDefault();
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
        [ProducesResponseType(typeof(vStudent), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Get/Biometric")]
        public async Task<IActionResult> GetStudentByBiometric(SearchByBiometrics model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Bad Request"));
                }

                var student = _userManager.Users.Where(x => x.LeftThumbFingerBiometrics == model.Biometric || x.RightThumbFingerBiometrics == model.Biometric)
                    .FirstOrDefault();
                if(student == null)
                {
                    return NotFound(new { 
                        Message = "Student Not Found"
                    });
                }

                var result = _vStudentRepo.FindBy(x => x.Id == student.Id).FirstOrDefault();
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
        [ProducesResponseType(typeof(List<vStudent>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Department/Get/{DepartmentID}")]
        public async Task<IActionResult> GetStudentsInDepartMent(Guid DepartmentID)
        {
            try
            {
                var dept = _deptRepo.FindBy(x => x.Id == DepartmentID).FirstOrDefault();
                if(dept == null)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Department does not Exist"));
                }

                var result = _vStudentRepo.FindBy(x => x.DepartmentID == DepartmentID && x.MatricNumber != null && x.Status == "Active").ToList();
                
                return Ok(result);
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
