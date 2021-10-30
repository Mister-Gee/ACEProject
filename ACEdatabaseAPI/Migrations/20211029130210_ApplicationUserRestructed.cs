using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ACEdatabaseAPI.Migrations
{
    public partial class ApplicationUserRestructed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CurrentLevelObjId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EntryLevelObjId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GenderObjId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MaritalStatusObjId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProgrammeObjId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ReligionObjId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SchoolObjId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudentCategoryObjId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrentLevelObjId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EntryLevelObjId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GenderObjId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MaritalStatusObjId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProgrammeObjId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReligionObjId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SchoolObjId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudentCategoryObjId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentLevelObjId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntryLevelObjId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GenderObjId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MaritalStatusObjId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgrammeObjId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReligionObjId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolObjId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentCategoryObjId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Programme",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Religion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchoolID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_School_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CurrentLevelObjId",
                table: "AspNetUsers",
                column: "CurrentLevelObjId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EntryLevelObjId",
                table: "AspNetUsers",
                column: "EntryLevelObjId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GenderObjId",
                table: "AspNetUsers",
                column: "GenderObjId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MaritalStatusObjId",
                table: "AspNetUsers",
                column: "MaritalStatusObjId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProgrammeObjId",
                table: "AspNetUsers",
                column: "ProgrammeObjId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ReligionObjId",
                table: "AspNetUsers",
                column: "ReligionObjId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SchoolObjId",
                table: "AspNetUsers",
                column: "SchoolObjId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudentCategoryObjId",
                table: "AspNetUsers",
                column: "StudentCategoryObjId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_SchoolID",
                table: "Department",
                column: "SchoolID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Gender_GenderObjId",
                table: "AspNetUsers",
                column: "GenderObjId",
                principalTable: "Gender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Level_CurrentLevelObjId",
                table: "AspNetUsers",
                column: "CurrentLevelObjId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Level_EntryLevelObjId",
                table: "AspNetUsers",
                column: "EntryLevelObjId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MaritalStatus_MaritalStatusObjId",
                table: "AspNetUsers",
                column: "MaritalStatusObjId",
                principalTable: "MaritalStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Programme_ProgrammeObjId",
                table: "AspNetUsers",
                column: "ProgrammeObjId",
                principalTable: "Programme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Religion_ReligionObjId",
                table: "AspNetUsers",
                column: "ReligionObjId",
                principalTable: "Religion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_School_SchoolObjId",
                table: "AspNetUsers",
                column: "SchoolObjId",
                principalTable: "School",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_StudentCategory_StudentCategoryObjId",
                table: "AspNetUsers",
                column: "StudentCategoryObjId",
                principalTable: "StudentCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
