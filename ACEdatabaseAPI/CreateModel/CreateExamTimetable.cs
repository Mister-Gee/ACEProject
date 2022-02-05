using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class CreateExamTimetable
    {
        [Required]
        public Guid CourseID { get; set; }

        [Required]
        public DateTime ExamDateTime { get; set; }

        [Required]
        public Guid SupervisorID { get; set; }
        public string ExamStartTime { get; set; }
        public string ExamDuration { get; set; }
        public string Venue { get; set; }
    }
}
