﻿// <auto-generated />
using System;
using ConcessionariaAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConcessionariaAPI.Migrations
{
    [DbContext(typeof(ConcessionariaContext))]
    [Migration("20240131112205_UpdateDatabase5")]
    partial class UpdateDatabase5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AcessorioVeiculo", b =>
                {
                    b.Property<int>("AcessoriosAcessorioID")
                        .HasColumnType("int");

                    b.Property<int>("VeiculosVeiculoId")
                        .HasColumnType("int");

                    b.HasKey("AcessoriosAcessorioID", "VeiculosVeiculoId");

                    b.HasIndex("VeiculosVeiculoId");

                    b.ToTable("AcessorioVeiculo");
                });

            modelBuilder.Entity("ConcessionariaAPI.Models.Acessorio", b =>
                {
                    b.Property<int?>("AcessorioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("AcessorioID"));

                    b.Property<string>("Descricao")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AcessorioID");

                    b.ToTable("Acessorio");
                });

            modelBuilder.Entity("ConcessionariaAPI.Models.Endereco", b =>
                {
                    b.Property<int?>("EnderecoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("EnderecoId"));

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("EnderecoId");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("ConcessionariaAPI.Models.Proprietario", b =>
                {
                    b.Property<int?>("ProprietarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ProprietarioId"));

                    b.Property<string>("CNPJ")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("CPF")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("ProprietarioId");

                    b.ToTable("Proprietario");
                });

            modelBuilder.Entity("ConcessionariaAPI.Models.Telefone", b =>
                {
                    b.Property<int?>("TelefoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("TelefoneId"));

                    b.Property<string>("NumeroTelefone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("TelefoneId");

                    b.ToTable("Telefone");
                });

            modelBuilder.Entity("ConcessionariaAPI.Models.Veiculo", b =>
                {
                    b.Property<int?>("VeiculoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("VeiculoId"));

                    b.Property<string>("NumeroChassi")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)");

                    b.Property<int?>("ProprietarioId")
                        .HasColumnType("int");

                    b.Property<int>("Quilometragem")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("VersaoSistema")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("VeiculoId");

                    b.HasIndex("NumeroChassi")
                        .IsUnique();

                    b.HasIndex("ProprietarioId");

                    b.ToTable("Veiculo");
                });

            modelBuilder.Entity("ConcessionariaAPI.Models.Venda", b =>
                {
                    b.Property<int?>("VendaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("VendaId"));

                    b.Property<DateTime>("DataVenda")
                        .HasColumnType("datetime2");

                    b.Property<int>("VeiculoId")
                        .HasColumnType("int");

                    b.Property<int>("VendedorId")
                        .HasColumnType("int");

                    b.HasKey("VendaId");

                    b.HasIndex("VeiculoId");

                    b.HasIndex("VendedorId");

                    b.ToTable("Venda");
                });

            modelBuilder.Entity("ConcessionariaAPI.Models.Vendedor", b =>
                {
                    b.Property<int?>("VendedorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("VendedorId"));

                    b.Property<DateTime>("DataAdmissao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<decimal>("SalarioBase")
                        .HasPrecision(6, 2)
                        .HasColumnType("decimal(6,2)");

                    b.HasKey("VendedorId");

                    b.ToTable("Vendedor");
                });

            modelBuilder.Entity("EnderecoProprietario", b =>
                {
                    b.Property<int>("EnderecosEnderecoId")
                        .HasColumnType("int");

                    b.Property<int>("ProprietariosProprietarioId")
                        .HasColumnType("int");

                    b.HasKey("EnderecosEnderecoId", "ProprietariosProprietarioId");

                    b.HasIndex("ProprietariosProprietarioId");

                    b.ToTable("EnderecoProprietario");
                });

            modelBuilder.Entity("EnderecoVendedor", b =>
                {
                    b.Property<int>("EnderecosEnderecoId")
                        .HasColumnType("int");

                    b.Property<int>("VendedoresVendedorId")
                        .HasColumnType("int");

                    b.HasKey("EnderecosEnderecoId", "VendedoresVendedorId");

                    b.HasIndex("VendedoresVendedorId");

                    b.ToTable("EnderecoVendedor");
                });

            modelBuilder.Entity("ProprietarioTelefone", b =>
                {
                    b.Property<int>("ProprietariosProprietarioId")
                        .HasColumnType("int");

                    b.Property<int>("TelefonesTelefoneId")
                        .HasColumnType("int");

                    b.HasKey("ProprietariosProprietarioId", "TelefonesTelefoneId");

                    b.HasIndex("TelefonesTelefoneId");

                    b.ToTable("ProprietarioTelefone");
                });

            modelBuilder.Entity("TelefoneVendedor", b =>
                {
                    b.Property<int>("TelefonesTelefoneId")
                        .HasColumnType("int");

                    b.Property<int>("VendedoresVendedorId")
                        .HasColumnType("int");

                    b.HasKey("TelefonesTelefoneId", "VendedoresVendedorId");

                    b.HasIndex("VendedoresVendedorId");

                    b.ToTable("TelefoneVendedor");
                });

            modelBuilder.Entity("AcessorioVeiculo", b =>
                {
                    b.HasOne("ConcessionariaAPI.Models.Acessorio", null)
                        .WithMany()
                        .HasForeignKey("AcessoriosAcessorioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConcessionariaAPI.Models.Veiculo", null)
                        .WithMany()
                        .HasForeignKey("VeiculosVeiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConcessionariaAPI.Models.Veiculo", b =>
                {
                    b.HasOne("ConcessionariaAPI.Models.Proprietario", "Proprietario")
                        .WithMany()
                        .HasForeignKey("ProprietarioId");

                    b.Navigation("Proprietario");
                });

            modelBuilder.Entity("ConcessionariaAPI.Models.Venda", b =>
                {
                    b.HasOne("ConcessionariaAPI.Models.Veiculo", "Veiculo")
                        .WithMany()
                        .HasForeignKey("VeiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConcessionariaAPI.Models.Vendedor", "Vendedor")
                        .WithMany()
                        .HasForeignKey("VendedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Veiculo");

                    b.Navigation("Vendedor");
                });

            modelBuilder.Entity("EnderecoProprietario", b =>
                {
                    b.HasOne("ConcessionariaAPI.Models.Endereco", null)
                        .WithMany()
                        .HasForeignKey("EnderecosEnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConcessionariaAPI.Models.Proprietario", null)
                        .WithMany()
                        .HasForeignKey("ProprietariosProprietarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EnderecoVendedor", b =>
                {
                    b.HasOne("ConcessionariaAPI.Models.Endereco", null)
                        .WithMany()
                        .HasForeignKey("EnderecosEnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConcessionariaAPI.Models.Vendedor", null)
                        .WithMany()
                        .HasForeignKey("VendedoresVendedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProprietarioTelefone", b =>
                {
                    b.HasOne("ConcessionariaAPI.Models.Proprietario", null)
                        .WithMany()
                        .HasForeignKey("ProprietariosProprietarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConcessionariaAPI.Models.Telefone", null)
                        .WithMany()
                        .HasForeignKey("TelefonesTelefoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TelefoneVendedor", b =>
                {
                    b.HasOne("ConcessionariaAPI.Models.Telefone", null)
                        .WithMany()
                        .HasForeignKey("TelefonesTelefoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConcessionariaAPI.Models.Vendedor", null)
                        .WithMany()
                        .HasForeignKey("VendedoresVendedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
