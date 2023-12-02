using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InglesApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjustesTabelaPraticas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Praticas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Praticas_UserId",
                table: "Praticas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Praticas_AspNetUsers_UserId",
                table: "Praticas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Praticas_AspNetUsers_UserId",
                table: "Praticas");

            migrationBuilder.DropIndex(
                name: "IX_Praticas_UserId",
                table: "Praticas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Praticas");
        }
    }
}
