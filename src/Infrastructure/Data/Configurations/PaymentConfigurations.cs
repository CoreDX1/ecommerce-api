using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class PaymentConfigurations : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(e => e.PaymentId).HasName("payments_pkey");

        builder.ToTable("payments");

        builder.Property(e => e.PaymentId).HasColumnName("payment_id");
        builder.Property(e => e.OrderId).HasColumnName("order_id");
        builder
            .Property(e => e.PaymentDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("payment_date");
        builder.Property(e => e.PaymentMethod).HasMaxLength(50).HasColumnName("payment_method");
        builder
            .Property(e => e.PaymentStatus)
            .HasMaxLength(50)
            .HasDefaultValueSql("'Pendiente'::character varying")
            .HasColumnName("payment_status");
        builder.Property(e => e.TransactionId).HasMaxLength(100).HasColumnName("transaction_id");

        builder
            .HasOne(d => d.Order)
            .WithMany(p => p.Payments)
            .HasForeignKey(d => d.OrderId)
            .HasConstraintName("payments_order_id_fkey");
    }
}
