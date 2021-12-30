using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class StaffBioData : BaseBioData
    {
        public string StaffID { get; set; }

        public DateTime? EmploymentDate { get; set; }
        public string IPPISNo { get; set; }
    }
}
