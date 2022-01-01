using System;
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
        public string AdditionalNote { get; set; }
        public List<Guid> MedicalConditions { get; set; }
        public string OtherMedicalConditions { get; set; }

    }
}
