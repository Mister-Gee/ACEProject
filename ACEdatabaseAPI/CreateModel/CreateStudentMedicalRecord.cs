using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class CreateStudentMedicalRecord : BaseCreateMedicalRecord
    {
        [Required]
        public string MatricNumber { get; set; }
    }
}
