using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Model
{
    public class DeviceVM
    {
        public string username { get; set; }
        public string deviceToken { get; set; }
        public string deviceId { get; set; }
        public string appName { get; set; }
    }
}
