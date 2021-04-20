using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SubscriptionAPI.Migrations
{
    public partial class CovertInoEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tariffs_TypeOfSubscriptions_TypeOfSubscriptionId",
                table: "Tariffs");

            migrationBuilder.DropTable(
                name: "TypeOfSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Tariffs_TypeOfSubscriptionId",
                table: "Tariffs");

            migrationBuilder.DropColumn(
                name: "TypeOfSubscriptionId",
                table: "Tariffs");

            migrationBuilder.AddColumn<int>(
                name: "PriceType",
                table: "Tariffs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfSubscription",
                table: "Tariffs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceType",
                table: "Tariffs");

            migrationBuilder.DropColumn(
                name: "TypeOfSubscription",
                table: "Tariffs");

            migrationBuilder.AddColumn<int>(
                name: "TypeOfSubscriptionId",
                table: "Tariffs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TypeOfSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfSubscriptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_TypeOfSubscriptionId",
                table: "Tariffs",
                column: "TypeOfSubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tariffs_TypeOfSubscriptions_TypeOfSubscriptionId",
                table: "Tariffs",
                column: "TypeOfSubscriptionId",
                principalTable: "TypeOfSubscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
