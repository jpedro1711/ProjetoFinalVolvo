using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcessionariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acessorio",
                columns: table => new
                {
                    AcessorioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acessorio", x => x.AcessorioID);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    EnderecoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rua = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.EnderecoId);
                });

            migrationBuilder.CreateTable(
                name: "Proprietario",
                columns: table => new
                {
                    ProprietarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    CNPJ = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietario", x => x.ProprietarioId);
                });

            migrationBuilder.CreateTable(
                name: "Telefone",
                columns: table => new
                {
                    TelefoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    NumeroTelefone = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefone", x => x.TelefoneId);
                });

            migrationBuilder.CreateTable(
                name: "Vendedor",
                columns: table => new
                {
                    VendedorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    SalarioBase = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAdmissao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedor", x => x.VendedorId);
                });

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
                name: "Veiculo",
                columns: table => new
                {
                    VeiculoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroChassi = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quilometragem = table.Column<int>(type: "int", nullable: false),
                    VersaoSistema = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ProprietarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.VeiculoId);
                    table.ForeignKey(
                        name: "FK_Veiculo_Proprietario_ProprietarioId",
                        column: x => x.ProprietarioId,
                        principalTable: "Proprietario",
                        principalColumn: "ProprietarioId");
                });

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

            migrationBuilder.CreateTable(
                name: "AcessorioVeiculo",
                columns: table => new
                {
                    AcessoriosAcessorioID = table.Column<int>(type: "int", nullable: false),
                    VeiculosVeiculoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcessorioVeiculo", x => new { x.AcessoriosAcessorioID, x.VeiculosVeiculoId });
                    table.ForeignKey(
                        name: "FK_AcessorioVeiculo_Acessorio_AcessoriosAcessorioID",
                        column: x => x.AcessoriosAcessorioID,
                        principalTable: "Acessorio",
                        principalColumn: "AcessorioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcessorioVeiculo_Veiculo_VeiculosVeiculoId",
                        column: x => x.VeiculosVeiculoId,
                        principalTable: "Veiculo",
                        principalColumn: "VeiculoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Venda",
                columns: table => new
                {
                    VendaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataVenda = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VeiculoId = table.Column<int>(type: "int", nullable: false),
                    VendedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venda", x => x.VendaId);
                    table.ForeignKey(
                        name: "FK_Venda_Veiculo_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculo",
                        principalColumn: "VeiculoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venda_Vendedor_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedor",
                        principalColumn: "VendedorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcessorioVeiculo_VeiculosVeiculoId",
                table: "AcessorioVeiculo",
                column: "VeiculosVeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_EnderecoProprietario_ProprietariosProprietarioId",
                table: "EnderecoProprietario",
                column: "ProprietariosProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EnderecoVendedor_VendedoresVendedorId",
                table: "EnderecoVendedor",
                column: "VendedoresVendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProprietarioTelefone_TelefonesTelefoneId",
                table: "ProprietarioTelefone",
                column: "TelefonesTelefoneId");

            migrationBuilder.CreateIndex(
                name: "IX_TelefoneVendedor_VendedoresVendedorId",
                table: "TelefoneVendedor",
                column: "VendedoresVendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_NumeroChassi",
                table: "Veiculo",
                column: "NumeroChassi",
                unique: true,
                filter: "[NumeroChassi] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_ProprietarioId",
                table: "Veiculo",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_VeiculoId",
                table: "Venda",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_VendedorId",
                table: "Venda",
                column: "VendedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcessorioVeiculo");

            migrationBuilder.DropTable(
                name: "EnderecoProprietario");

            migrationBuilder.DropTable(
                name: "EnderecoVendedor");

            migrationBuilder.DropTable(
                name: "ProprietarioTelefone");

            migrationBuilder.DropTable(
                name: "TelefoneVendedor");

            migrationBuilder.DropTable(
                name: "Venda");

            migrationBuilder.DropTable(
                name: "Acessorio");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Telefone");

            migrationBuilder.DropTable(
                name: "Veiculo");

            migrationBuilder.DropTable(
                name: "Vendedor");

            migrationBuilder.DropTable(
                name: "Proprietario");
        }
    }
}
