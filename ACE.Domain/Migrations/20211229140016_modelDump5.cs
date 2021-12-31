using Microsoft.EntityFrameworkCore.Migrations;

namespace ACE.Domain.Migrations
{
    public partial class modelDump5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamAttendances_AcademicYears_AcademicYearID",
                table: "ExamAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamAttendances_Courses_CourseID",
                table: "ExamAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamAttendances_Semesters_SemesterID",
                table: "ExamAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamRecords_AcademicYears_AcademicYearID",
                table: "ExamRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamRecords_Departments_DepartmentID",
                table: "ExamRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamRecords_Schools_SchoolID",
                table: "ExamRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamRecords_Semesters_SemesterID",
                table: "ExamRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Flags_FlagLevels_FlagLevelID",
                table: "Flags");

            migrationBuilder.DropIndex(
                name: "IX_Flags_FlagLevelID",
                table: "Flags");

            migrationBuilder.DropIndex(
                name: "IX_ExamRecords_AcademicYearID",
                table: "ExamRecords");

            migrationBuilder.DropIndex(
                name: "IX_ExamRecords_DepartmentID",
                table: "ExamRecords");

            migrationBuilder.DropIndex(
                name: "IX_ExamRecords_SchoolID",
                table: "ExamRecords");

            migrationBuilder.DropIndex(
                name: "IX_ExamRecords_SemesterID",
                table: "ExamRecords");

            migrationBuilder.DropIndex(
                name: "IX_ExamAttendances_AcademicYearID",
                table: "ExamAttendances");

            migrationBuilder.DropIndex(
                name: "IX_ExamAttendances_CourseID",
                table: "ExamAttendances");

            migrationBuilder.DropIndex(
                name: "IX_ExamAttendances_SemesterID",
                table: "ExamAttendances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Flags_FlagLevelID",
                table: "Flags",
                column: "FlagLevelID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ExamAttendances_AcademicYears_AcademicYearID",
                table: "ExamAttendances",
                column: "AcademicYearID",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamAttendances_Courses_CourseID",
                table: "ExamAttendances",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamAttendances_Semesters_SemesterID",
                table: "ExamAttendances",
                column: "SemesterID",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamRecords_AcademicYears_AcademicYearID",
                table: "ExamRecords",
                column: "AcademicYearID",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamRecords_Departments_DepartmentID",
                table: "ExamRecords",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamRecords_Schools_SchoolID",
                table: "ExamRecords",
                column: "SchoolID",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamRecords_Semesters_SemesterID",
                table: "ExamRecords",
                column: "SemesterID",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flags_FlagLevels_FlagLevelID",
                table: "Flags",
                column: "FlagLevelID",
                principalTable: "FlagLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
