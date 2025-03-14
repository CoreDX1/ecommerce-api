using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.OrderId).HasName("orders_pkey");

        builder.ToTable("orders");

        builder.Property(e => e.OrderId).HasColumnName("order_id");
        builder.Property(e => e.CustomerId).HasColumnName("customer_id");
        builder
            .Property(e => e.OrderDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("order_date");
        builder
            .Property(e => e.OrderStatus)
            .HasMaxLength(50)
            .HasDefaultValueSql("'Pendiente'::character varying")
            .HasColumnName("order_status");
        builder.Property(e => e.ShippingAddress).HasColumnName("shipping_address");
        builder.Property(e => e.TotalAmount).HasPrecision(12, 2).HasColumnName("total_amount");

        builder
            .HasOne(d => d.Customer)
            .WithMany(p => p.Orders)
            .HasForeignKey(d => d.CustomerId)
            .HasConstraintName("orders_customer_id_fkey");
    }
}
