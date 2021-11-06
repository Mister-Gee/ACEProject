using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class GradingUnitDTO
    {
        public int StartingScore { get; set; }
        public int EndingScore { get; set; }
        public int GradePoint { get; set; }
        public char LetterGrade { get; set; }
    }
}
