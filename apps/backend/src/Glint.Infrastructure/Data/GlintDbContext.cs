using Glint.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Glint.Infrastructure.Data;

public class GlintDbContext : DbContext
{
    DbSet<Asset> Assets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Asset>()
            .HasKey(a => a.Id);
    }
}