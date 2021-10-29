using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities.ControlledEntities
{
    public class Department : baseClass
    {
        [ForeignKey("School")]
        public Guid SchoolID { get; set; }
        public School School { get; set; }

    }
}
