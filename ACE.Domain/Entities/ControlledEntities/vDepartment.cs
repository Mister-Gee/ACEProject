using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities.ControlledEntities
{
    public class vDepartment : baseClass
    {
        public Guid SchoolID { get; set; }
        public string School { get; set; }
    }
}
