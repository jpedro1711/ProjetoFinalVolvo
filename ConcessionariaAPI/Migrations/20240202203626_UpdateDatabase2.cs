using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcessionariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Veiculo_NumeroChassi",
                table: "Veiculo");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroChassi",
                table: "Veiculo",
                type: "nvarchar(17)",
                maxLength: 17,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(17)",
                oldMaxLength: 17,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_NumeroChassi",
                table: "Veiculo",
                column: "NumeroChassi",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Veiculo_NumeroChassi",
                table: "Veiculo");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroChassi",
                table: "Veiculo",
                type: "nvarchar(17)",
                maxLength: 17,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(17)",
                oldMaxLength: 17);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_NumeroChassi",
                table: "Veiculo",
                column: "NumeroChassi",
                unique: true,
                filter: "[NumeroChassi] IS NOT NULL");
        }
    }
}
