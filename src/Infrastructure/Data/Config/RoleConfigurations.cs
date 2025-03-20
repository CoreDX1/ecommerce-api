using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class RoleConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.RoleId).HasName("roles_pkey");

        builder.ToTable("roles");

        builder.Property(e => e.RoleId).HasColumnName("role_id");
        builder.Property(e => e.RoleName).HasMaxLength(50).HasColumnName("role_name");
    }
}
