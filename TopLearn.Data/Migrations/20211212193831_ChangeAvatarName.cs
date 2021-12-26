using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.Data.Migrations
{
    public partial class ChangeAvatarName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserAvatar",
                table: "Users",
                newName: "AvatarName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvatarName",
                table: "Users",
                newName: "UserAvatar");
        }
    }
}
