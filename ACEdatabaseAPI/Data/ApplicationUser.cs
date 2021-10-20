using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Name Length Exceeded")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Name Length Exceeded")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Name Length Exceeded")]
        public string OtherName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name Length Exceeded")]
        public string FormerName { get; set; }
        [Required]
        [MaxLength(11, ErrorMessage = "Your NIN should not be more than 11 digits")]
        public string NIN { get; set; }
        public string JambRegNumber { get; set; }
        public string MatricNumber { get; set; }
        public string StaffID { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string ModeOfAdmission { get; set; }
        public Guid StudentCategory { get; set; }
        public Guid Programme { get; set; }
        public Guid EntryLevel { get; set; }
        public Guid CurrentLevel { get; set; }
        public Guid School { get; set; }
        public Guid Department { get; set; }
        public Guid StudentStatus { get; set; }
        public Guid MaritalStatus { get; set; }
        public Guid Religion { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid Gender { get; set; }
        public bool isDisabled { get; set; }
        public string Disability { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public string Status { get => "Active"; set { } }

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
        public string RolesCategory { get; set; }
        public bool ForcePasswordChange { get; set; }

    }
}
