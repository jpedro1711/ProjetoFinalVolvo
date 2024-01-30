using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcessionariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proprietario_Telefone_TelefoneId",
                table: "Proprietario");

            migrationBuilder.DropIndex(
                name: "IX_Proprietario_TelefoneId",
                table: "Proprietario");

            migrationBuilder.DropColumn(
                name: "TelefoneId",
                table: "Proprietario");

            migrationBuilder.AddColumn<int>(
                name: "ProprietarioId",
                table: "Endereco",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendedorId",
                table: "Endereco",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProprietarioTelefone",
                columns: table => new
                {
                    ProprietariosProprietarioId = table.Column<int>(type: "int", nullable: false),
                    TelefonesTelefoneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProprietarioTelefone", x => new { x.ProprietariosProprietarioId, x.TelefonesTelefoneId });
                    table.ForeignKey(
                        name: "FK_ProprietarioTelefone_Proprietario_ProprietariosProprietarioId",
                        column: x => x.ProprietariosProprietarioId,
                        principalTable: "Proprietario",
                        principalColumn: "ProprietarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProprietarioTelefone_Telefone_TelefonesTelefoneId",
                        column: x => x.TelefonesTelefoneId,
                        principalTable: "Telefone",
                        principalColumn: "TelefoneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelefoneVendedor",
                columns: table => new
                {
                    TelefonesTelefoneId = table.Column<int>(type: "int", nullable: false),
                    VendedoresVendedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelefoneVendedor", x => new { x.TelefonesTelefoneId, x.VendedoresVendedorId });
                    table.ForeignKey(
                        name: "FK_TelefoneVendedor_Telefone_TelefonesTelefoneId",
                        column: x => x.TelefonesTelefoneId,
                        principalTable: "Telefone",
                        principalColumn: "TelefoneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelefoneVendedor_Vendedor_VendedoresVendedorId",
                        column: x => x.VendedoresVendedorId,
                        principalTable: "Vendedor",
                        principalColumn: "VendedorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_ProprietarioId",
                table: "Endereco",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_VendedorId",
                table: "Endereco",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProprietarioTelefone_TelefonesTelefoneId",
                table: "ProprietarioTelefone",
                column: "TelefonesTelefoneId");

            migrationBuilder.CreateIndex(
                name: "IX_TelefoneVendedor_VendedoresVendedorId",
                table: "TelefoneVendedor",
                column: "VendedoresVendedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Proprietario_ProprietarioId",
                table: "Endereco",
                column: "ProprietarioId",
                principalTable: "Proprietario",
                principalColumn: "ProprietarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Vendedor_VendedorId",
                table: "Endereco",
                column: "VendedorId",
                principalTable: "Vendedor",
                principalColumn: "VendedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Proprietario_ProprietarioId",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Vendedor_VendedorId",
                table: "Endereco");

            migrationBuilder.DropTable(
                name: "ProprietarioTelefone");

            migrationBuilder.DropTable(
                name: "TelefoneVendedor");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_ProprietarioId",
                table: "Endereco");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_VendedorId",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "ProprietarioId",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "VendedorId",
                table: "Endereco");

            migrationBuilder.AddColumn<int>(
                name: "TelefoneId",
                table: "Proprietario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proprietario_TelefoneId",
                table: "Proprietario",
                column: "TelefoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proprietario_Telefone_TelefoneId",
                table: "Proprietario",
                column: "TelefoneId",
                principalTable: "Telefone",
                principalColumn: "TelefoneId");
        }
    }
}
