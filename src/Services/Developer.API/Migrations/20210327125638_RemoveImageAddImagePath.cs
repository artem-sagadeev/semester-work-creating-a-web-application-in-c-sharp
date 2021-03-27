using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Developer.API.Migrations
{
    public partial class RemoveImageAddImagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Image_ImageId",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Image_ImageId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Image_ImageId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropIndex(
                name: "IX_User_ImageId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Project_ImageId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Company_ImageId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Company");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Project",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Company",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Company");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "User",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Project",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Company",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_ImageId",
                table: "User",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ImageId",
                table: "Project",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_ImageId",
                table: "Company",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Image_ImageId",
                table: "Company",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Image_ImageId",
                table: "Project",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Image_ImageId",
                table: "User",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
