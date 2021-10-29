using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Model
{
    public class SMSSettings
    {
        public string smsSender { get; set; }
        public string smsUsername { get; set; }
        public string smsApiKey { get; set; }
        public string smsPassword { get; set; } = "";
    }
}
