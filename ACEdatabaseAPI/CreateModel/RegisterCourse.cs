using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class RegisterCourse
    {
        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public List<Course> Courses { get; set; }

        [Required]
        public Guid AcademicYearID { get; set; }

        [Required]
        public Guid SemesterID { get; set; }
    }

    public class Course 
    {
        public Guid CourseID { get; set; }
    }

    public class EditRegisteredCourse
    {
        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public List<EditCourse> Courses { get; set; }

        [Required]
        public Guid AcademicYearID { get; set; }

        [Required]
        public Guid SemesterID { get; set; }
    }

    public class EditCourse
    {
        public Guid? Id { get; set; }
        public Guid CourseID { get; set; }
    }
}
