using Microsoft.EntityFrameworkCore.Migrations;

namespace ACE.Domain.Migrations
{
    public partial class updateModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentAcademicSession",
                table: "CurrentAcademicSession");

            migrationBuilder.RenameTable(
                name: "CurrentAcademicSession",
                newName: "CurrentAcademicSessions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentAcademicSessions",
                table: "CurrentAcademicSessions",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentAcademicSessions",
                table: "CurrentAcademicSessions");

            migrationBuilder.RenameTable(
                name: "CurrentAcademicSessions",
                newName: "CurrentAcademicSession");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentAcademicSession",
                table: "CurrentAcademicSession",
                column: "Id");
        }
    }
}
