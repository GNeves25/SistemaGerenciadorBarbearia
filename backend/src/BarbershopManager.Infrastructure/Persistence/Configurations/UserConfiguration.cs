using BarbershopManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarbershopManager.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(user => user.PasswordHash)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(user => user.Role)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(user => user.Username).IsUnique();
    }
}
