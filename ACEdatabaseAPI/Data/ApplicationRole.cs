using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Data
{
    public class ApplicationRole : IdentityRole
    {
        public string Category { get; set; }
        public string Description { get; set; }
        public int? GroupId { get; set; }
        public string FriendlyName { get; set; }
    }
}
