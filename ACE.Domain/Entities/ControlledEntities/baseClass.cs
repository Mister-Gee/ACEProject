using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities.ControlledEntities
{
    public class baseClass
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
