using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customers");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.FullName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("full_name");

            builder.Property(c => c.Email)
                .HasMaxLength(255)
                .HasColumnName("email");

            builder.Property(c => c.Phone)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("phone");

            builder.Property(c => c.Address)
                .HasMaxLength(500)
                .HasColumnName("address");

            builder.Property(c => c.City)
                .HasMaxLength(100)
                .HasColumnName("city");

            builder.Property(c => c.Postcode)
                .HasMaxLength(20)
                .HasColumnName("postcode");

            builder.Property(c => c.Notes)
                .HasColumnType("TEXT")
                .HasColumnName("notes");

            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder.Property(c => c.UpdatedAt)
                .HasColumnName("updated_at");

            // Indexes
            builder.HasIndex(c => c.Email)
                .HasDatabaseName("ix_customers_email")
                .IsUnique();

            builder.HasIndex(c => c.Phone)
                .HasDatabaseName("ix_customers_phone")
                .IsUnique();

            builder.HasIndex(c => c.FullName)
                .HasDatabaseName("ix_customers_full_name");

            builder.HasIndex(c => c.City)
                .HasDatabaseName("ix_customers_city");
        }
    }
}