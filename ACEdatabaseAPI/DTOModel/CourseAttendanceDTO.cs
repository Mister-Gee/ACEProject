using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class CourseAttendanceDTO
    {
        public Guid Id { get; set; }
        public string Course { get; set; }
        public int CourseUnit { get; set; }
        public int TotalClassesAttended { get; set; }
        public int TotalClassesHeld { get; set; }
        public double  AttendancePercent { get; set; }
    }
}
