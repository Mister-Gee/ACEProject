using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities.ControlledEntities
{
    public class GradingUnit
    {
        public Guid ID { get; set; }
        public string ScoreRange { get; set; }
        public int GradePoint { get; set; }
        public char LetterGrade { get; set; }
    }
}
