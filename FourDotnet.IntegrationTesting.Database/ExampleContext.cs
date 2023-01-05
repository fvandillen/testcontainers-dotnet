using Microsoft.EntityFrameworkCore;

namespace FourDotnet.IntegrationTesting.Database;

public class ExampleContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    public ExampleContext(DbContextOptions<ExampleContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .Property(x => x.FirstName)
            .IsRequired();
        
        modelBuilder.Entity<Employee>()
            .Property(x => x.Lastname)
            .IsRequired();
    }
}