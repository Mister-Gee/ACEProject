using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class StudentNameDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MatricNumber { get; set; }
    }
}
