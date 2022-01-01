using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class FlagStudentBase
    {
        public Guid FlagLevelID { get; set; }
        public Guid SecurityID { get; set; }
    }

    public class FlagByID : FlagStudentBase
    {
        public Guid StudentID { get; set; }
    }

    public class FlagByMatricNumber : FlagStudentBase
    {
        public string MatricNumber { get; set; }
    }

    public class FlagByBiometric : FlagStudentBase
    {
        public string Biometric { get; set; }
    }
}
