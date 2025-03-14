using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.UserId).HasName("users_pkey");

        builder.ToTable("users");

        builder.HasIndex(e => e.CustomerId, "users_customer_id_key").IsUnique();

        builder.HasIndex(e => e.Email, "users_email_key").IsUnique();

        builder.HasIndex(e => e.Username, "users_username_key").IsUnique();

        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.CustomerId).HasColumnName("customer_id");
        builder.Property(e => e.Email).HasMaxLength(100).HasColumnName("email");
        builder
            .Property(e => e.EmailVerified)
            .HasDefaultValue(false)
            .HasColumnName("email_verified");
        builder.Property(e => e.IsActive).HasDefaultValue(true).HasColumnName("is_active");
        builder
            .Property(e => e.LastLogin)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("last_login");
        builder.Property(e => e.PasswordHash).HasMaxLength(255).HasColumnName("password_hash");
        builder
            .Property(e => e.PasswordResetExpiry)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("password_reset_expiry");
        builder
            .Property(e => e.PasswordResetToken)
            .HasMaxLength(255)
            .HasColumnName("password_reset_token");
        builder
            .Property(e => e.RegistrationDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("registration_date");
        builder.Property(e => e.Username).HasMaxLength(50).HasColumnName("username");
        builder
            .Property(e => e.VerificationToken)
            .HasMaxLength(255)
            .HasColumnName("verification_token");

        builder
            .HasOne(d => d.Customer)
            .WithOne(p => p.User)
            .HasForeignKey<User>(d => d.CustomerId)
            .HasConstraintName("users_customer_id_fkey");
    }
}
