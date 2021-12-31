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
    public class StudentRegisteredCourse
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseRegisterationID { get; set; }
        public Guid CourseID { get; set; }
        public Guid AcademicYearID { get; set; }
        public Guid SemesterID { get; set; }
        public int CourseUnit { get; set; }
        public DateTime CourseRegisterationDate { get; set; }
    }
}
