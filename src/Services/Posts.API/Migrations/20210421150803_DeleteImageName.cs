using Microsoft.EntityFrameworkCore.Migrations;

namespace Posts.API.Migrations
{
    public partial class DeleteImageName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Post");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Post",
                type: "text",
                nullable: true);
        }
    }
}
