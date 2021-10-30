using ACE.Domain.Entities.ControlledEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities
{
    public class Flag
    {
        [Key]
        public Guid Id { get; set; }

        public Guid StudentID { get; set; }

        [ForeignKey("FlagLevel")]
        public Guid FlagLevelID { get; set; }
        public FlagLevel FlagLevel { get; set; }
    }
}
