using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities
{
    public class CurrentAcademicSession
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AcademicYearID { get; set; }
        public Guid SemesterID { get; set; }
        public DateTime Date { get; set; }
    }
}
