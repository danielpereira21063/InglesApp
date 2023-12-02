using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InglesApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ColunaInativo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Inativo",
                table: "Vocabularios",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inativo",
                table: "Vocabularios");
        }
    }
}
