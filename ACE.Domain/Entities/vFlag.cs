using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities
{
    public class vFlag
    {
        public Guid Id { get; set; }
        public Guid StudentID { get; set; }
        public string StudentName { get; set; }
        public Guid FlagLevelID { get; set; }
        public string FlagLevel { get; set; }
        public Guid SecurityID { get; set; }
        public string Security { get; set; }

    }
}
