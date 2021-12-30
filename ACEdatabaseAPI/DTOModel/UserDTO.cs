using ACE.Domain.Entities.ControlledEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string OtherName { get; set; }
        public string FormerName { get; set; }
        public string NIN { get; set; }
        public string Status { get; set; }
        public string School { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public string UserImageURL { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public bool isDisabled { get; set; }
        public string Disability { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public bool isIndigenous { get; set; }
        public string Nationality { get; set; }
        public string StateOfOrigin { get; set; }
        public string LG { get; set; }
        public string Hometown { get; set; }
        public string ZipPostalCode { get; set; }
        public string Address { get; set; }
        public string TwitterID { get; set; }
        public string FacebookID { get; set; }
        public string InstagramID { get; set; }
        public string LinkedInID { get; set; }
    }
}
