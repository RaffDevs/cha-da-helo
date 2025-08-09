using System;
using ChaHelo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChaHelo.Infra.Context;

public class ChaHeloDbContext : DbContext
{
    public ChaHeloDbContext(DbContextOptions<ChaHeloDbContext> options) : base(options) { }

    public DbSet<Presence> Presences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Presence>(e =>
        {
            e.HasKey(p => p.Id);
        });
    }
}   
