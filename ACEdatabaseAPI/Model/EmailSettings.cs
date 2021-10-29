using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Model
{
    public class EmailSettings
    {
        public string support { get; set; }
        public string MailSender { get; set; }
        public string UserName { get; set; }
        public string MailBcc { get; set; }
        public bool UseSsl { get; set; }
        public string Mailpass { get; set; }
        public string MailHost { get; set; }
        public int ServerPort { get; set; }
        public bool WriteAsFile { get; set; }
        public string FileLocation { get; set; }
    }
}
