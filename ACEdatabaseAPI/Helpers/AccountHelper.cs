using ACEdatabaseAPI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Helpers
{
    public class AccountHelper
    {

        private readonly IHttpClientFactory _clientFactory;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountHelper(IHttpClientFactory clientFactory, IWebHostEnvironment hostEnvironment, 
            RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _clientFactory = clientFactory;
            _hostingEnvironment = hostEnvironment;
        }
    }
}
