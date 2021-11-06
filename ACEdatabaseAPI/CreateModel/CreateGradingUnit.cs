using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class CreateGradingUnit
    {
        [Required]
        public int StartingScore { get; set; }

        [Required]
        public int EndingScore { get; set; }

        [Required]
        public int GradePoint { get; set; }

        [Required]
        public char LetterGrade { get; set; }
    }
}
