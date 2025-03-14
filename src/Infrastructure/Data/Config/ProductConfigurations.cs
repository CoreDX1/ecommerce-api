using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.ProductId).HasName("products_pkey");

        builder.ToTable("products");

        builder.Property(e => e.ProductId).HasColumnName("product_id");
        builder.Property(e => e.CategoryId).HasColumnName("category_id");
        builder
            .Property(e => e.CreationDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("creation_date");
        builder.Property(e => e.Description).HasColumnName("description");
        builder
            .Property(e => e.LastUpdated)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("last_updated");
        builder.Property(e => e.Name).HasMaxLength(255).HasColumnName("name");
        builder.Property(e => e.Price).HasPrecision(10, 2).HasColumnName("price");
        builder.Property(e => e.StockQuantity).HasDefaultValue(0).HasColumnName("stock_quantity");

        builder
            .HasOne(d => d.Category)
            .WithMany(p => p.Products)
            .HasForeignKey(d => d.CategoryId)
            .HasConstraintName("products_category_id_fkey");
    }
}
