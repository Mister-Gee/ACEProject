using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class RegisteredStudentChartLineDTO
    {
        public List<string> Session { get; set; }
        public GenderData MaleStudents { get; set; }
        public GenderData FemaleStudents { get; set; }
    }

    public class GenderData
    {
        public List<int> RegisteredStudentPerSession { get; set; }
        public string Label { get; set; }
    }
}
