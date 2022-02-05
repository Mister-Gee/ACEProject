using ACE.Domain.Abstract;
using ACE.Domain.Entities;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.Data;
using ACEdatabaseAPI.Model;
using Microsoft.AspNetCore.Authorization;
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
	//[Authorize("MIS")]
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        IvUserRoleRepo _vUserRoleRepo;

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
               IvUserRoleRepo vUserRoleRepo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _vUserRoleRepo = vUserRoleRepo;
        }


		[HttpGet]
		[ProducesResponseType(typeof(List<ApplicationRole>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiError),
			StatusCodes.Status500InternalServerError)]
		[Route("Get")]
		public async Task<IActionResult> GetRole()
		{
            try
            {
				var roles = _roleManager.Roles.ToList();
				if (roles.Count > 0)
				{
					return Ok(roles);
				}

				return Ok(new
				{
					Message = "No Roles Created Yet"
				});
			}
			catch(Exception x)
            {
				return StatusCode((int)HttpStatusCode.InternalServerError,
					new ApiError((int)HttpStatusCode.InternalServerError,
						HttpStatusCode.InternalServerError.ToString(), x.ToString()));
			}

		}

		[HttpGet]
		[ProducesResponseType(typeof(List<vUserRole>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiError),
			StatusCodes.Status500InternalServerError)]
		[Route("Staff")]
		public IActionResult GetStaffRole()
		{
			try
			{
				var userRoles = _vUserRoleRepo.FindBy(x => x.Role.ToUpper() != "STUDENT").ToList();
				var result = userRoles.GroupBy(x => x.UserId).Select(x => new vUserRole { 
					UserId = x.First().UserId,
					RoleId = x.First().RoleId,
					Email = x.First().Email,
					FirstName = x.First().FirstName,
					LastName = x.First().LastName,
					Role = string.Join(", ", x.Select(y => y.Role)),
					StaffID = x.First().StaffID,
					MatricNumber = x.First().MatricNumber
				}).ToList();

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
		[Route("Role/Create")]
		public async Task<IActionResult> CreateRole(CreateRole model)
        {
            if (ModelState.IsValid)
            {
				bool roleState = await _roleManager.RoleExistsAsync(model.RoleName);
                if (roleState)
                {
					BadRequest(new ApiError(StatusCodes.Status400BadRequest,
									HttpStatusCode.BadRequest.ToString(), "Role Already Exist"));
				}
				var role = new ApplicationRole()
				{
					Category = model.Category,
					Name = model.RoleName,
					Description = model.Description,
				};
				var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
					return Ok(new {
						Message = "Role Created Successfully"
					});
                }
            }
			return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
									HttpStatusCode.BadRequest.ToString(), "Invalid Request"));

        }

        [HttpPost]
        [Route("roles/{userName}")]
        public async Task<IActionResult> Roles(string userName)
        {
            //var usr = await _userManager.FindByNameAsync(userName);
            if (!string.IsNullOrEmpty(userName))
            {
				var user = _userManager.FindByNameAsync(userName).Result;
				var _rols = _userManager.GetRolesAsync(user);
                //var _rols = _vUserRoleRepo.FindBy(a => a.UserName.ToLower() == userName.ToLower()).Select(a => new { a.RoleName, a.RoleFriendlyName }).ToList();// _vAspnetRoleRep.FindBy(a =>a.Name==).ToList();
                return Ok(_rols);
            }
            else
            {
                return NotFound(new ApiError((int)HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), $"Unable to load user with ID."));

            }
        }

		[HttpPost]
		[Route("addRoles/byUserName/{userName}/{roles}")]
		public async Task<IActionResult> UserRoles(string userName, string roles)
		{
			if (!string.IsNullOrEmpty(roles) && !string.IsNullOrEmpty(userName))
			{

				var usr = await _userManager.FindByNameAsync(userName);
				if (usr != null)
				{
					string result = "";
					var rls = new List<string>();
					foreach (var item in roles.Split(','))
					{
						if (!string.IsNullOrEmpty(item))
						{
							//check if the role exists
							if (await _roleManager.RoleExistsAsync(item))
							{
								if(!await _userManager.IsInRoleAsync(usr, item))
                                {
									rls.Add(item);
								}
							}
							else
							{
								if (string.IsNullOrEmpty(result))
								{
									result = item;
								}
								else
								{
									result += $",{Environment.NewLine} {item} does not Exist as a role";
								}
							}

							if (rls.Count > 0)
							{
								await _userManager.AddToRolesAsync(usr, rls);
							}
							else
							{
								return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
									HttpStatusCode.BadRequest.ToString(), result));
							}
						}
					}

					return Ok(rls);
				}
				else
				{
					return NotFound(new ApiError((int)HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(),
						$"Unable to load user with ID."));

				}
			}

			return BadRequest(new ApiError((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(),
				$"userName and roles cannot be empty"));

		}

		[HttpPost]
		[Route("addRoles/byUserId/{userId}/{roles}")]
		public async Task<IActionResult> UserRoles(Guid userId, string roles)
		{
			if (!string.IsNullOrEmpty(roles))
			{

				var usr = await _userManager.FindByIdAsync(userId.ToString());
				if (usr != null)
				{
					string result = "";
					var rls = new List<string>();
					foreach (var item in roles.Split(','))
					{
						if (!string.IsNullOrEmpty(item))
						{
							//check if the role exists
							if (await _roleManager.RoleExistsAsync(item))
							{
								if (!await _userManager.IsInRoleAsync(usr, item))
								{
									rls.Add(item);
								}
							}
							else
							{
								if (string.IsNullOrEmpty(result))
								{
									result = item;
								}
								else
								{
									result += $",{Environment.NewLine} {item} does not Exist as a role";
								}
							}

							if (rls.Count > 0)
							{
								await _userManager.AddToRolesAsync(usr, rls);
							}
							else
							{
								return BadRequest(new ApiError(StatusCodes.Status400BadRequest,
									HttpStatusCode.BadRequest.ToString(), result));
							}
						}
					}

					return Ok(rls);
				}
				else
				{
					return NotFound(new ApiError((int)HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(),
						$"Unable to load user with ID."));

				}
			}

			return BadRequest(new ApiError((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(),
				$"userName and roles cannot be empty"));

		}

		[HttpPost]
		[Route("removeRole/byUserID/{userId}/{role}")]
		public async Task<IActionResult> RemoveRoleByUserID (Guid userId, string role)
        {
            try
            {
				var user = await _userManager.FindByIdAsync(userId.ToString());
				if(user == null)
                {
					return NotFound(new
					{
						Message = "User Not Found"
					});
                }
				var result = await _userManager.RemoveFromRoleAsync(user, role);
                if (result.Succeeded)
                {
					return Ok(new { 
						Message = "Success"
					});
                }
				return BadRequest(new { 
					Message = "Fail"
				});
            }
			catch(Exception x)
            {
				return StatusCode((int)HttpStatusCode.InternalServerError,
					new ApiError((int)HttpStatusCode.InternalServerError,
						HttpStatusCode.InternalServerError.ToString(), x.ToString()));
			}
        }

		[HttpPost]
		[Route("removeRole/byUserName/{userName}/{role}")]
		public async Task<IActionResult> RemoveRoleByUserName(string userName, string role)
		{
			try
			{
				var user = await _userManager.FindByNameAsync(userName);
				if (user == null)
				{
					return NotFound(new
					{
						Message = "User Not Found"
					});
				}
				var result = await _userManager.RemoveFromRoleAsync(user, role);
				if (result.Succeeded)
				{
					return Ok(new
					{
						Message = "Success"
					});
				}
				return BadRequest(new
				{
					Message = "Fail"
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
