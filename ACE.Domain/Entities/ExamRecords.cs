using ACE.Domain.Entities.ControlledEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities
{
    public class ExamRecords
    { 
        [Key]
        public Guid Id { get; set; }
        public Guid StudentID { get; set; }
        public Guid CourseID { get; set; }
        public double ExamScore { get; set; }
        public double OtherAssessmentScore { get; set; }
        public double TotalScore { get; set; }
        public int CourseUnit { get; set; }
        public char LetterGrade { get; set; }
        public int GradePoint { get; set; }
        public Guid AcademicYearID { get; set; }
        public Guid SemesterID { get; set; }
        public Guid SchoolID { get; set; }
        public Guid DepartmentID { get; set; }
    }
}
