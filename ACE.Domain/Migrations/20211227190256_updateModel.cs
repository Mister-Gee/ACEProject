using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ACE.Domain.Migrations
{
    public partial class updateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AcademicYears_AcademicYearID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Departments_DepartmentID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Schools_SchoolID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Semesters_SemesterID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentRegisteredCourses_AcademicYears_AcademicYearID",
                table: "StudentRegisteredCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentRegisteredCourses_Courses_CourseID",
                table: "StudentRegisteredCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentRegisteredCourses_Semesters_SemesterID",
                table: "StudentRegisteredCourses");

            migrationBuilder.DropIndex(
                name: "IX_StudentRegisteredCourses_AcademicYearID",
                table: "StudentRegisteredCourses");

            migrationBuilder.DropIndex(
                name: "IX_StudentRegisteredCourses_CourseID",
                table: "StudentRegisteredCourses");

            migrationBuilder.DropIndex(
                name: "IX_StudentRegisteredCourses_SemesterID",
                table: "StudentRegisteredCourses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_AcademicYearID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_DepartmentID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_SchoolID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_SemesterID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StudentMatricNumber",
                table: "StudentRegisteredCourses");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseRegisterationID",
                table: "StudentRegisteredCourses",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "CourseUnit",
                table: "StudentRegisteredCourses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CourseRegisterations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Courses = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentID = table.Column<Guid>(type: "TEXT", nullable: false),
                    AcademicYearID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SemesterID = table.Column<Guid>(type: "TEXT", nullable: false),
                    RegDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalUnit = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRegisterations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrentAcademicSession",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AcademicYearID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SemesterID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentAcademicSession", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseRegisterations");

            migrationBuilder.DropTable(
                name: "CurrentAcademicSession");

            migrationBuilder.DropColumn(
                name: "CourseRegisterationID",
                table: "StudentRegisteredCourses");

            migrationBuilder.DropColumn(
                name: "CourseUnit",
                table: "StudentRegisteredCourses");

            migrationBuilder.AddColumn<string>(
                name: "StudentMatricNumber",
                table: "StudentRegisteredCourses",
                type: "TEXT",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AcademicYears_AcademicYearID",
                table: "Courses",
                column: "AcademicYearID",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Departments_DepartmentID",
                table: "Courses",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Schools_SchoolID",
                table: "Courses",
                column: "SchoolID",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Semesters_SemesterID",
                table: "Courses",
                column: "SemesterID",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentRegisteredCourses_AcademicYears_AcademicYearID",
                table: "StudentRegisteredCourses",
                column: "AcademicYearID",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentRegisteredCourses_Courses_CourseID",
                table: "StudentRegisteredCourses",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentRegisteredCourses_Semesters_SemesterID",
                table: "StudentRegisteredCourses",
                column: "SemesterID",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
