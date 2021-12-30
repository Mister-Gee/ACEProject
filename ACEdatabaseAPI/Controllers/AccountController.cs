using ACE.Domain.Abstract;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.Data;
using ACEdatabaseAPI.DTOModel;
using ACEdatabaseAPI.Helpers;
using ACEdatabaseAPI.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IOptions<JwtAuth> _jwtAuthentication;
        private readonly IOptions<AppSettings> _appSetting;
        private readonly IWebHostEnvironment _hostingEnvironment;
        IvStaffRepo _vStaffRepo;
        IvStudentRepo _vStudentRepo;
        IMapper _mapper;

        public AccountController(UserManager<ApplicationUser> userManager, IMapper mapper,
                IOptions<JwtAuth> jwtAuthentication, SignInManager<ApplicationUser> signInManger,
                IOptions<AppSettings> appSetting, IWebHostEnvironment hostingEnvironment,
                IvStaffRepo vStaffRepo, IvStudentRepo vStudentRepo)
        {
            _userManager = userManager;
            _signInManager = signInManger;
            _jwtAuthentication = jwtAuthentication;
            _appSetting = appSetting;
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
            _vStaffRepo = vStaffRepo;
            _vStudentRepo = vStudentRepo;
        }


        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] Login model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Invalid Login Attempt"));
                }

                ApplicationUser user;

                if (model.UserId.Contains('@'))
                {
                    user = await _userManager.FindByEmailAsync(model.UserId);
                }
                else
                {
                    user = await _userManager.FindByNameAsync(model.UserId);
                }

                if (user == null)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Invalid Login Attempt, You are not registered"));

                }

                if (user.Status == "InActive")
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "User Account Deactivated/Suspended"));

                }

                if (user.ForcePasswordChange)
                {

                    return StatusCode((int)HttpStatusCode.Unauthorized, new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(), $"ForcePasswordChange"));

                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var myClaims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                       
                    foreach (var item in roles)
                    {
                        myClaims.Add(new Claim(ClaimTypes.Role, item));
                    }

                    var token = new JwtSecurityToken(
                            issuer: _jwtAuthentication.Value.ValidIssuer,
                            audience: _jwtAuthentication.Value.ValidAudience,
                            claims: myClaims,
                            expires: DateTime.UtcNow.AddDays(1000),
                            notBefore: DateTime.UtcNow,
                            signingCredentials: _jwtAuthentication.Value.SigningCredentials);
                    string role = string.Join(",", roles);
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        firstName = user.FirstName,
                        lastName = user.LastName,
                        email = user.Email,
                        phone = user.PhoneNumber,
                        userID = user.Id,
                        role
                    });
                }
                else
                {

                return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Invalid login attempt"));
                }

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
        //[Authorize("MIS")]
        public async Task<ActionResult> Register([FromBody] Register model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (User.IsInRole("MIS"))
                    {
                        var strippedPhone = $"0{model.PhoneNumber.Substring(model.PhoneNumber.Length - 10, 10)}";
                        var user = new ApplicationUser()
                        {
                            UserName = model.Email,
                            Email = model.Email,
                            FirstName = model.SurName,
                            PhoneNumber = strippedPhone,
                            LastName = model.Name,
                            Date = Utility.CurrentTime,
                            ForcePasswordChange = true
                        };

                        var emailCheck = _userManager.Users.Where(x => x.Email == model.Email).FirstOrDefault();
                        if (emailCheck != null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                                new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                    "Email Already Exist!"));
                        }

                        var phoneNumCheck = _userManager.Users.Where(x => x.Email == strippedPhone).FirstOrDefault();
                        if (phoneNumCheck != null)
                        {
                            return StatusCode((int)HttpStatusCode.Unauthorized,
                                new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                    "Phone Number Already Exist!"));
                        }

                        var result = _userManager.CreateAsync(user, model.Password).Result;
                        if (result.Succeeded)
                        {
                            if (model.isStaff)
                            {
                               var role = await _userManager.AddToRoleAsync(user, "Staff");
                                if (role.Succeeded)
                                {
                                    return Ok(new
                                    {
                                        email = model.Email,
                                        message = "Staff Registered Successfully"
                                    });
                                }
                            }
                            else
                            {
                                var role = await _userManager.AddToRoleAsync(user, "Student");
                                if (role.Succeeded)
                                {
                                    return Ok(new
                                    {
                                        email = model.Email,
                                        message = "Student Registered Successfully"
                                    });
                                }
                            }
                        }
                    }
                }
                return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Invalid Registeration Attempt"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                                    new ApiError((int)HttpStatusCode.InternalServerError,
                                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Cafe/Register")]
        //[Authorize("MIS")]
        public async Task<ActionResult> RegisterCafe([FromBody] BaseRegister model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var strippedPhone = $"0{model.PhoneNumber.Substring(model.PhoneNumber.Length - 10, 10)}";
                    var user = new ApplicationUser()
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        FirstName = model.SurName,
                        PhoneNumber = strippedPhone,
                        LastName = model.Name,
                        Date = Utility.CurrentTime,
                        LockoutEnabled = false,
                        ForcePasswordChange = true

                    };

                    var emailCheck = _userManager.Users.Where(x => x.Email == model.Email).FirstOrDefault();
                    if(emailCheck != null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Email Already Exist!"));
                    }

                    var phoneNumCheck = _userManager.Users.Where(x => x.Email == strippedPhone).FirstOrDefault();
                    if (phoneNumCheck != null)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized,
                            new ApiError((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(),
                                "Phone Number Already Exist!"));
                    }

                    var result = _userManager.CreateAsync(user, model.Password).Result;
                    if (result.Succeeded)
                    {
                        var role = await _userManager.AddToRoleAsync(user, "Cafe");
                        if (role.Succeeded)
                        {
                            return Ok(new
                            {
                                email = model.Email,
                                message = "Cafe Registered Successfully"
                            });
                        }
                    }
                }
                return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Invalid Registeration Attempt"));
            }
            catch (Exception x)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                                    new ApiError((int)HttpStatusCode.InternalServerError,
                                        HttpStatusCode.InternalServerError.ToString(), x.ToString()));
            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        {
            try
            {

                if (model == null)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(),
                        "Please pass the required parameters"));
                }

                if (!ModelState.IsValid)
                {
                    string outerError = "";
                    foreach (var item in ModelState)
                    {
                        var innrErr = "";
                        foreach (var itm in item.Value.Errors)
                        {
                            innrErr += string.Join(';', itm.ErrorMessage);
                        }

                        outerError += $"{item.Key}: {innrErr} :: {Environment.NewLine}";
                    }

                }
                var usr = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(usr);
                if (user == null)
                {
                    return NotFound(new ApiError((int)HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), $"Unable to load user. :: {usr} :: {JsonConvert.SerializeObject(User)}"));
                }
                if (user.ForcePasswordChange && model.OldPassword.ToLower() == model.NewPassword.ToLower())
                {

                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Old and New password cannot be the same because you have been mandated to change your password"));

                }
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    string changePassError = "";
                    foreach (var error in changePasswordResult.Errors)
                    {
                        changePassError += error.Description + "; ";
                    }
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), changePassError));
                }
                if (user.ForcePasswordChange)
                {
                    user.ForcePasswordChange = false;
                    await _userManager.UpdateAsync(user);
                }
                await _signInManager.RefreshSignInAsync(user);

                //generate token again,
                var rols = await _userManager.GetRolesAsync(user);
                var myClaims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

                };
                foreach (var item in rols)
                {
                    myClaims.Add(new Claim(ClaimTypes.Role, item));
                }

                var token = new JwtSecurityToken(
                    issuer: _jwtAuthentication.Value.ValidIssuer,
                    audience: _jwtAuthentication.Value.ValidAudience,
                    claims: myClaims,
                    expires: DateTime.UtcNow.AddDays(30),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: _jwtAuthentication.Value.SigningCredentials);
                //var tkns = token.Claims.ToList();

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });

            }
            catch (Exception)
            {

                return StatusCode(500,
                    new ApiError(500, HttpStatusCode.InternalServerError.ToString(), "Error Changing your email."));

            }
        }

        [HttpPost]
        [Route("ForgetPassword/{userName}"), AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(string userName)
        {
            try
            {
                var msgBody = "";
                var emailsettin = _appSetting.Value.EmailSetting;
                
                var user = await _userManager.FindByEmailAsync(userName);
                if (user == null)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(),
                        "User has been removed or has not confirmed or does not exist"));
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var webUrl = _appSetting.Value.WebBaseUrl;
                //string[] url;
                //var RegResponse = new { UserId = user.Id, code = code, message = "Succesful Registration" };
                //url = new string[] { $"{webUrl}/Account/ResetPassword?code={code}", "" };
                string url = $"{webUrl}/Account/ResetPassword?code={code}";

                //msgBody = MailHelper.GetMailContent(_hostingEnvironment, "nu_mail.txt", "ForgotPassword.txt",
                //    user.FirstName, url);
                MailHelper.SendMailAsync(user.UserName, "Password Reset", url, emailsettin);
                return Ok();
                       
            }
            catch (Exception x)
            {
                return StatusCode(500,
                    new ApiError(500, HttpStatusCode.InternalServerError.ToString(),
                        $"Error generating reset Code for your password : {x.ToString()}."));
            }
        }

        [HttpPost]
        [Route("ResetPassword"), AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Invalid Model, please review and send again"));
                }
                ApplicationUser user = null;
                var origin = Request.Headers["Origin"];
                user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(),
                        "User has been removed or has not confirmed or does not exist"));

                }
                if (user.ForcePasswordChange)
                {
                    var compareHash = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
                    if (compareHash == PasswordVerificationResult.Success)
                    {

                        return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "you cannot use same password since you have been mandated to change your password"));

                    }
                }

                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Code));
                var result = await _userManager.ResetPasswordAsync(user, code, model.Password);

                if (result.Succeeded)
                {
                    if (user.ForcePasswordChange)
                    {
                        user.ForcePasswordChange = false;
                        await _userManager.UpdateAsync(user);
                    }
                    return Ok();
                }
                string resetErr = "";
                foreach (var error in result.Errors)
                {
                    resetErr = error.Description + "; ";
                }

                return BadRequest(new ApiError(500, HttpStatusCode.InternalServerError.ToString(), resetErr));

            }
            catch (Exception)
            {

                return StatusCode(500,
                    new ApiError(500, HttpStatusCode.InternalServerError.ToString(),
                        "Error generating reset Code for your password."));

            }
        }

        [HttpGet]
        [Route("LoggedIn/User")]
        public IActionResult GetUser()
        {
            try
            {
                var name = User.Identity.Name;
                var user = _userManager.FindByNameAsync(name).Result;

                if(_userManager.IsInRoleAsync(user, "Staff").Result)
                {
                    var staffResult = _vStaffRepo.FindBy(x => x.Id == user.Id).FirstOrDefault();
                    if(staffResult != null)
                    {
                        return Ok(staffResult);
                    }
                }

                if(_userManager.IsInRoleAsync(user, "Student").Result)
                {
                    var studentResult = _vStudentRepo.FindBy(x => x.Id == user.Id).FirstOrDefault();
                    if(studentResult != null)
                    {
                        return Ok(studentResult);
                    }
                }
                var userDTO = new UserDTO();
                var result = _mapper.Map(user, userDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500,
                    new ApiError(500, HttpStatusCode.InternalServerError.ToString(),
                        ex.Message.ToString()));

            }
        }

        [HttpGet]
        [Route("Get/Email")]
        public IActionResult GetUser(string Email)
        {
            try
            {
                var user = _userManager.FindByNameAsync(Email).Result;

                if(user == null)
                {
                    return Ok(new { 
                        Message = "User Not Found"
                    });
                }

                if (_userManager.IsInRoleAsync(user, "Staff").Result)
                {
                    var staffResult = _vStaffRepo.FindBy(x => x.Id == user.Id).FirstOrDefault();
                    if (staffResult != null)
                    {
                        return Ok(staffResult);
                    }
                }

                if (_userManager.IsInRoleAsync(user, "Student").Result)
                {
                    var studentResult = _vStudentRepo.FindBy(x => x.Id == user.Id).FirstOrDefault();
                    if (studentResult != null)
                    {
                        return Ok(studentResult);
                    }
                }

                var userDTO = new UserDTO();
                var result = _mapper.Map(user, userDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500,
                    new ApiError(500, HttpStatusCode.InternalServerError.ToString(),
                        ex.Message.ToString()));

            }
        }

        [HttpPost]
        [Route("Get/ByMatricNumber")]
        public IActionResult GetUserByMatricNumber(SearchByMatricNumber model)
        {
            try
            {
                var user = _vStudentRepo.FindBy(x => x.MatricNumber == model.MatricNumber).FirstOrDefault();
                if (user == null)
                {
                    return Ok(new
                    {
                        Message = "User Not Found"
                    });
                }

                return Ok(user);
            }
            catch (Exception ex)
            {

                return StatusCode(500,
                    new ApiError(500, HttpStatusCode.InternalServerError.ToString(),
                        ex.Message.ToString()));

            }
        }

        [HttpPost]
        [Route("Get/ByStaffID")]
        public IActionResult GetUserByStaffID(SearchByStaffID model)
        {
            try
            {
                var user = _vStaffRepo.FindBy(x => x.StaffID == model.StaffID).FirstOrDefault();
                if (user == null)
                {
                    return Ok(new
                    {
                        Message = "User Not Found"
                    });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {

                return StatusCode(500,
                    new ApiError(500, HttpStatusCode.InternalServerError.ToString(),
                        ex.Message.ToString()));

            }
        }

        [HttpPut]
        [Route("User/UpdateStatus/ByID")]
        public IActionResult ChangeUserStatus(userActiveStatusID model)
        {
            try
            {
                if (User.IsInRole("MIS"))
                {
                    var user = _userManager.FindByIdAsync(model.UserID).Result;
                    if (user == null)
                    {
                        return Ok(new
                        {
                            Message = "User Not Found"
                        });
                    }
                    user.isDisabled = model.ActiveStatus;

                    var result = _userManager.UpdateAsync(user).Result;
                    if (result.Succeeded)
                    {
                        return Ok( new { 
                            Message = "Status Changed Succesffully"
                        });
                    }
                    return BadRequest();
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {

                return StatusCode(500,
                    new ApiError(500, HttpStatusCode.InternalServerError.ToString(),
                        ex.Message.ToString()));

            }
        }

        [HttpPut]
        [Route("User/UpdateStatus/ByEmail")]
        public IActionResult ChangeUserStatus(userActiveStatusEmail model)
        {
            try
            {
                if (User.IsInRole("MIS"))
                {
                    var user = _userManager.FindByEmailAsync(model.UserEmail).Result;
                    if (user == null)
                    {
                        return Ok(new
                        {
                            Message = "User Not Found"
                        });
                    }
                    user.isDisabled = model.ActiveStatus;

                    var result = _userManager.UpdateAsync(user).Result;
                    if (result.Succeeded)
                    {
                        return Ok(new
                        {
                            Message = "Status Changed Succesffully"
                        });
                    }
                    return BadRequest();
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {

                return StatusCode(500,
                    new ApiError(500, HttpStatusCode.InternalServerError.ToString(),
                        ex.Message.ToString()));

            }
        }

        [HttpPut]
        [Route("User/UpdateStatus/ByStaffID")]
        public IActionResult ChangeUserStatus(userActiveStatusStaffID model)
        {
            try
            {
                if (User.IsInRole("MIS"))
                {
                    var user = _userManager.Users.Where(x => x.StaffID == model.StaffID).FirstOrDefault();
                    if (user == null)
                    {
                        return Ok(new
                        {
                            Message = "User Not Found"
                        });
                    }
                    user.isDisabled = model.ActiveStatus;

                    var result = _userManager.UpdateAsync(user).Result;
                    if (result.Succeeded)
                    {
                        return Ok(new
                        {
                            Message = "Status Changed Succesffully"
                        });
                    }
                    return BadRequest();
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {

                return StatusCode(500,
                    new ApiError(500, HttpStatusCode.InternalServerError.ToString(),
                        ex.Message.ToString()));

            }
        }

        [HttpPut]
        [Route("User/UpdateStatus/ByMatricNumber")]
        public IActionResult ChangeUserStatus(userActiveStatusMatricNumber model)
        {
            try
            {
                if (User.IsInRole("MIS"))
                {
                    var user = _userManager.Users.Where(x => x.MatricNumber == model.MatricNumber).FirstOrDefault();
                    if (user == null)
                    {
                        return Ok(new
                        {
                            Message = "User Not Found"
                        });
                    }
                    user.isDisabled = model.ActiveStatus;

                    var result = _userManager.UpdateAsync(user).Result;
                    if (result.Succeeded)
                    {
                        return Ok(new
                        {
                            Message = "Status Changed Succesffully"
                        });
                    }
                    return BadRequest();
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {

                return StatusCode(500,
                    new ApiError(500, HttpStatusCode.InternalServerError.ToString(),
                        ex.Message.ToString()));

            }
        }
    }
}