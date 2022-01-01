using ACE.Domain.Abstract;
using ACE.Domain.Entities;
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
    public class AnnoucementController : ControllerBase
    {
        IAnnoucementRepo _annoucementRepo;
        public AnnoucementController(IAnnoucementRepo annoucementRepo)
        {
            _annoucementRepo = annoucementRepo;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Annoucement>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
           StatusCodes.Status500InternalServerError)]
        [Route("Get/All")]
        public IActionResult Get()
        {
            try
            {
                var result = _annoucementRepo.GetAll().OrderByDescending(x => x.Date).ToList();
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
        [ProducesResponseType(typeof(Annoucement), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError),
           StatusCodes.Status500InternalServerError)]
        [Route("Get/{ID}")]
        public IActionResult Get(Guid ID)
        {
            try
            {
                var result = _annoucementRepo.FindBy(x => x.Id == ID).FirstOrDefault();
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
        [Route("Add")]
        public IActionResult Add(AddAnnoucement model)
        {
            try
            {
                if (User.IsInRole("MIS"))
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest();
                    }
                    var annoucement = new Annoucement();
                    annoucement.Body = model.Body;
                    annoucement.Date = DateTime.UtcNow.AddHours(1);
                    annoucement.PostedBy = User.Identity.Name;
                    annoucement.Title = model.Title;

                    _annoucementRepo.Add(annoucement);
                    _annoucementRepo.Save(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(new { 
                        Message = "Annoucement Added"
                    });
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

        [HttpPut]
        [Route("Edit/{ID}")]
        public IActionResult Edit(Guid ID, AddAnnoucement model)
        {
            try
            {
                if (User.IsInRole("MIS"))
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest();
                    }
                    var annoucement = _annoucementRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    if(annoucement == null)
                    {
                        return BadRequest(new { 
                            Message = "Annoucement Does Not Exist"
                        });
                    }

                    annoucement.Body = model.Body;
                    annoucement.Date = DateTime.UtcNow.AddHours(1);
                    annoucement.PostedBy = User.Identity.Name;
                    annoucement.Title = model.Title;

                    _annoucementRepo.Edit(annoucement);
                    _annoucementRepo.Save(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(new
                    {
                        Message = "Annoucement Updated"
                    });
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

        [HttpDelete]
        [Route("Delete/{ID}")]
        public IActionResult Delete(Guid ID)
        {
            try
            {
                if (User.IsInRole("MIS"))
                {
                    var annoucement = _annoucementRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    if (annoucement == null)
                    {
                        return BadRequest(new
                        {
                            Message = "Annoucement Does Not Exist"
                        });
                    }

                   
                    _annoucementRepo.Delete(annoucement);
                    _annoucementRepo.Save(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(new
                    {
                        Message = "Annoucement Deleted"
                    });
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
    }
}
