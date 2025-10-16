using BarbershopManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarbershopManager.Infrastructure.Persistence.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.CustomerName).IsRequired().HasMaxLength(200);
        builder.Property(a => a.ScheduledAt).IsRequired();
        builder.Property(a => a.Duration).IsRequired();
        builder.Property(a => a.Notes).HasMaxLength(500);

        builder.HasOne<Barber>()
            .WithMany()
            .HasForeignKey(a => a.BarberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<ServiceOffering>()
            .WithMany()
            .HasForeignKey(a => a.ServiceOfferingId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
