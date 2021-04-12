using Microsoft.EntityFrameworkCore.Migrations;

namespace SubscriptionAPI.Migrations
{
    public partial class A : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tariff",
                table: "PaidSubscriptions",
                newName: "TariffId");

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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaidSubscriptions_Tariffs_TariffId",
                table: "PaidSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_PaidSubscriptions_TariffId",
                table: "PaidSubscriptions");

            migrationBuilder.RenameColumn(
                name: "TariffId",
                table: "PaidSubscriptions",
                newName: "Tariff");
        }
    }
}
