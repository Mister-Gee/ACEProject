using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Model
{
    public class AppSettings
    {
        public string WebBaseUrl { get; set; }
        public bool DevelopmentMode { get; set; }
        public string DeveloperBaseUrl { get; set; }
        public string DeveloperAppName { get; set; }
        public EmailSettings EmailSetting { get; set; }
        public string GoogleAPIkey { get; set; }
        public SMSSettings SMSSettings { get; set; }
        public string[] MProperties { get; set; }
        public string DeliveryHour { get; set; }
        public string UsedDomains { get; set; }
        public string InterswitchPOSStatus { get; set; }
        public string MinDeveloperFundAmount { get; set; }
    }
}
