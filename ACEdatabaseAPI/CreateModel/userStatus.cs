using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class userActiveStatusEmail
    {
        public string UserEmail { get; set; }
        public bool ActiveStatus { get; set; }
    }

    public class userActiveStatusID
    {
        public string UserID { get; set; }
        public bool ActiveStatus { get; set; }
    }

    public class userActiveStatusStaffID
    {
        public string StaffID { get; set; }
        public bool ActiveStatus { get; set; }
    }

    public class userActiveStatusMatricNumber
    {
        public string MatricNumber { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
