using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Padaria.Migrations
{
    /// <inheritdoc />
    public partial class AttProdutosConta2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ValorTotal",
                table: "Conta",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "Conta");
        }
    }
}
