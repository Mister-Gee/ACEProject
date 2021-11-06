using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class CreateCourse
    {
        [Required]
        public string CourseTitle { get; set; }

        [Required]
        public string CourseCode { get; set; }

        public string CourseDescription { get; set; }

        [Required]
        public double CourseUnit { get; set; }

        [Required]
        public Guid AcademicYearID { get; set; }

        [Required]
        public Guid SemesterID { get; set; }

        [Required]
        public Guid LeadLecturerID { get; set; }
        public Guid AssistantLecturerID { get; set; }

        [Required]
        public Guid SchoolID { get; set; }

        [Required]
        public Guid DepartmentID { get; set; }
        

        public bool isDepartmental { get; set; }
        public bool isOptional { get; set; }
        public string OtherCourseLecturer { get; set; }
    }
}
