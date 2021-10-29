using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class StudentBioData : BaseBioData
    {
        [Required]
        public string JambRegNumber { get; set; }

        [Required]
        public string MatricNumber { get; set; }

        [Required]
        public DateTime AdmissionDate { get; set; }

        [Required]
        public string ModeOfAdmission { get; set; }

        [Required]
        public Guid StudentCategory { get; set; }

        [Required]
        public Guid Programme { get; set; }

        [Required]
        public Guid EntryLevel { get; set; }

        [Required]
        public Guid CurrentLevel { get; set; }
    }
}
