using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InglesApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ColunaPraticaDeTraducaoTabelaPratica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PraticaDeTraducao",
                table: "Praticas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PraticaDeTraducao",
                table: "Praticas");
        }
    }
}
