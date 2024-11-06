using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Padaria.Migrations
{
    /// <inheritdoc />
    public partial class attProdutosConta3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "ProdutosConta",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "ProdutosConta");
        }
    }
}
