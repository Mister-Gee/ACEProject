using ACE.Domain.Entities.ControlledEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities
{
    public class MedicalRecord
    {
        [Key]
        public Guid UserId { get; set; }

        [ForeignKey("BloodGroup")]
        public Guid BloodGroupID { get; set; }
        public BloodGroup BloodGroup { get; set; }

        [ForeignKey("Genotype")]
        public Guid GenotypeID { get; set; }
        public Genotype Genotype { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public string FamilyDoctorName { get; set; }
        public string FamilyDoctorPhoneNumber { get; set; }

        public string MedicalHistory { get; set; }

        public string OtherMedicalHistorys { get; set; }

    }
}
