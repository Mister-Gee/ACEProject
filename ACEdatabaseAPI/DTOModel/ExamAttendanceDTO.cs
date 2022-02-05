using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class ExamAttendanceDTO
    {
        public Guid Id { get; set; }
        public Guid CourseID { get; set; }
        public string Course { get; set; }
        public DateTime ExamStartDateTime { get; set; }
        public DateTime ExamEndDateTime { get; set; }
        public List<Guid> ExamStartingStudents { get; set; }
        public List<Guid> ExamEndingStudents { get; set; }
        public Guid SupervisorID { get; set; }
        public string Supervisor { get; set; }
        public Guid AcademicYearID { get; set; }
        public string AcademicYear { get; set; }
        public Guid SemesterID { get; set; }
        public string Semester { get; set; }
    }
}
