using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.Data.Migrations
{
    public partial class AddIsDeletedForEpisodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CourseEpisodes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CourseEpisodes");
        }
    }
}
