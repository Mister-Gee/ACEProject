using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class CourseEligibleStudentByDepartmentDTO
    {
        public string Department { get; set; }
        public List<StudentNameDTO> Students { get; set; }
    }
}
