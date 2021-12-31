using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities
{
    public class vCourse
    {
        public Guid Id { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }
        public int CourseUnit { get; set; }
        public string CourseDescription { get; set; }
        public Guid LeadLecturerID { get; set; }
        public string LeadLecturer { get; set; }
        public Guid AssistantLecturerID { get; set; }
        public string AssistantLecturer { get; set; }
        public Guid OtherCourseLecturerID { get; set; }
        public string OtherCourseLecturer { get; set; }

        public Guid SchoolID { get; set; }
        public string School { get; set; }
        public Guid DepartmentID { get; set; }
        public string Department { get; set; }
        public bool isDepartmental { get; set; }
        public bool isGeneral { get; set; }
        public bool isOptional { get; set; }
        public string EligibleDepartments { get; set; }
    }
}
