using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(e => e.OrderItemId).HasName("order_items_pkey");

        builder.ToTable("order_items");

        builder.Property(e => e.OrderItemId).HasColumnName("order_item_id");
        builder.Property(e => e.OrderId).HasColumnName("order_id");
        builder.Property(e => e.ProductId).HasColumnName("product_id");
        builder.Property(e => e.Quantity).HasDefaultValue(1).HasColumnName("quantity");
        builder.Property(e => e.Subtotal).HasPrecision(12, 2).HasColumnName("subtotal");
        builder.Property(e => e.UnitPrice).HasPrecision(10, 2).HasColumnName("unit_price");

        builder
            .HasOne(d => d.Order)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(d => d.OrderId)
            .HasConstraintName("order_items_order_id_fkey");

        builder
            .HasOne(d => d.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(d => d.ProductId)
            .HasConstraintName("order_items_product_id_fkey");
    }
}
