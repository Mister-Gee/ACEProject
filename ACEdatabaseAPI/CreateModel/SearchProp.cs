using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class SearchByEmail
    {
        public string Email { get; set; }
    }

    public class SearchByMatricNumber
    {
        public string MatricNumber { get; set; }
    }

    public class SearchByStaffID
    {
        public string StaffID { get; set; }
    }

    public class SearchByBiometrics
    {
        public string Biometric { get; set; }
    }
}
