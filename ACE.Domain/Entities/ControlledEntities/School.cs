using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities.ControlledEntities
{
    public class School : baseClass 
    {
        public List<Department> Departments { get; set; }
    }
}
