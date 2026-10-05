using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // di
    {
    }
    // коллекция сущностей
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) // переопределение DbContext
    {
        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();
    }
}