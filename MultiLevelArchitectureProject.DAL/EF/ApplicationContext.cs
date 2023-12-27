using Microsoft.EntityFrameworkCore;
using MultiLevelArchitectureProject.DAL.Entities;

namespace MultiLevelArchitectureProject.DAL.EF;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Purchase> Purchases { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=testuser;Username=testuser;Password=12345");
    }
}
