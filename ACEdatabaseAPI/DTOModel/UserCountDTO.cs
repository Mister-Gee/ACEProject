using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class UserCountDTO
    {
        public int TotalActiveStudent { get; set; }
        public int TotalInactiveStudent { get; set; }
        public int TotalStudent { get; set; }
        public int TotalStaff { get; set; }
    }
}
