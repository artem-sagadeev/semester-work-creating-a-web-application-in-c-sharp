using Microsoft.EntityFrameworkCore.Migrations;

namespace SubscriptionAPI.Migrations
{
    public partial class SubscrAnPaymntApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tariffs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tariffs",
                type: "text",
                nullable: true);
        }
    }
}
