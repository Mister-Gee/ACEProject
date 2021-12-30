using ACE.Domain.Entities.ControlledEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name Length Exceeded")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name Length Exceeded")]
        public string LastName { get; set; }
        public string OtherName { get; set; }
        public string FormerName { get; set; }
        public string NIN { get; set; }
        public string JambRegNumber { get; set; }
        public string MatricNumber { get; set; }
        public string StaffID { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string ModeOfAdmission { get; set; }
        public Guid? StudentCategoryID { get; set; }
        public Guid? ProgrammeID { get; set; }
        public Guid? EntryLevelID { get; set; }
        public Guid? CurrentLevelID { get; set; }
        public Guid? DepartmentID { get; set; }
        public Guid? SchoolID { get; set; }
        public Guid? MaritalStatusID { get; set; }
        public Guid? ReligionID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid? GenderID { get; set; }
        public bool isDisabled { get; set; }
        public string Disability { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public string Status { get => "Active"; set { } }
        public string IPPISNo { get; set; }
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
        public DateTime Date { get; set; }
        public string UserImageURL { get; set; }
        public long UserImageData { get; set; }
        public string RightThumbFingerBiometrics { get; set; }
        public string LeftThumbFingerBiometrics { get; set; }

    }
}
