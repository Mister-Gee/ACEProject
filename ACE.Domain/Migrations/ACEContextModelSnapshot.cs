﻿// <auto-generated />
using System;
using ACE.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ACE.Domain.Migrations
{
    [DbContext(typeof(ACEContext))]
    partial class ACEContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("ACE.Domain.Entities.AuditLog", b =>
                {
                    b.Property<Guid>("AuditLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ColumnName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EventDateUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("EventType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ip")
                        .HasColumnType("TEXT");

                    b.Property<string>("NewValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("OriginalValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RecordId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TableName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("AuditLogId");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ClassAttendance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AcademicYearID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ClassAttendanceID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ClassDateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("ClassWeek")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CourseID")
                        .HasColumnType("TEXT");

                    b.Property<string>("PresentStudent")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SemesterID")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ClassAttendances");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.AcademicYear", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Year")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AcademicYears");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.BloodGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("BloodGroup");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SchoolID")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.FlagLevel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FlagLevels");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.Gender", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.Genotype", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genotype");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.GradingUnit", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("GradePoint")
                        .HasColumnType("INTEGER");

                    b.Property<char>("LetterGrade")
                        .HasColumnType("TEXT");

                    b.Property<string>("ScoreRange")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("GradingUnits");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.Level", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.MaritalStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MaritalStatus");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.MedicalCondition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MedicalConditions");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.Programme", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Programmes");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.Religion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Religions");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.School", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.Semester", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.StudentCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("StudentCategories");
                });

            modelBuilder.Entity("ACE.Domain.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AssistantLecturerID")
                        .HasColumnType("TEXT");

                    b.Property<string>("CourseCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("CourseDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("CourseTitle")
                        .HasColumnType("TEXT");

                    b.Property<int>("CourseUnit")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("DepartmentID")
                        .HasColumnType("TEXT");

                    b.Property<string>("EligibleDepartments")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("LeadLecturerID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OtherCourseLecturer")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SchoolID")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isDepartmental")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("isGeneral")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("isOptional")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("ACE.Domain.Entities.CourseRegisteration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AcademicYearID")
                        .HasColumnType("TEXT");

                    b.Property<int>("Courses")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RegDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SemesterID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("StudentID")
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalUnit")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("CourseRegisterations");
                });

            modelBuilder.Entity("ACE.Domain.Entities.CurrentAcademicSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AcademicYearID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SemesterID")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CurrentAcademicSessions");
                });

            modelBuilder.Entity("ACE.Domain.Entities.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DeviceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceToken")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("Logout")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ExamAttendance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AcademicYearID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CourseID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExamEndDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExamEndingStudents")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExamStartDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExamStartingStudents")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SemesterID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SupervisorID")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ExamAttendances");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ExamRecords", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AcademicYearID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CourseID")
                        .HasColumnType("TEXT");

                    b.Property<int>("CourseUnit")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("DepartmentID")
                        .HasColumnType("TEXT");

                    b.Property<double>("ExamScore")
                        .HasColumnType("REAL");

                    b.Property<int>("GradePoint")
                        .HasColumnType("INTEGER");

                    b.Property<char>("LetterGrade")
                        .HasColumnType("TEXT");

                    b.Property<double>("OtherAssessmentScore")
                        .HasColumnType("REAL");

                    b.Property<Guid>("SchoolID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SemesterID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("StudentID")
                        .HasColumnType("TEXT");

                    b.Property<double>("TotalScore")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("ExamRecords");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ExamTimetable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AcademicYearID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CourseID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExamDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExamDuration")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExamStartTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SemesterID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SupervisorID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Venue")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ExamTimetables");
                });

            modelBuilder.Entity("ACE.Domain.Entities.Flag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FlagLevelID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SecurityID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("StudentID")
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalFlags")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Flags");
                });

            modelBuilder.Entity("ACE.Domain.Entities.MedicalHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AdditionDoctorsNote")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Doctor")
                        .HasColumnType("TEXT");

                    b.Property<string>("FinalDiagnosis")
                        .HasColumnType("TEXT");

                    b.Property<string>("InitialDiagnosis")
                        .HasColumnType("TEXT");

                    b.Property<string>("TreatmentPlan")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserID")
                        .HasColumnType("TEXT");

                    b.Property<string>("VitalSign")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MedicalHistory");
                });

            modelBuilder.Entity("ACE.Domain.Entities.MedicalRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AdditionalNote")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BloodGroupID")
                        .HasColumnType("TEXT");

                    b.Property<string>("FamilyDoctorName")
                        .HasColumnType("TEXT");

                    b.Property<string>("FamilyDoctorPhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GenotypeID")
                        .HasColumnType("TEXT");

                    b.Property<double>("Height")
                        .HasColumnType("REAL");

                    b.Property<string>("MedicalConditions")
                        .HasColumnType("TEXT");

                    b.Property<string>("OtherMedicalConditions")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("MedicalRecords");
                });

            modelBuilder.Entity("ACE.Domain.Entities.StudentRegisteredCourse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AcademicYearID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CourseID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CourseRegisterationDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CourseRegisterationID")
                        .HasColumnType("TEXT");

                    b.Property<int>("CourseUnit")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("SemesterID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("StudentRegisteredCourses");
                });
#pragma warning restore 612, 618
        }
    }
}
