using ACE.Domain.Abstract;
using ACE.Domain.Entities;
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
    [ApiController]
    public class DeviceController : ControllerBase
    {
		IDeviceRepo _deviceRepo;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public DeviceController(IDeviceRepo deviceRepo, SignInManager<ApplicationUser> signInManager)
		{
			_deviceRepo = deviceRepo;
			_signInManager = signInManager;
		}


		//[ApiExplorerSettings(IgnoreApi = true)]
		[AllowAnonymous]
		[HttpPost, Route("RegisterDevice")]
		public IActionResult RegisterDeviceAsync([FromBody] DeviceVM model)
		{
			var user = User.Identity.Name;
			if (string.IsNullOrEmpty(user))
			{
				user = model.username;
			}

			var dv = _deviceRepo.FindBy(c => c.Username.ToLower() == user.ToLower()).FirstOrDefault();
			if (dv == null)
			{
				Device device = new Device
				{
					DeviceToken = model.deviceToken,
					Username = user,
					DeviceId = model.deviceId
				};
				_deviceRepo.Add(device);
				_deviceRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
			}
			else
			{
				if (dv.DeviceId == model.deviceId)
				{
					if (dv.Logout == true)
					{
						_deviceRepo.Delete(dv);
						try
						{
							_signInManager.SignOutAsync();
						}
						catch (Exception)
						{

						}
					}
					else
					{
						dv.DeviceToken = model.deviceToken;
						_deviceRepo.Edit(dv);
					}

					_deviceRepo.Save(user, HttpContext.Connection.RemoteIpAddress.ToString());
				}
				else
				{
					return BadRequest(new ApiError((int)HttpStatusCode.BadRequest,
						HttpStatusCode.BadRequest.ToString(),
						$"Your account is logged in on another device. Kindly logout from the other device."));
				}
			}

			return Ok();
		}

		//[ApiExplorerSettings(IgnoreApi = true)]
		[HttpPost, Route("UnRegisterDevice")]
		public IActionResult UnRegisterDevice([FromBody] DeviceVM model)
		{
			var dv = _deviceRepo
				.FindBy(c => c.DeviceId == model.deviceId && c.Username == User.Identity.Name).FirstOrDefault();
			if (dv != null)
			{
				_signInManager.SignOutAsync();
				_deviceRepo.Delete(dv);
				_deviceRepo.Save(User.Identity.Name, HttpContext.Connection.RemoteIpAddress.ToString());
			}

			return Ok();
		}
	}
}
