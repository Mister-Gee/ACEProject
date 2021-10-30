using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ACEdatabaseAPI.Migrations
{
    public partial class ApplicationUserRestructedAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentCategory",
                table: "AspNetUsers",
                newName: "StudentCategoryID");

            migrationBuilder.RenameColumn(
                name: "School",
                table: "AspNetUsers",
                newName: "SchoolID");

            migrationBuilder.RenameColumn(
                name: "Religion",
                table: "AspNetUsers",
                newName: "ReligionID");

            migrationBuilder.RenameColumn(
                name: "Programme",
                table: "AspNetUsers",
                newName: "ProgrammeID");

            migrationBuilder.RenameColumn(
                name: "MaritalStatus",
                table: "AspNetUsers",
                newName: "MaritalStatusID");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "AspNetUsers",
                newName: "GenderID");

            migrationBuilder.RenameColumn(
                name: "EntryLevel",
                table: "AspNetUsers",
                newName: "EntryLevelID");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "AspNetUsers",
                newName: "DepartmentID");

            migrationBuilder.RenameColumn(
                name: "CurrentLevel",
                table: "AspNetUsers",
                newName: "CurrentLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartmentID",
                table: "AspNetUsers",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EntryLevelID",
                table: "AspNetUsers",
                column: "EntryLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GenderID",
                table: "AspNetUsers",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MaritalStatusID",
                table: "AspNetUsers",
                column: "MaritalStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProgrammeID",
                table: "AspNetUsers",
                column: "ProgrammeID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ReligionID",
                table: "AspNetUsers",
                column: "ReligionID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SchoolID",
                table: "AspNetUsers",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudentCategoryID",
                table: "AspNetUsers",
                column: "StudentCategoryID");


            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentID",
                table: "AspNetUsers",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Genders_GenderID",
                table: "AspNetUsers",
                column: "GenderID",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Levels_EntryLevelID",
                table: "AspNetUsers",
                column: "EntryLevelID",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MaritalStatus_MaritalStatusID",
                table: "AspNetUsers",
                column: "MaritalStatusID",
                principalTable: "MaritalStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Programmes_ProgrammeID",
                table: "AspNetUsers",
                column: "ProgrammeID",
                principalTable: "Programmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Religions_ReligionID",
                table: "AspNetUsers",
                column: "ReligionID",
                principalTable: "Religions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Schools_SchoolID",
                table: "AspNetUsers",
                column: "SchoolID",
                principalTable: "Schools",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_StudentCategories_StudentCategoryID",
                table: "AspNetUsers",
                column: "StudentCategoryID",
                principalTable: "StudentCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Department_DepartmentID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Gender_GenderID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Level_EntryLevelID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MaritalStatus_MaritalStatusID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Programme_ProgrammeID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Religion_ReligionID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_School_SchoolID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_StudentCategory_StudentCategoryID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Level");

            migrationBuilder.DropTable(
                name: "MaritalStatus");

            migrationBuilder.DropTable(
                name: "Programme");

            migrationBuilder.DropTable(
                name: "Religion");

            migrationBuilder.DropTable(
                name: "StudentCategory");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DepartmentID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EntryLevelID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GenderID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MaritalStatusID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProgrammeID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ReligionID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SchoolID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudentCategoryID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "StudentCategoryID",
                table: "AspNetUsers",
                newName: "StudentCategory");

            migrationBuilder.RenameColumn(
                name: "SchoolID",
                table: "AspNetUsers",
                newName: "School");

            migrationBuilder.RenameColumn(
                name: "ReligionID",
                table: "AspNetUsers",
                newName: "Religion");

            migrationBuilder.RenameColumn(
                name: "ProgrammeID",
                table: "AspNetUsers",
                newName: "Programme");

            migrationBuilder.RenameColumn(
                name: "MaritalStatusID",
                table: "AspNetUsers",
                newName: "MaritalStatus");

            migrationBuilder.RenameColumn(
                name: "GenderID",
                table: "AspNetUsers",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "EntryLevelID",
                table: "AspNetUsers",
                newName: "EntryLevel");

            migrationBuilder.RenameColumn(
                name: "DepartmentID",
                table: "AspNetUsers",
                newName: "Department");

            migrationBuilder.RenameColumn(
                name: "CurrentLevelID",
                table: "AspNetUsers",
                newName: "CurrentLevel");
        }
    }
}
