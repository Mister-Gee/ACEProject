using Microsoft.EntityFrameworkCore.Migrations;

namespace ACE.Domain.Migrations
{
    public partial class modelDump8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Departments_Schools_SchoolID",
            //    table: "Departments");

            //migrationBuilder.DropIndex(
            //    name: "IX_Departments_SchoolID",
            //    table: "Departments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Departments_SchoolID",
                table: "Departments",
                column: "SchoolID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Schools_SchoolID",
                table: "Departments",
                column: "SchoolID",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
