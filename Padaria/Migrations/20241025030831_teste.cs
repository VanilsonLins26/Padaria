using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Padaria.Migrations
{
    /// <inheritdoc />
    public partial class teste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosConta_Conta_ContaId",
                table: "ProdutosConta");

            migrationBuilder.DropIndex(
                name: "IX_ProdutosConta_ContaId",
                table: "ProdutosConta");

            migrationBuilder.CreateTable(
                name: "ContaProdutoConta",
                columns: table => new
                {
                    ContasId = table.Column<int>(type: "int", nullable: false),
                    ProdutosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaProdutoConta", x => new { x.ContasId, x.ProdutosId });
                    table.ForeignKey(
                        name: "FK_ContaProdutoConta_Conta_ContasId",
                        column: x => x.ContasId,
                        principalTable: "Conta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContaProdutoConta_ProdutosConta_ProdutosId",
                        column: x => x.ProdutosId,
                        principalTable: "ProdutosConta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ContaProdutoConta_ProdutosId",
                table: "ContaProdutoConta",
                column: "ProdutosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContaProdutoConta");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosConta_ContaId",
                table: "ProdutosConta",
                column: "ContaId");

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
