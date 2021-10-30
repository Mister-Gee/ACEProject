using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class StaffDTO : UserDTO
    {
        public string StaffID { get; set; }
        public DateTime EmploymentDate { get; set; }

    }
}
