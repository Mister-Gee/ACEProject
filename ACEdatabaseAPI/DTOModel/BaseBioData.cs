﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class BaseBioData
    {
        public string OtherName { get; set; }
        public string FormerName { get; set; }

        [Required]
        public string NIN { get; set; }

        [Required]
        public Guid School { get; set; }

        [Required]
        public Guid Department { get; set; }

        [Required]
        public Guid MaritalStatus { get; set; }

        [Required]
        public Guid Religion { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Guid Gender { get; set; }

        [Required]
        public bool isDisabled { get; set; }

        public string Disability { get; set; }

        public string AlternatePhoneNumber { get; set; }

        [Required]
        public bool isIndigenous { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Required]
        public string StateOfOrigin { get; set; }

        [Required]
        public string LG { get; set; }

        [Required]
        public string Hometown { get; set; }

        public string ZipPostalCode { get; set; }

        [Required]
        public string Address { get; set; }

        public string TwitterID { get; set; }
        public string FacebookID { get; set; }
        public string InstagramID { get; set; }
        public string LinkedInID { get; set; }
    }
}
