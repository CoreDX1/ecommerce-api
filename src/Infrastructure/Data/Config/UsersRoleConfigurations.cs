using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class UsersRoleConfigurations : IEntityTypeConfiguration<UsersRole>
{
    public void Configure(EntityTypeBuilder<UsersRole> builder)
    {
        builder.HasKey(e => new { e.UserId, e.RoleId }).HasName("users_roles_pkey");

        builder.ToTable("users_roles");

        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.RoleId).HasColumnName("role_id");

        builder.HasOne(d => d.Role).WithMany(p => p.UsersRoles).HasForeignKey(d => d.RoleId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("users_roles_role_id_fkey");
    }
}
