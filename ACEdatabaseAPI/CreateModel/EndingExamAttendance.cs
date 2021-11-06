using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class EndingExamAttendance
    {
        [Required]
        public List<Guid> ExamEndingStudents { get; set; }

        [Required]
        public Guid SupervisorID { get; set; }

    }
}
