using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Padaria.Migrations
{
    /// <inheritdoc />
    public partial class _111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosConta_Conta_ContaId",
                table: "ProdutosConta");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosConta_Conta_ContaId",
                table: "ProdutosConta",
                column: "ContaId",
                principalTable: "Conta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosConta_Conta_ContaId",
                table: "ProdutosConta");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosConta_Conta_ContaId",
                table: "ProdutosConta",
                column: "ContaId",
                principalTable: "Conta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
