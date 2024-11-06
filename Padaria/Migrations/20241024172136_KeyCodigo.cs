using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Padaria.Migrations
{
    /// <inheritdoc />
    public partial class KeyCodigo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosConta_Produto_ProdutoId",
                table: "ProdutosConta");

            migrationBuilder.DropIndex(
                name: "IX_ProdutosConta_ProdutoId",
                table: "ProdutosConta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Produto",
                table: "Produto");

            migrationBuilder.AddColumn<string>(
                name: "ProdutoCodigo",
                table: "ProdutosConta",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Produto",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Produto",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Produto",
                table: "Produto",
                column: "Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosConta_ProdutoCodigo",
                table: "ProdutosConta",
                column: "ProdutoCodigo");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosConta_Produto_ProdutoCodigo",
                table: "ProdutosConta",
                column: "ProdutoCodigo",
                principalTable: "Produto",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosConta_Produto_ProdutoCodigo",
                table: "ProdutosConta");

            migrationBuilder.DropIndex(
                name: "IX_ProdutosConta_ProdutoCodigo",
                table: "ProdutosConta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Produto",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "ProdutoCodigo",
                table: "ProdutosConta");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Produto",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Produto",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Produto",
                table: "Produto",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosConta_ProdutoId",
                table: "ProdutosConta",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosConta_Produto_ProdutoId",
                table: "ProdutosConta",
                column: "ProdutoId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
