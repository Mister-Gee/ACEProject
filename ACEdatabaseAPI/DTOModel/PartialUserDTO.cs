using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class PartialUserDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
