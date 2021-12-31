using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities
{
    public class CourseRegisteration
    {
        public Guid Id { get; set; }
        public int Courses { get; set; }
        public Guid StudentID { get; set; }
        public Guid AcademicYearID { get; set; }
        public Guid SemesterID { get; set; }
        public DateTime RegDate { get; set; }
        public int TotalUnit { get; set; }
    }
}
