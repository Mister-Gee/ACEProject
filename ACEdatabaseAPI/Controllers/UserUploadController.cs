using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.Helpers;
using ACEdatabaseAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize("MIS")]
    public class UserUploadController : ControllerBase
    {
        IExcelHelper _excelHelper;
        public UserUploadController(IExcelHelper excelHelper)
        {
            _excelHelper = excelHelper;
        }

        [HttpPost]
        [Route("Staff/New")]
        public async Task<IActionResult> UploadNewStaff([FromForm]UploadFile model)
        {
            try
            {
                if (model.ExcelSheet != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        await model.ExcelSheet.CopyToAsync(stream);
                        var resp = _excelHelper.readStaffXLS(stream).Result;
                        return Ok(new { 
                            Success = resp.Item2,
                            Failed = resp.Item3
                        });
                    }
                }

                return BadRequest("Staff Excel Sheet required");
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), ex.ToString()));
            }
        }

        [HttpPost]
        [Route("Student/Returning/{ProgrammeID}/{LevelID}/{DepartmentID}")]
        public async Task<IActionResult> UploadReturningStudent(Guid ProgrammeID, Guid LevelID, Guid DepartmentID, [FromForm] UploadFile model)
        {
            try
            {
                if (model.ExcelSheet != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        await model.ExcelSheet.CopyToAsync(stream);
                        var resp = _excelHelper.readReturnStudentXLS(ProgrammeID, LevelID, DepartmentID, stream).Result;
                        return Ok(new
                        {
                            Success = resp.Item2,
                            Failed = resp.Item3
                        });
                    }
                }

                return BadRequest("Student Excel Sheet required");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), ex.ToString()));
            }
        }


        [HttpPost]
        [Route("Student/New/{ProgrammeID}/{LevelID}/{DepartmentID}")]
        public async Task<IActionResult> UploadNewStudent(Guid ProgrammeID, Guid LevelID, Guid DepartmentID, [FromForm] UploadFile model)
        {
            try
            {
                if (model.ExcelSheet != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        await model.ExcelSheet.CopyToAsync(stream);
                        var resp = _excelHelper.readNewStudentXLS(ProgrammeID, LevelID, DepartmentID, stream).Result;
                        return Ok(new
                        {
                            Success = resp.Item2,
                            Failed = resp.Item3
                        });
                    }
                }

                return BadRequest("Student Excel Sheet required");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), ex.ToString()));
            }
        }
    }
}
