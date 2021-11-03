﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class BaseCreateMedicalRecord
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]

        public Guid BloodGroupID { get; set; }

        [Required]

        public Guid GenotypeID { get; set; }

        [Required]

        public double Weight { get; set; }

        [Required]

        public double Height { get; set; }

        public string FamilyDoctorName { get; set; }
        public string FamilyDoctorPhoneNumber { get; set; }

        public string MedicalHistory { get; set; }

        public List<Guid> OtherMedicalHistorys { get; set; }
    }
}
