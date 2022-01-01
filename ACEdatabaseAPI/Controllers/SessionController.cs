using ACE.Domain.Abstract;
using ACE.Domain.Abstract.IControlledRepo;
using ACE.Domain.Entities;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.DTOModel;
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
    public class SessionController : ControllerBase
    {
        ICurrentAcademicSessionRepo _currentSessionRepo;
        IAcademicYearRepo _acadRepo;
        ISemesterRepo _semesterRepo;
        public SessionController(ICurrentAcademicSessionRepo currentSessionRepo, IAcademicYearRepo acadRepo, ISemesterRepo semesterRepo)
        {
            _currentSessionRepo = currentSessionRepo;
            _acadRepo = acadRepo;
            _semesterRepo = semesterRepo;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AcademicSessionDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
            StatusCodes.Status500InternalServerError)]
        [Route("Current")]
        public IActionResult CurrentSession()
        {
            try
            {
                var result = _currentSessionRepo.GetAll().FirstOrDefault();
                if(result == null)
                {
                    return NoContent();
                }
                var acad = _acadRepo.FindBy(x => x.Id == result.AcademicYearID).FirstOrDefault();
                var semester = _semesterRepo.FindBy(x => x.Id == result.SemesterID).FirstOrDefault();

                var resultDTO = new AcademicSessionDTO();
                resultDTO.AcademicYear = acad.Name;
                resultDTO.AcademicYearID = result.AcademicYearID;
                resultDTO.Date = result.Date;
                resultDTO.Id = result.Id;
                resultDTO.Semester = semester.Name;
                resultDTO.SemesterID = result.SemesterID;

                return Ok(resultDTO);
            }
            catch(Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError .ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("Semester/New")]
        public IActionResult NewSessionSemester(UpdateSession model)
        {
            try
            {
                if (User.IsInRole("MIS"))
                {
                    var result = _currentSessionRepo.GetAll().ToList();
                    if (result.Count < 1)
                    {
                        var newSession = new CurrentAcademicSession();
                        newSession.SemesterID = model.SemesterID;
                        newSession.AcademicYearID = model.AcademicYearID;
                        newSession.Date = DateTime.UtcNow.AddHours(1);

                        _currentSessionRepo.Add(newSession);
                        _currentSessionRepo.Save(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
                    }
                    else
                    {
                        var currentSession = result.FirstOrDefault();
                        currentSession.SemesterID = model.SemesterID;
                        currentSession.AcademicYearID = model.AcademicYearID;
                        currentSession.Date = DateTime.UtcNow.AddHours(1);

                        _currentSessionRepo.Edit(currentSession);
                        _currentSessionRepo.Save(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
                    }

                    return Ok(new { 
                        Message = "Session and Semester Updated"
                    });
                }
                return Unauthorized(new { 
                    Message = "Unauthorized Access"
                });
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
