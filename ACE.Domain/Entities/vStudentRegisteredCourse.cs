using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities
{
    public class vStudentRegisteredCourse
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string StudentMatricNumber { get; set; }
        public Guid CourseRegisterationID { get; set; }
        public Guid CourseID { get; set; }
        public string Course { get; set; }
        public Guid AcademicYearID { get; set; }
        public string AcademicYear { get; set; }
        public Guid SemesterID { get; set; }
        public string Semester { get; set; }
        public int CourseUnit { get; set; }
        public DateTime CourseRegisterationDate { get; set; }
    }
}
