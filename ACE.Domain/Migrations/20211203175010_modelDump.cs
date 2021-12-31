using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ACE.Domain.Migrations
{
    public partial class modelDump : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicYears",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Year = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    AuditLogId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    EventDateUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EventType = table.Column<string>(type: "TEXT", nullable: true),
                    TableName = table.Column<string>(type: "TEXT", nullable: true),
                    RecordId = table.Column<string>(type: "TEXT", nullable: true),
                    ColumnName = table.Column<string>(type: "TEXT", nullable: true),
                    OriginalValue = table.Column<string>(type: "TEXT", nullable: true),
                    NewValue = table.Column<string>(type: "TEXT", nullable: true),
                    Ip = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.AuditLogId);
                });

            migrationBuilder.CreateTable(
                name: "BloodGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    DeviceId = table.Column<string>(type: "TEXT", nullable: true),
                    DeviceToken = table.Column<string>(type: "TEXT", nullable: true),
                    Logout = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlagLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlagLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genotype",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genotype", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradingUnits",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ScoreRange = table.Column<string>(type: "TEXT", nullable: true),
                    GradePoint = table.Column<int>(type: "INTEGER", nullable: false),
                    LetterGrade = table.Column<char>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradingUnits", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalConditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Programmes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programmes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Religions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StudentID = table.Column<Guid>(type: "TEXT", nullable: false),
                    FlagLevelID = table.Column<Guid>(type: "TEXT", nullable: false),
                    TotalFlags = table.Column<int>(type: "INTEGER", nullable: false),
                    SecurityID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flags_FlagLevels_FlagLevelID",
                        column: x => x.FlagLevelID,
                        principalTable: "FlagLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MatricNumber = table.Column<string>(type: "TEXT", nullable: true),
                    StaffID = table.Column<string>(type: "TEXT", nullable: true),
                    BloodGroupID = table.Column<Guid>(type: "TEXT", nullable: false),
                    GenotypeID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    Height = table.Column<double>(type: "REAL", nullable: false),
                    FamilyDoctorName = table.Column<string>(type: "TEXT", nullable: true),
                    FamilyDoctorPhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    MedicalHistory = table.Column<string>(type: "TEXT", nullable: true),
                    OtherMedicalHistorys = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_BloodGroup_BloodGroupID",
                        column: x => x.BloodGroupID,
                        principalTable: "BloodGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Genotype_GenotypeID",
                        column: x => x.GenotypeID,
                        principalTable: "Genotype",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SchoolID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CourseTitle = table.Column<string>(type: "TEXT", nullable: true),
                    CourseCode = table.Column<string>(type: "TEXT", nullable: true),
                    CourseUnit = table.Column<int>(type: "INTEGER", nullable: false),
                    CourseDescription = table.Column<string>(type: "TEXT", nullable: true),
                    AcademicYearID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SemesterID = table.Column<Guid>(type: "TEXT", nullable: false),
                    LeadLecturerID = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssistantLecturerID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SchoolID = table.Column<Guid>(type: "TEXT", nullable: false),
                    DepartmentID = table.Column<Guid>(type: "TEXT", nullable: false),
                    isDepartmental = table.Column<bool>(type: "INTEGER", nullable: false),
                    isOptional = table.Column<bool>(type: "INTEGER", nullable: false),
                    OtherCourseLecturer = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_AcademicYears_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Semesters_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StudentID = table.Column<Guid>(type: "TEXT", nullable: false),
                    CourseID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExamScore = table.Column<double>(type: "REAL", nullable: false),
                    OtherAssessmentScore = table.Column<double>(type: "REAL", nullable: false),
                    TotalScore = table.Column<double>(type: "REAL", nullable: false),
                    CourseUnit = table.Column<int>(type: "INTEGER", nullable: false),
                    LetterGrade = table.Column<char>(type: "TEXT", nullable: false),
                    GradePoint = table.Column<int>(type: "INTEGER", nullable: false),
                    AcademicYearID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SemesterID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SchoolID = table.Column<Guid>(type: "TEXT", nullable: false),
                    DepartmentID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamRecords_AcademicYears_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamRecords_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamRecords_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamRecords_Semesters_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassAttendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CourseID = table.Column<Guid>(type: "TEXT", nullable: false),
                    PresentStudent = table.Column<string>(type: "TEXT", nullable: true),
                    ClassDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClassWeek = table.Column<int>(type: "INTEGER", nullable: false),
                    AcademicYearID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SemesterID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassAttendances_AcademicYears_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassAttendances_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassAttendances_Semesters_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamAttendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CourseID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExamStartDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExamEndDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExamStartingStudents = table.Column<string>(type: "TEXT", nullable: true),
                    ExamEndingStudents = table.Column<string>(type: "TEXT", nullable: true),
                    SupervisorID = table.Column<Guid>(type: "TEXT", nullable: false),
                    AcademicYearID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SemesterID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamAttendances_AcademicYears_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamAttendances_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamAttendances_Semesters_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamTimetables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CourseID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExamStartTime = table.Column<string>(type: "TEXT", nullable: true),
                    ExamDuration = table.Column<string>(type: "TEXT", nullable: true),
                    ExamDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Venue = table.Column<string>(type: "TEXT", nullable: true),
                    SupervisorID = table.Column<Guid>(type: "TEXT", nullable: false),
                    AcademicYearID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SemesterID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTimetables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamTimetables_AcademicYears_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamTimetables_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamTimetables_Semesters_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentRegisteredCourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StudentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StudentMatricNumber = table.Column<string>(type: "TEXT", nullable: true),
                    CourseID = table.Column<Guid>(type: "TEXT", nullable: false),
                    AcademicYearID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SemesterID = table.Column<Guid>(type: "TEXT", nullable: false),
                    CourseRegisterationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentRegisteredCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentRegisteredCourses_AcademicYears_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentRegisteredCourses_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentRegisteredCourses_Semesters_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassAttendances_AcademicYearID",
                table: "ClassAttendances",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassAttendances_CourseID",
                table: "ClassAttendances",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassAttendances_SemesterID",
                table: "ClassAttendances",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AcademicYearID",
                table: "Courses",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentID",
                table: "Courses",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SchoolID",
                table: "Courses",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SemesterID",
                table: "Courses",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_SchoolID",
                table: "Departments",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAttendances_AcademicYearID",
                table: "ExamAttendances",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAttendances_CourseID",
                table: "ExamAttendances",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAttendances_SemesterID",
                table: "ExamAttendances",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRecords_AcademicYearID",
                table: "ExamRecords",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRecords_DepartmentID",
                table: "ExamRecords",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRecords_SchoolID",
                table: "ExamRecords",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRecords_SemesterID",
                table: "ExamRecords",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTimetables_AcademicYearID",
                table: "ExamTimetables",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTimetables_CourseID",
                table: "ExamTimetables",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTimetables_SemesterID",
                table: "ExamTimetables",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_Flags_FlagLevelID",
                table: "Flags",
                column: "FlagLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_BloodGroupID",
                table: "MedicalRecords",
                column: "BloodGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_GenotypeID",
                table: "MedicalRecords",
                column: "GenotypeID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentRegisteredCourses_AcademicYearID",
                table: "StudentRegisteredCourses",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentRegisteredCourses_CourseID",
                table: "StudentRegisteredCourses",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentRegisteredCourses_SemesterID",
                table: "StudentRegisteredCourses",
                column: "SemesterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "ClassAttendances");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "ExamAttendances");

            migrationBuilder.DropTable(
                name: "ExamRecords");

            migrationBuilder.DropTable(
                name: "ExamTimetables");

            migrationBuilder.DropTable(
                name: "Flags");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "GradingUnits");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "MaritalStatus");

            migrationBuilder.DropTable(
                name: "MedicalConditions");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Programmes");

            migrationBuilder.DropTable(
                name: "Religions");

            migrationBuilder.DropTable(
                name: "StudentCategories");

            migrationBuilder.DropTable(
                name: "StudentRegisteredCourses");

            migrationBuilder.DropTable(
                name: "FlagLevels");

            migrationBuilder.DropTable(
                name: "BloodGroup");

            migrationBuilder.DropTable(
                name: "Genotype");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "AcademicYears");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
