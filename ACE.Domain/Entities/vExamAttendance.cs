using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities
{
    public class vExamAttendance
    {
        public Guid Id { get; set; }
        public Guid CourseID { get; set; }
        public string Course { get; set; }
        public DateTime ExamStartDateTime { get; set; }
        public DateTime ExamEndDateTime { get; set; }
        public string ExamStartingStudents { get; set; }
        public string ExamEndingStudents { get; set; }
        public Guid SupervisorID { get; set; }
        public string Supervisor { get; set; }
        public Guid AcademicYearID { get; set; }
        public string AcademicYear { get; set; }
        public Guid SemesterID { get; set; }
        public string Semester { get; set; }

    }
}
