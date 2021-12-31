using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class StudentExamAttendance
    {
        public Guid Id { get; set; }
        public string Course { get; set; }
        public string Supervisor { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public List<ExamStudents> Students { get; set; }
    }

    public class ExamStudents
    {
        public bool StartAttendance { get; set; }
        public bool EndAttendance { get; set; }
        public string StudentMatricNumber { get; set; }
    }
}
