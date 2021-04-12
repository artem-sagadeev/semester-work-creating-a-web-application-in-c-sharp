using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SubscriptionAPI.Migrations
{
    public partial class q : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaidSubscriptions_Tariffs_TariffId",
                table: "PaidSubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaidSubscriptions",
                table: "PaidSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_PaidSubscriptions_TariffId",
                table: "PaidSubscriptions");

            migrationBuilder.DropColumn(
                name: "TariffId",
                table: "PaidSubscriptions");

            migrationBuilder.RenameColumn(
                name: "Tye",
                table: "TypeOfSubscriptions",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "PricePerMounth",
                table: "Tariffs",
                newName: "PricePerMonth");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PaidSubscriptions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Tariff",
                table: "PaidSubscriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaidSubscriptions",
                table: "PaidSubscriptions",
                columns: new[] { "UserId", "SubscribedToId", "Tariff" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PaidSubscriptions",
                table: "PaidSubscriptions");

            migrationBuilder.DropColumn(
                name: "Tariff",
                table: "PaidSubscriptions");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "TypeOfSubscriptions",
                newName: "Tye");

            migrationBuilder.RenameColumn(
                name: "PricePerMonth",
                table: "Tariffs",
                newName: "PricePerMounth");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PaidSubscriptions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "TariffId",
                table: "PaidSubscriptions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaidSubscriptions",
                table: "PaidSubscriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaidSubscriptions_TariffId",
                table: "PaidSubscriptions",
                column: "TariffId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaidSubscriptions_Tariffs_TariffId",
                table: "PaidSubscriptions",
                column: "TariffId",
                principalTable: "Tariffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
