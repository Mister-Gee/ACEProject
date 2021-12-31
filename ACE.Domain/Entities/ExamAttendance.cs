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
    public class ExamAttendance
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CourseID { get; set; }
        public DateTime ExamStartDateTime { get; set; }
        public DateTime ExamEndDateTime { get; set; }
        public string ExamStartingStudents { get; set; }
        public string ExamEndingStudents { get; set; }
        public Guid SupervisorID { get; set; }
        public Guid AcademicYearID { get; set; }
        public Guid SemesterID { get; set; }
    }
}
