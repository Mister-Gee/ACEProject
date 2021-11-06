using ACE.Domain.Abstract.IControlledRepo;
using ACEdatabaseAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Helpers.ExamGradeService
{
    public class ExamGradingService : IExamGradingService
    {
        IGradingUnitRepo _gradingUnitRepo;

        
        public ExamGradingService(IGradingUnitRepo gradingUnitRepo)
        {
            _gradingUnitRepo = gradingUnitRepo;
        }

        public GradeVM GetLetterGradeAndGradePoint(double totalScore)
        {
            var gradeVM = new GradeVM();
            var result = _gradingUnitRepo.GetAll().ToList();
            foreach(var gradingUnit in result)
            {
                List<int> scoreRangeList = JsonConvert.DeserializeObject<List<int>>(gradingUnit.ScoreRange);
                if (scoreRangeList.Contains((int)totalScore))
                {
                    gradeVM.LetterGrade = gradingUnit.LetterGrade;
                    gradeVM.GradePoint = gradingUnit.GradePoint;
                    break;

                }
            }
            return gradeVM;
        }
    }

    
}
