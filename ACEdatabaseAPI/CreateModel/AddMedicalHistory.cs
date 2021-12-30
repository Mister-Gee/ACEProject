using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class AddMedicalHistory
    {
        public Guid UserID { get; set; }
        public string InitialDiagnosis { get; set; }
        public string FinalDiagnosis { get; set; }
        public string Description { get; set; }
        public string TreatmentPlan { get; set; }
        public string VitalSign { get; set; }
        public string AdditionDoctorsNote { get; set; }
    }

    public class EditMedicalHistory
    {
        public Guid Id { get; set; }
        public Guid UserID { get; set; }
        public string InitialDiagnosis { get; set; }
        public string FinalDiagnosis { get; set; }
        public string Description { get; set; }
        public string TreatmentPlan { get; set; }
        public string VitalSign { get; set; }
        public string AdditionDoctorsNote { get; set; }
    }
}
