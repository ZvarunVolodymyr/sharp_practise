using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Certificates",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "Certificates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_userId",
                table: "Certificates",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_AspNetUsers_userId",
                table: "Certificates",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_AspNetUsers_userId",
                table: "Certificates");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_userId",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Certificates");
        }
    }
}
