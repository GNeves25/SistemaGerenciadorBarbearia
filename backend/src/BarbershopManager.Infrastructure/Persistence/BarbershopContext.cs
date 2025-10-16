using BarbershopManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarbershopManager.Infrastructure.Persistence;

public class BarbershopContext : DbContext
{
    public BarbershopContext(DbContextOptions<BarbershopContext> options)
        : base(options)
    {
    }

    public DbSet<Barber> Barbers => Set<Barber>();
    public DbSet<ServiceOffering> ServiceOfferings => Set<ServiceOffering>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BarbershopContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
