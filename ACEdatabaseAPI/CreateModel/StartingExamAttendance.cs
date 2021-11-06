using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class StartingExamAttendance
    {
        [Required]
        public Guid CourseID { get; set; }

        [Required]

        public List<Guid> ExamStartingStudents { get; set; }

        [Required]

        public Guid SupervisorID { get; set; }

        [Required]

        public Guid AcademicYearID { get; set; }

        [Required]
          
        public Guid SemesterID { get; set; }
    }
}
