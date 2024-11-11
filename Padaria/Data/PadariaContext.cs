using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Padaria.Models;

public class PadariaContext : DbContext
{
    public PadariaContext(DbContextOptions<PadariaContext> options)
        : base(options)
    {
    }

    public DbSet<Produto> Produto { get; set; } = default!;
    public DbSet<Conta> Conta { get; set; } = default!;
    public DbSet<ProdutoConta> ProdutosConta { get; set; } = default!;
    public DbSet<Encomenda> Encomenda { get; set; } = default!;
    public DbSet<Cliente> Cliente { get; set; } = default!;




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>().HasIndex(e => e.Codigo).IsUnique();
        base.OnModelCreating(modelBuilder);
        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
        .SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder.Entity<ProdutoConta>()
        .HasOne(p => p.Conta)
        .WithMany(c => c.Produtos)
        .HasForeignKey(p => p.ContaId)
        .OnDelete(DeleteBehavior.Cascade);



    }



}


