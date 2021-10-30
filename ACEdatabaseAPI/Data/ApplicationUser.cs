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

        [ForeignKey("StudentCategory")]
        public Guid? StudentCategoryID { get; set; }
        public StudentCategory StudentCategory { get; set; }

        [ForeignKey("Programme")]
        public Guid? ProgrammeID { get; set; }
        public Programme Programme { get; set; }

        [ForeignKey("Level")]
        public Guid? EntryLevelID { get; set; }
        public Guid? CurrentLevelID { get; set; }
        public Level Level { get; set; }

        [ForeignKey("Department")]
        public Guid? DepartmentID { get; set; }
        public Department Departments { get; set; }


        [ForeignKey("School")]
        public Guid? SchoolID { get; set; }
        public School School { get; set; }


        [ForeignKey("MaritalStatus")]
        public Guid? MaritalStatusID { get; set; }
        public MaritalStatus MaritalStatus { get; set; }


        [ForeignKey("Religion")]
        public Guid? ReligionID { get; set; }
        public Religion Religion { get; set; }

        public DateTime DateOfBirth { get; set; }

        [ForeignKey("Gender")]
        public Guid? GenderID { get; set; }
        public Gender Gender { get; set; }

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
        public DateTime Date { get; set; }
        public string UserImageURL { get; set; }
        public long UserImageData { get; set; }

        public string RightThumbFingerBiometrics { get; set; }
        public string LeftThumbFingerBiometrics { get; set; }

    }
}
