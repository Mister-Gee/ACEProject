using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class CreateAttendance
    {
        [Required]
        public Guid CourseID { get; set; }

        [Required]

        public List<Guid> PresentStudent { get; set; }

        [Required]

        public int ClassWeek { get; set; }

        [Required]

        public Guid AcademicYearID { get; set; }

        [Required]

        public Guid SemesterID { get; set; }
    }
}
