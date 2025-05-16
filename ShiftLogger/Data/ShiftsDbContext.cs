using Microsoft.EntityFrameworkCore;
using ShiftLogger.Model;

namespace ShiftLogger.Data;

public class ShiftsDbContext : DbContext
{
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Person> People { get; set; }
    
    public ShiftsDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasMany(p => p.Shifts)
            .WithOne(s => s.Person)
            .HasForeignKey(s => s.PersonId)
            .HasPrincipalKey(p => p.Id);
    }
}