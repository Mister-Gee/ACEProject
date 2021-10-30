using Microsoft.EntityFrameworkCore.Migrations;

namespace ACEdatabaseAPI.Migrations
{
    public partial class ApplicationUserUpdatedwITHbIOMETRICS2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserImage",
                table: "AspNetUsers",
                newName: "UserImageURL");

            migrationBuilder.AddColumn<long>(
                name: "UserImageData",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserImageData",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserImageURL",
                table: "AspNetUsers",
                newName: "UserImage");
        }
    }
}
