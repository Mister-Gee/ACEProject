using ACE.Domain.Abstract;
using ACE.Domain.Entities;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.Data;
using ACEdatabaseAPI.Model;
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
    public class HealthCenterController : ControllerBase
    {
        IMedicalRecordRepo _medRepo;
        IMedicalHistoryRepo _medHistoryRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public HealthCenterController(IMedicalRecordRepo medRepo, IMedicalHistoryRepo medHistoryRepo, UserManager<ApplicationUser> userManager)
        {
            _medRepo = medRepo;
            _medHistoryRepo = medHistoryRepo;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Records/All")]
        public IActionResult AllMedicalData()
        {
            try
            {
                if (User.IsInRole("Health"))
                {
                    var result = _medRepo.GetAll().ToList();
                    return Ok(result);
                }
                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Unauthorized Access"));
            }
            catch(Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiError((int)HttpStatusCode.InternalServerError,
                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpGet]
        [Route("Records/Student/All")]
        public IActionResult AllStudentMedicalData()
        {
            try
            {
                if (User.IsInRole("Health"))
                {
                    var result = _medRepo.GetAll().Where(x => !String.IsNullOrEmpty(x.MatricNumber)).ToList();
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
        [Route("Records/Staff/All")]
        public IActionResult AllStaffMedicalData()
        {
            try
            {
                if (User.IsInRole("Health"))
                {
                    var result = _medRepo.GetAll().Where(x => !String.IsNullOrEmpty(x.StaffID)).ToList();
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
        [Route("Records/{ID}")]
        public IActionResult RecordByID(Guid ID)
        {
            try
            {
                if (User.IsInRole("Health"))
                {
                    var result = _medRepo.FindBy(x => x.Id == ID).FirstOrDefault();
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
        [Route("Records/Status/{UserID}")]
        public IActionResult HealthRecordStatus(Guid UserID)
        {
            try
            {
                if (User.IsInRole("Health") || User.IsInRole("MIS") || User.IsInRole("Exam&Records") || User.IsInRole("Security"))
                {
                    var result = _medRepo.FindBy(x => x.UserId == UserID).FirstOrDefault();
                    if(result == null)
                    {
                        return Ok(new { 
                            IsCompleted = false
                        });
                    }
                    return Ok(new
                    {
                        IsCompleted = true
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

        [HttpGet]
        [Route("Records/User/{UserID}")]
        public IActionResult RecordByUserID(Guid UserID)
        {
            try
            {
                if (User.IsInRole("Health"))
                {
                    var result = _medRepo.FindBy(x => x.UserId == UserID).FirstOrDefault();
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
        [Route("MedicalHistory/{UserID}")]
        public IActionResult MedicalHistoryByUserID(Guid UserID)
        {
            try
            {
                if (User.IsInRole("Health"))
                {
                    var result = _medHistoryRepo.FindBy(x => x.UserID == UserID).OrderByDescending(x => x.Date).ToList();
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
        [Route("Records/MatricNumber")]
        public IActionResult RecordByMatricNumber(SearchByMatricNumber model)
        {
            try
            {
                if (User.IsInRole("Health"))
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Bad Request"));
                    }

                    var result = _medRepo.FindBy(x => x.MatricNumber == model.MatricNumber).FirstOrDefault();
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
        [Route("Diagnosis/Add")]
        public IActionResult AddDiagnosis(AddMedicalHistory model)
        {
            try
            {
                if (User.IsInRole("Health"))
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Bad Request"));
                    }
                    var user = User.Identity.Name;
                    var medicalHistory = new MedicalHistory();

                    var patient = _userManager.FindByIdAsync(model.UserID.ToString()).Result;
                    if (patient == null)
                    {
                        return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "User does not exist"));
                    }

                    medicalHistory.AdditionDoctorsNote = model.AdditionDoctorsNote;
                    medicalHistory.Date = DateTime.UtcNow.AddHours(1);
                    medicalHistory.Description = model.Description;
                    medicalHistory.Doctor = User.Identity.Name;
                    medicalHistory.FinalDiagnosis = model.FinalDiagnosis;
                    medicalHistory.InitialDiagnosis = model.InitialDiagnosis;
                    medicalHistory.TreatmentPlan = model.TreatmentPlan;
                    medicalHistory.UserID = model.UserID;
                    medicalHistory.VitalSign = model.VitalSign;

                    _medHistoryRepo.Add(medicalHistory);
                    _medHistoryRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());

                    return Ok(new
                    {
                        Message = "Medical History Added Successfully"
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

        [HttpPost]
        [Route("Records/StaffID")]
        public IActionResult RecordByStaffID(SearchByStaffID model)
        {
            try
            {
                if (User.IsInRole("Health"))
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Bad Request"));
                    }

                    var result = _medRepo.FindBy(x => x.StaffID == model.StaffID).FirstOrDefault();
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
        [Route("Records/Student/Add")]
        public IActionResult AddStudentMedicalRecord(CreateStudentMedicalRecord model)
        {
            try
            {
                if (User.IsInRole("Health"))
                {
                    if (ModelState.IsValid)
                    {
                        var user = User.Identity.Name;
                        var medicalRecord = new MedicalRecord();
                        medicalRecord.UserId = model.UserId;
                        medicalRecord.MatricNumber = model.MatricNumber;
                        medicalRecord.BloodGroupID = model.BloodGroupID;
                        medicalRecord.FamilyDoctorName = model.FamilyDoctorName;
                        medicalRecord.FamilyDoctorPhoneNumber = model.FamilyDoctorPhoneNumber;
                        medicalRecord.GenotypeID = model.GenotypeID;
                        medicalRecord.Height = model.Height;
                        medicalRecord.AdditionalNote = model.AdditionalNote;
                        medicalRecord.Weight = model.Weight;

                        _medRepo.Add(medicalRecord);
                        _medRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new
                        {
                            Message = "Student Medical Record Added Successfully"
                        });
                    }
                    return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Unauthorized Access"));
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
        [Route("Records/Staff/Add")]
        public IActionResult AddStaffMedicalRecord(CreateStaffMedicalRecord model)
        {
            try
            {
                if (User.IsInRole("Health"))
                {
                    if (ModelState.IsValid)
                    {
                        var user = User.Identity.Name;
                        var medicalRecord = new MedicalRecord();
                        medicalRecord.UserId = model.UserId;
                        medicalRecord.StaffID = model.StaffID;
                        medicalRecord.BloodGroupID = model.BloodGroupID;
                        medicalRecord.FamilyDoctorName = model.FamilyDoctorName;
                        medicalRecord.FamilyDoctorPhoneNumber = model.FamilyDoctorPhoneNumber;
                        medicalRecord.GenotypeID = model.GenotypeID;
                        medicalRecord.Height = model.Height;
                        medicalRecord.AdditionalNote = model.AdditionalNote;
                        medicalRecord.Weight = model.Weight;
                        _medRepo.Add(medicalRecord);
                        _medRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new
                        {
                            Message = "Staff Medical Record Added Successfully"
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

        [HttpPost]
        [Route("Records/Student/Edit/{ID}")]
        public IActionResult EditStudentMedicalRecord(Guid ID, CreateStudentMedicalRecord model)
        {
            try
            {
                if (User.IsInRole("Health"))
                {
                    if(ID == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Request"));
                    }
                    if (ModelState.IsValid)
                    {
                        var studentRecord = _medRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                        if(studentRecord == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                                                        new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                                            "Student Medical record does not Exist!"));
                        }

                        var user = User.Identity.Name;
                        studentRecord.UserId = model.UserId;
                        studentRecord.MatricNumber = model.MatricNumber;
                        studentRecord.BloodGroupID = model.BloodGroupID;
                        studentRecord.FamilyDoctorName = model.FamilyDoctorName;
                        studentRecord.FamilyDoctorPhoneNumber = model.FamilyDoctorPhoneNumber;
                        studentRecord.GenotypeID = model.GenotypeID;
                        studentRecord.Height = model.Height;
                        studentRecord.AdditionalNote = model.AdditionalNote;
                        studentRecord.Weight = model.Weight;
                        _medRepo.Edit(studentRecord);
                        _medRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new
                        {
                            Message = "Student Medical Record Updated Successfully"
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
        [Route("Records/Staff/Edit/{ID}")]
        public IActionResult EditStaffMedicalRecord(Guid ID, CreateStaffMedicalRecord model)
        {
            try
            {
                if (User.IsInRole("Health"))
                {
                    if (ID == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid Request"));
                    }
                    if (ModelState.IsValid)
                    {
                        var staffRecord = _medRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                        if (staffRecord == null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                                                        new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                                            "Staff Medical record does not Exist!"));
                        }

                        var user = User.Identity.Name;
                        staffRecord.UserId = model.UserId;
                        staffRecord.StaffID = model.StaffID;
                        staffRecord.BloodGroupID = model.BloodGroupID;
                        staffRecord.FamilyDoctorName = model.FamilyDoctorName;
                        staffRecord.FamilyDoctorPhoneNumber = model.FamilyDoctorPhoneNumber;
                        staffRecord.GenotypeID = model.GenotypeID;
                        staffRecord.Height = model.Height;
                        staffRecord.AdditionalNote = model.AdditionalNote;
                        staffRecord.Weight = model.Weight;
                        _medRepo.Edit(staffRecord);
                        _medRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                        return Ok(new
                        {
                            Message = "Staff Medical Record Updated Successfully"
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
        [Route("Diagnosis/Edit/{ID}")]
        public IActionResult EditDiagnosis(Guid ID, EditMedicalHistory model)
        {
            try
            {
                if (User.IsInRole("Health"))
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Bad Request"));
                    }
                    var user = User.Identity.Name;
                    var medicalHistory = _medHistoryRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    if (medicalHistory == null)
                    {
                        return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Medical History does not exist"));
                    }

                    var patient = _userManager.FindByIdAsync(model.UserID.ToString()).Result;
                    if (patient == null)
                    {
                        return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "User does not exist"));
                    }

                    medicalHistory.AdditionDoctorsNote = model.AdditionDoctorsNote;
                    medicalHistory.Date = DateTime.UtcNow.AddHours(1);
                    medicalHistory.Description = model.Description;
                    medicalHistory.Doctor = User.Identity.Name;
                    medicalHistory.FinalDiagnosis = model.FinalDiagnosis;
                    medicalHistory.InitialDiagnosis = model.InitialDiagnosis;
                    medicalHistory.TreatmentPlan = model.TreatmentPlan;
                    medicalHistory.UserID = model.UserID;
                    medicalHistory.VitalSign = model.VitalSign;

                    _medHistoryRepo.Edit(medicalHistory);
                    _medHistoryRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());

                    return Ok(new { 
                        Message = "Medical History Updated Successfully"
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

        [HttpDelete]
        [Route("Record/Delete/{ID}")]
        public IActionResult DeleteMedRecord(Guid ID)
        {
            try
            {
                var user = User.Identity.Name;
                if (User.IsInRole("Health"))
                {
                    var record = _medRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    if (record == null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                                new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                    "Medical Record Does not Exist"));
                    }
                    _medRepo.Delete(record);
                    _medRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(new
                    {
                        Message = "Record Deleted Successfully"
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

        [HttpDelete]
        [Route("Record/Delete/{ID}")]
        public IActionResult DeleteMedicalHistory(Guid ID)
        {
            try
            {
                var user = User.Identity.Name;
                if (User.IsInRole("Health"))
                {
                    var medicalHistory = _medHistoryRepo.FindBy(x => x.Id == ID).FirstOrDefault();
                    if (medicalHistory == null)
                    {
                        return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Medical History does not exist"));
                    }
                    _medHistoryRepo.Delete(medicalHistory);
                    _medHistoryRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
                    return Ok(new
                    {
                        Message = "Medical History Deleted Successfully"
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
