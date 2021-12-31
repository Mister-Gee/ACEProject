using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class CourseExamAttendanceDTO
    {
        public Guid Id { get; set; }
        public string Course { get; set; }
        public DateTime? StartTime { get; set; }
        public bool StartAttendance { get; set; }
        public DateTime? EndTime { get; set; }
        public bool EndAttendance { get; set; }
        public string Supervisor { get; set; }
        public bool IsCommence { get; set; }
    }
}
