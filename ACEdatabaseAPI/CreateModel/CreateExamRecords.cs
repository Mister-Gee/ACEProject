using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class CreateExamRecords
    {
        [Required]
        public Guid StudentID { get; set; }

        [Required]
        public double ExamScore { get; set; }
        public double OtherAssessmentScore { get; set; }

    }
}
