﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class CreateRole
    {
        [Required]
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
