using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities
{
    public class vMedicalRecord
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public Guid BloodGroupID { get; set; }
        public string BloodGroup { get; set; }
        public Guid GenotypeID { get; set; }
        public string Genotype { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string FamilyDoctorName { get; set; }
        public string FamilyDoctorPhoneNumber { get; set; }
        public string AdditionalNote { get; set; }
        public string MedicalConditions { get; set; }
        public string OtherMedicalConditions { get; set; }
    }
}
