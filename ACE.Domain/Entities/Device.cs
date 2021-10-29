using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string DeviceId { get; set; }
        public string DeviceToken { get; set; }
        public bool? Logout { get; set; }
    }
}
