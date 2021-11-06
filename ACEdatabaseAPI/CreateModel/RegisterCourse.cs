using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class RegisterCourse
    {
        public Guid Id { get; set; }

        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public string StudentMatricNumber { get; set; }

        [Required]
        public Guid CourseID { get; set; }

        [Required]
        public Guid AcademicYearID { get; set; }

        [Required]
        public Guid SemesterID { get; set; }
    }
}
