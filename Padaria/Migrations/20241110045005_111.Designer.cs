﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Padaria.Migrations
{
    [DbContext(typeof(PadariaContext))]
    [Migration("20241110045005_111")]
    partial class _111
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Padaria.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contato")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("Padaria.Models.Conta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)");

                    b.Property<int?>("MetodoPagamento")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Conta");

                    b.HasDiscriminator().HasValue("Conta");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Padaria.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("QntDisponiveis")
                        .HasColumnType("int");

                    b.Property<int>("QntVendidas")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<double>("ValorUnitario")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("Padaria.Models.ProdutoConta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ContaId")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<double>("ValorUnitario")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("ContaId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ProdutosConta");
                });

            modelBuilder.Entity("Padaria.Models.Encomenda", b =>
                {
                    b.HasBaseType("Padaria.Models.Conta");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataPedido")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Obs")
                        .HasColumnType("longtext");

                    b.Property<double?>("ValorAntecipado")
                        .HasColumnType("double");

                    b.HasIndex("ClienteId");

                    b.HasDiscriminator().HasValue("Encomenda");
                });

            modelBuilder.Entity("Padaria.Models.ProdutoConta", b =>
                {
                    b.HasOne("Padaria.Models.Conta", "Conta")
                        .WithMany("Produtos")
                        .HasForeignKey("ContaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Padaria.Models.Produto", "Produto")
                        .WithMany("Produtos")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Conta");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Padaria.Models.Encomenda", b =>
                {
                    b.HasOne("Padaria.Models.Cliente", "Cliente")
                        .WithMany("Encomendas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("Padaria.Models.Cliente", b =>
                {
                    b.Navigation("Encomendas");
                });

            modelBuilder.Entity("Padaria.Models.Conta", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("Padaria.Models.Produto", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
