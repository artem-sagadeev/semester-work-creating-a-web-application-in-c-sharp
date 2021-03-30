using Microsoft.EntityFrameworkCore.Migrations;

namespace SubscriptionAPI.Migrations
{
    public partial class fsf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tariffs_TypeOfSubscriptions_TypeId",
                table: "Tariffs");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Tariffs",
                newName: "TypeOfSubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Tariffs_TypeId",
                table: "Tariffs",
                newName: "IX_Tariffs_TypeOfSubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tariffs_TypeOfSubscriptions_TypeOfSubscriptionId",
                table: "Tariffs",
                column: "TypeOfSubscriptionId",
                principalTable: "TypeOfSubscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tariffs_TypeOfSubscriptions_TypeOfSubscriptionId",
                table: "Tariffs");

            migrationBuilder.RenameColumn(
                name: "TypeOfSubscriptionId",
                table: "Tariffs",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tariffs_TypeOfSubscriptionId",
                table: "Tariffs",
                newName: "IX_Tariffs_TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tariffs_TypeOfSubscriptions_TypeId",
                table: "Tariffs",
                column: "TypeId",
                principalTable: "TypeOfSubscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
