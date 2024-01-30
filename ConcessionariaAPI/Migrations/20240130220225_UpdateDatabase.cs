using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcessionariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SalarioBase",
                table: "Vendedor",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_VendedorId",
                table: "Venda",
                column: "VendedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Vendedor_VendedorId",
                table: "Venda",
                column: "VendedorId",
                principalTable: "Vendedor",
                principalColumn: "VendedorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Vendedor_VendedorId",
                table: "Venda");

            migrationBuilder.DropIndex(
                name: "IX_Venda_VendedorId",
                table: "Venda");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalarioBase",
                table: "Vendedor",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2);
        }
    }
}
