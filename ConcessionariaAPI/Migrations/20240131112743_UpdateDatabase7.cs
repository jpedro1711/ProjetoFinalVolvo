using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcessionariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Proprietario_ProprietarioId",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Vendedor_VendedorId",
                table: "Endereco");

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

            migrationBuilder.CreateTable(
                name: "EnderecoProprietario",
                columns: table => new
                {
                    EnderecosEnderecoId = table.Column<int>(type: "int", nullable: false),
                    ProprietariosProprietarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnderecoProprietario", x => new { x.EnderecosEnderecoId, x.ProprietariosProprietarioId });
                    table.ForeignKey(
                        name: "FK_EnderecoProprietario_Endereco_EnderecosEnderecoId",
                        column: x => x.EnderecosEnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnderecoProprietario_Proprietario_ProprietariosProprietarioId",
                        column: x => x.ProprietariosProprietarioId,
                        principalTable: "Proprietario",
                        principalColumn: "ProprietarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnderecoVendedor",
                columns: table => new
                {
                    EnderecosEnderecoId = table.Column<int>(type: "int", nullable: false),
                    VendedoresVendedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnderecoVendedor", x => new { x.EnderecosEnderecoId, x.VendedoresVendedorId });
                    table.ForeignKey(
                        name: "FK_EnderecoVendedor_Endereco_EnderecosEnderecoId",
                        column: x => x.EnderecosEnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnderecoVendedor_Vendedor_VendedoresVendedorId",
                        column: x => x.VendedoresVendedorId,
                        principalTable: "Vendedor",
                        principalColumn: "VendedorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnderecoProprietario_ProprietariosProprietarioId",
                table: "EnderecoProprietario",
                column: "ProprietariosProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EnderecoVendedor_VendedoresVendedorId",
                table: "EnderecoVendedor",
                column: "VendedoresVendedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnderecoProprietario");

            migrationBuilder.DropTable(
                name: "EnderecoVendedor");

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

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_ProprietarioId",
                table: "Endereco",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_VendedorId",
                table: "Endereco",
                column: "VendedorId");

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
    }
}
