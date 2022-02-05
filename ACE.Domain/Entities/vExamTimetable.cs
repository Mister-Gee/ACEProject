﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Entities
{
    public class vExamTimetable
    {
        public Guid Id { get; set; }
        public Guid CourseID { get; set; }
        public string Course { get; set; }
        public string Department { get; set; }
        public string EligibleDepartments { get; set; }
        public string ExamStartTime { get; set; }
        public string ExamDuration { get; set; }
        public DateTime ExamDateTime { get; set; }
        public string Venue { get; set; }
        public Guid SupervisorID { get; set; }
        public string Supervisor { get; set; }
        public Guid AcademicYearID { get; set; }
        public string AcademicYear { get; set; }
        public Guid SemesterID { get; set; }
        public string Semester { get; set; }

    }
}
