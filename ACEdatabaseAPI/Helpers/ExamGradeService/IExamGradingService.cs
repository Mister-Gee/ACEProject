using ACEdatabaseAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Helpers.ExamGradeService
{
    public interface IExamGradingService
    {
        GradeVM GetLetterGradeAndGradePoint(double totalScore);
        
    }
}
