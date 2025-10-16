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

        builder.HasData(new User(Guid.Parse("11111111-1111-1111-1111-111111111111"),
            "admin",
            "0e44ce7308af2b3de5232e4616403ce7d49ba2aec83f79c196409556422a4927",
            "Administrator"));
    }
}
