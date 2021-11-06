using ACE.Domain.Abstract.IControlledRepo;
using ACE.Domain.Entities.ControlledEntities;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.DTOModel;
using ACEdatabaseAPI.Model;
using Microsoft.AspNetCore.Http;
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
    public class GradingUnitController : ControllerBase
    {
        IGradingUnitRepo _gradingUnit;
        public GradingUnitController(IGradingUnitRepo gradingUnit)
        {
            _gradingUnit = gradingUnit;
        }

        [HttpGet]
        [Route("All")]
        public IActionResult Get()
        {
            try
            {
                var gradingUnit = _gradingUnit.GetAll().ToList();
                List<GradingUnitDTO> gradedto = new List<GradingUnitDTO>();
                foreach(var unit in gradingUnit)
                {
                    var gradingunitDTO = new GradingUnitDTO();
                    var scoreRangeArray = JsonConvert.DeserializeObject<List<int>>(unit.ScoreRange);
                    gradingunitDTO.StartingScore = scoreRangeArray.FirstOrDefault();
                    gradingunitDTO.EndingScore = scoreRangeArray.LastOrDefault();
                    gradingunitDTO.GradePoint = unit.GradePoint;
                    gradingunitDTO.LetterGrade = unit.LetterGrade;

                    gradedto.Add(gradingunitDTO);
                }
                return Ok(gradedto);
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(CreateGradingUnit model)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    if (ModelState.IsValid)
                    {
                        var gradingUnit = new GradingUnit();
                        int startingScore = model.StartingScore;
                        int rawRange = model.EndingScore - model.StartingScore;
                        int range = rawRange + 1;
                        var scoreRange = Enumerable.Range(startingScore, range).ToList();
                        gradingUnit.ScoreRange = JsonConvert.SerializeObject(scoreRange);
                        gradingUnit.LetterGrade = model.LetterGrade;
                        gradingUnit.GradePoint = model.GradePoint;

                        _gradingUnit.Add(gradingUnit);
                        _gradingUnit.Save(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new { 
                            Message = "Grading Unit Created"
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
        public IActionResult Edit(Guid ID, CreateGradingUnit model)
        {
            try
            {
                if (User.IsInRole("Staff"))
                {
                    if (ModelState.IsValid)
                    {
                        var gradingUnit = _gradingUnit.FindBy(x => x.ID == ID).FirstOrDefault();
                        if(gradingUnit == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                           new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                               "Grading Unit does not exist"));
                        }

                        int startingScore = model.StartingScore;
                        int rawRange = model.EndingScore - model.StartingScore;
                        int range = rawRange + 1;
                        var scoreRange = Enumerable.Range(startingScore, range).ToList();
                        gradingUnit.ScoreRange = JsonConvert.SerializeObject(scoreRange);
                        gradingUnit.LetterGrade = model.LetterGrade;
                        gradingUnit.GradePoint = model.GradePoint;

                        _gradingUnit.Edit(gradingUnit);
                        _gradingUnit.Save(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new
                        {
                            Message = "Grading Unit Updated"
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
                        var gradingUnit = _gradingUnit.FindBy(x => x.ID == ID).FirstOrDefault();
                        if (gradingUnit == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                           new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                               "Grading Unit does not exist"));
                        }

                        

                        _gradingUnit.Delete(gradingUnit);
                        _gradingUnit.Save(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new
                        {
                            Message = "Grading Unit Deleted"
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
