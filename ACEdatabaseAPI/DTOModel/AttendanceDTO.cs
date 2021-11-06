using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class AttendanceDTO
    {
        public Guid Id { get; set; }
        public int ClassWeek { get; set; }
        public List<StudentAttendanceDetailDTO> PresentStudents { get; set; }
    }
}
