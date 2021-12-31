using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ACE.Domain.Migrations
{
    public partial class modelDump3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassAttendances_AcademicYears_AcademicYearID",
                table: "ClassAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassAttendances_Courses_CourseID",
                table: "ClassAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassAttendances_Semesters_SemesterID",
                table: "ClassAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamTimetables_AcademicYears_AcademicYearID",
                table: "ExamTimetables");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamTimetables_Courses_CourseID",
                table: "ExamTimetables");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamTimetables_Semesters_SemesterID",
                table: "ExamTimetables");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_BloodGroup_BloodGroupID",
                table: "MedicalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Genotype_GenotypeID",
                table: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "MedicalConditions");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_BloodGroupID",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_GenotypeID",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_ExamTimetables_AcademicYearID",
                table: "ExamTimetables");

            migrationBuilder.DropIndex(
                name: "IX_ExamTimetables_CourseID",
                table: "ExamTimetables");

            migrationBuilder.DropIndex(
                name: "IX_ExamTimetables_SemesterID",
                table: "ExamTimetables");

            migrationBuilder.DropIndex(
                name: "IX_ClassAttendances_AcademicYearID",
                table: "ClassAttendances");

            migrationBuilder.DropIndex(
                name: "IX_ClassAttendances_CourseID",
                table: "ClassAttendances");

            migrationBuilder.DropIndex(
                name: "IX_ClassAttendances_SemesterID",
                table: "ClassAttendances");

            migrationBuilder.DropColumn(
                name: "MedicalHistory",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "AcademicYearID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SemesterID",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "OtherMedicalHistorys",
                table: "MedicalRecords",
                newName: "AdditionalNote");

            migrationBuilder.AlterColumn<Guid>(
                name: "OtherCourseLecturer",
                table: "Courses",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EligibleDepartments",
                table: "Courses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isGeneral",
                table: "Courses",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ClassAttendanceID",
                table: "ClassAttendances",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MedicalHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InitialDiagnosis = table.Column<string>(type: "TEXT", nullable: true),
                    FinalDiagnosis = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    TreatmentPlan = table.Column<string>(type: "TEXT", nullable: true),
                    Doctor = table.Column<string>(type: "TEXT", nullable: true),
                    VitalSign = table.Column<string>(type: "TEXT", nullable: true),
                    AdditionDoctorsNote = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalHistory");

            migrationBuilder.DropColumn(
                name: "EligibleDepartments",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "isGeneral",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ClassAttendanceID",
                table: "ClassAttendances");

            migrationBuilder.RenameColumn(
                name: "AdditionalNote",
                table: "MedicalRecords",
                newName: "OtherMedicalHistorys");

            migrationBuilder.AddColumn<string>(
                name: "MedicalHistory",
                table: "MedicalRecords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OtherCourseLecturer",
                table: "Courses",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "AcademicYearID",
                table: "Courses",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SemesterID",
                table: "Courses",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_BloodGroupID",
                table: "MedicalRecords",
                column: "BloodGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_GenotypeID",
                table: "MedicalRecords",
                column: "GenotypeID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAttendances_AcademicYears_AcademicYearID",
                table: "ClassAttendances",
                column: "AcademicYearID",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAttendances_Courses_CourseID",
                table: "ClassAttendances",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAttendances_Semesters_SemesterID",
                table: "ClassAttendances",
                column: "SemesterID",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamTimetables_AcademicYears_AcademicYearID",
                table: "ExamTimetables",
                column: "AcademicYearID",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamTimetables_Courses_CourseID",
                table: "ExamTimetables",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamTimetables_Semesters_SemesterID",
                table: "ExamTimetables",
                column: "SemesterID",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_BloodGroup_BloodGroupID",
                table: "MedicalRecords",
                column: "BloodGroupID",
                principalTable: "BloodGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Genotype_GenotypeID",
                table: "MedicalRecords",
                column: "GenotypeID",
                principalTable: "Genotype",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
