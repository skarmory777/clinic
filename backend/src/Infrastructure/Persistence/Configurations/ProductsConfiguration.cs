using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.Sku)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("sku");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("name");

            builder.Property(e => e.Description)
                    .HasColumnType("TEXT")
                    .HasColumnName("description");

            builder.Property(e => e.Category)
                .HasMaxLength(100)
                .HasColumnName("category");

            builder.Property(e => e.UnitPrice)
                .IsRequired()
                .HasColumnType("DECIMAL(10,2)")
                .HasColumnName("unit_price");

            builder.Property(e => e.CostPrice)
                .HasColumnType("DECIMAL(10,2)")
                .HasColumnName("cost_price");

            builder.Property(e => e.TaxRate)
                .IsRequired()
                .HasColumnType("DECIMAL(5,2)")
                .HasDefaultValue(0.00m)
                .HasColumnName("tax_rate");

            builder.Property(e => e.UnitOfMeasure)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("unit")
                .HasColumnName("unit_of_measure");

            builder.Property(e => e.StockQuantity)
                .IsRequired()
                .HasDefaultValue(0)
                .HasColumnName("stock_quantity");

            builder.Property(e => e.MinStockLevel)
                .IsRequired()
                .HasDefaultValue(0)
                .HasColumnName("min_stock_level");

            builder.Property(e => e.ReorderQuantity)
                .HasColumnName("reorder_quantity");

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValue(true)
                .HasColumnName("is_active");

            builder.Property(e => e.Notes)
                .HasColumnType("TEXT")
                .HasColumnName("notes");

            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            // Indexes
            builder.HasIndex(e => e.Sku)
                .HasDatabaseName("ix_products_sku")
                .IsUnique();

            builder.HasIndex(e => e.Name)
                .HasDatabaseName("ix_products_name");

            builder.HasIndex(e => e.Category)
                .HasDatabaseName("ix_products_category");

            builder.HasIndex(e => e.IsActive)
                .HasDatabaseName("ix_products_is_active");

            // Check constraints (em algumas versões do MySQL/EF Core)
            builder.HasCheckConstraint("CK_Product_UnitPrice", "unit_price >= 0");
            builder.HasCheckConstraint("CK_Product_StockQuantity", "stock_quantity >= 0");
            builder.HasCheckConstraint("CK_Product_MinStockLevel", "min_stock_level >= 0");
            builder.HasCheckConstraint("CK_Product_TaxRate", "tax_rate >= 0 AND tax_rate <= 100");
        }
    }
}