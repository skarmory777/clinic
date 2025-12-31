using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("id");
            builder.Property(e => e.Username).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(255);
            builder.Property(e => e.PasswordHash).IsRequired();
            builder.Property(e => e.FullName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.IsActive).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired()
                .HasColumnName("created_at");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            builder.HasIndex(e => e.Email).IsUnique();
            builder.HasIndex(e => e.Username).IsUnique();
        }
    }
}