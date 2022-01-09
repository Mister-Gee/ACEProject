﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class CreateControlledData
    {
        [Required]
        public string Name { get; set; }
    }

    public class CreateAcademicYear : CreateControlledData
    {
        public DateTime Year { get; set; }

    }
}
