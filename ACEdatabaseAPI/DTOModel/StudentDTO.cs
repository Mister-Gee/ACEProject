using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class StudentDTO : UserDTO
    {
        public DateTime AdmissionDate { get; set; }
        public string ModeOfAdmission { get; set; }
        public string StudentCategory { get; set; }
        public string Programme { get; set; }
        public string EntryLevel { get; set; }
        public string CurrentLevel { get; set; }
        public string JambRegNumber { get; set; }
        public string MatricNumber { get; set; }
    }
}
