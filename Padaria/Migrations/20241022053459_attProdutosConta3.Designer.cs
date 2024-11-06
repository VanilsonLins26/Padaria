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
    [Migration("20241022053459_attProdutosConta3")]
    partial class attProdutosConta3
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
                        .HasColumnType("longtext");

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

                    b.Property<double>("ValorTotal")
                        .HasColumnType("double");

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
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Preco")
                        .HasColumnType("double");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("Padaria.Models.ProdutosConta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ContaId")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<double>("Total")
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

                    b.Property<int>("ContaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataEntrega")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasIndex("ClienteId");

                    b.HasIndex("ContaId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("Encomenda");
                });

            modelBuilder.Entity("Padaria.Models.ProdutosConta", b =>
                {
                    b.HasOne("Padaria.Models.Conta", "Conta")
                        .WithMany("Produtos")
                        .HasForeignKey("ContaId")
                        .OnDelete(DeleteBehavior.Restrict);

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

                    b.HasOne("Padaria.Models.Conta", "Conta")
                        .WithOne("Encomenda")
                        .HasForeignKey("Padaria.Models.Encomenda", "ContaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Conta");
                });

            modelBuilder.Entity("Padaria.Models.Cliente", b =>
                {
                    b.Navigation("Encomendas");
                });

            modelBuilder.Entity("Padaria.Models.Conta", b =>
                {
                    b.Navigation("Encomenda");

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
