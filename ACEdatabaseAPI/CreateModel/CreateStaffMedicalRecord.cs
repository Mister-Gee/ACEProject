﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class CreateStaffMedicalRecord : BaseCreateMedicalRecord
    {
        [Required]
        public string StaffID { get; set; }
    }

    public class EditStaffMedicalRecord : BaseCreateMedicalRecord
    {
        [Required]
        public string StaffID { get; set; }

        [Required]
        public Guid Id { get; set; }
    }
}
