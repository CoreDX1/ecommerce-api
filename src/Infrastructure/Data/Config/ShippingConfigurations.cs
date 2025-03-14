using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class ShippingConfigurations : IEntityTypeConfiguration<Shipping>
{
    public void Configure(EntityTypeBuilder<Shipping> builder)
    {
        builder.HasKey(e => e.ShippingId).HasName("shipping_pkey");

        builder.ToTable("shipping");

        builder.Property(e => e.ShippingId).HasColumnName("shipping_id");
        builder
            .Property(e => e.DeliveryDate)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("delivery_date");
        builder.Property(e => e.OrderId).HasColumnName("order_id");
        builder
            .Property(e => e.ShippingCarrier)
            .HasMaxLength(100)
            .HasColumnName("shipping_carrier");
        builder
            .Property(e => e.ShippingDate)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("shipping_date");
        builder.Property(e => e.TrackingNumber).HasMaxLength(100).HasColumnName("tracking_number");

        builder
            .HasOne(d => d.Order)
            .WithMany(p => p.Shippings)
            .HasForeignKey(d => d.OrderId)
            .HasConstraintName("shipping_order_id_fkey");
    }
}
