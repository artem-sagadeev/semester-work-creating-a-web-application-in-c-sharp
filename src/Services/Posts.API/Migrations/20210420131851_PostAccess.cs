using Microsoft.EntityFrameworkCore.Migrations;

namespace Posts.API.Migrations
{
    public partial class PostAccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequiredSubscriptionType",
                table: "Post",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredSubscriptionType",
                table: "Post");
        }
    }
}
