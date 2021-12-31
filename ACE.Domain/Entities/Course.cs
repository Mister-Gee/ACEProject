using ACE.Domain.Entities.ControlledEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }
        public int CourseUnit { get; set; }
        public string CourseDescription { get; set; }
        public Guid LeadLecturerID { get; set; }
        public Guid AssistantLecturerID { get; set; }
        public Guid SchoolID { get; set; }
        public Guid DepartmentID { get; set; }
        public bool isDepartmental { get; set; }
        public bool isGeneral { get; set; }
        public bool isOptional { get; set; }
        public Guid OtherCourseLecturer { get; set; }
        public string EligibleDepartments { get; set; }
    }

}
