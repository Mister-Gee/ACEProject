using Microsoft.EntityFrameworkCore.Migrations;

namespace ACEdatabaseAPI.Migrations
{
    public partial class ApplicationUserUpdatedwITHbIOMETRICS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LeftThumbFingerBiometrics",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RightThumbFingerBiometrics",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserImage",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeftThumbFingerBiometrics",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RightThumbFingerBiometrics",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserImage",
                table: "AspNetUsers");
        }
    }
}
