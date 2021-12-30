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
        public int CourseUnit { get; set; }

        [Required]
        public Guid LeadLecturerID { get; set; }
        public Guid? AssistantLecturerID { get; set; }

        [Required]
        public Guid SchoolID { get; set; }

        [Required]
        public Guid DepartmentID { get; set; }
        public bool isGeneral { get; set; }
        public bool isDepartmental { get; set; }
        public bool isOptional { get; set; }
        public Guid? OtherCourseLecturer { get; set; }
        public List<Guid> EligibleDepartments { get; set; }
    }

    public class EditCreatedCourse
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string CourseTitle { get; set; }

        [Required]
        public string CourseCode { get; set; }

        public string CourseDescription { get; set; }

        [Required]
        public int CourseUnit { get; set; }

        [Required]
        public Guid LeadLecturerID { get; set; }
        public Guid? AssistantLecturerID { get; set; }

        [Required]
        public Guid SchoolID { get; set; }

        [Required]
        public Guid DepartmentID { get; set; }
        public bool isGeneral { get; set; }
        public bool isDepartmental { get; set; }
        public bool isOptional { get; set; }
        public Guid? OtherCourseLecturer { get; set; }
        public List<Guid> EligibleDepartments { get; set; }
    }
}
