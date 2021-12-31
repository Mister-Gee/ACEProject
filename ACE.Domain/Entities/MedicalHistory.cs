using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities
{
    public class MedicalHistory
    {
        public Guid Id { get; set; }
        public Guid UserID { get; set; }
        public DateTime Date { get; set; }
        public string InitialDiagnosis { get; set; }
        public string FinalDiagnosis { get; set; }
        public string Description { get; set; }
        public string TreatmentPlan { get; set; }
        public string Doctor { get; set; }
        public string VitalSign { get; set; }
        public string AdditionDoctorsNote { get; set; }
    }
}
