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
    public class ClassAttendance
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CourseID { get; set; }
        public string PresentStudent { get; set; }
        public DateTime ClassDateTime { get; set; }
        public int ClassWeek { get; set; }
        public Guid AcademicYearID { get; set; }
        public Guid SemesterID { get; set; }
        public Guid ClassAttendanceID { get; set; }

    }
}
