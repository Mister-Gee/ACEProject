using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class FlagStudent
    {
        public Guid? Id { get; set; }
        public Guid StudentID { get; set; }

        public Guid FlagLevelID { get; set; }
        public string Status { get; set; }
        public Guid SecurityID { get; set; }
    }
}
