using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Padaria.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Conta_ContaId",
                table: "Conta");

            migrationBuilder.DropIndex(
                name: "IX_Conta_ContaId",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "ContaId",
                table: "Conta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContaId",
                table: "Conta",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conta_ContaId",
                table: "Conta",
                column: "ContaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Conta_ContaId",
                table: "Conta",
                column: "ContaId",
                principalTable: "Conta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
