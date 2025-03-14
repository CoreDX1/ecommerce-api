using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(e => e.CustomerId).HasName("customers_pkey");

        builder.ToTable("customers");

        builder.HasIndex(e => e.Email, "customers_email_key").IsUnique();

        builder.Property(e => e.CustomerId).HasColumnName("customer_id");
        builder.Property(e => e.Address).HasColumnName("address");
        builder.Property(e => e.Email).HasMaxLength(100).HasColumnName("email");
        builder.Property(e => e.FirstName).HasMaxLength(50).HasColumnName("first_name");
        builder.Property(e => e.LastName).HasMaxLength(50).HasColumnName("last_name");
        builder.Property(e => e.PhoneNumber).HasMaxLength(20).HasColumnName("phone_number");
        builder
            .Property(e => e.RegistrationDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("registration_date");
    }
}
