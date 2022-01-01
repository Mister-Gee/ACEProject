using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities
{
    public class vStudent
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
        public Guid SchoolID { get; set; }
        public string Department { get; set; }
        public Guid DepartmentID { get; set; }
        public string PhoneNumber { get; set; }
        public string UserImageURL { get; set; }
        public string MaritalStatus { get; set; }
        public Guid MaritalStatusID { get; set; }
        public string Religion { get; set; }
        public Guid ReligionID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public Guid GenderID { get; set; }
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
        public DateTime AdmissionDate { get; set; }
        public string ModeOfAdmission { get; set; }
        public string StudentCategory { get; set; }
        public Guid StudentCategoryID { get; set; }
        public string Programme { get; set; }
        public Guid ProgrammeID { get; set; }

        //public string EntryLevel { get; set; }
        public string CurrentLevel { get; set; }
        public Guid CurrentLevelID { get; set; }
        public string JambRegNumber { get; set; }
        public string MatricNumber { get; set; }
    }
}
