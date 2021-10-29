using ACE.Domain.Abstract;
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
		[Route("/Get")]
		public async Task<IActionResult> CreateRole()
		{
			var roles = _roleManager.Roles.ToList();
			if(roles.Count > 0)
            {
				return Ok(roles);
            }
			
			return Ok(new {
				Message = "No Roles Created Yet"
			});

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
        [Route("/roles/{userName}")]
        public async Task<IActionResult> Roles(string userName)
        {
            //var usr = await _userManager.FindByNameAsync(userName);
            if (!string.IsNullOrEmpty(userName))
            {
                // var rols = await _userManager.GetRolesAsync(usr).ConfigureAwait(true);
                var _rols = _vUserRoleRepo.FindBy(a => a.UserName.ToLower() == userName.ToLower()).Select(a => new { a.RoleName, a.RoleFriendlyName }).ToList();// _vAspnetRoleRep.FindBy(a =>a.Name==).ToList();
                return Ok(_rols);
            }
            else
            {
                return NotFound(new ApiError((int)HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), $"Unable to load user with ID."));

            }
        }

		[HttpPost, Authorize(Roles = "Admin")]
		[Route("/addRoles/{userName}/{roles}")]
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
								rls.Add(item);

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
	}
}
