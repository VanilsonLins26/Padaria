using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Padaria.Migrations
{
    /// <inheritdoc />
    public partial class v10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataEntrega",
                table: "Conta",
                newName: "DataPedido");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataPedido",
                table: "Conta",
                newName: "DataEntrega");
        }
    }
}
