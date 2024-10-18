using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Padaria.Models;

    public class PadariaContext : DbContext
    {
        public PadariaContext (DbContextOptions<PadariaContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produto { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
        .SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

public DbSet<Padaria.Models.Cliente> Cliente { get; set; } = default!;
}


