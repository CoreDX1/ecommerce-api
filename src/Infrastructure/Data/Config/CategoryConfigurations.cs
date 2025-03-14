using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(e => e.CategoryId).HasName("categories_pkey");

        builder.ToTable("categories");

        builder.HasIndex(e => e.Name, "categories_name_key").IsUnique();

        builder.Property(e => e.CategoryId).HasColumnName("category_id");
        builder.Property(e => e.Description).HasColumnName("description");
        builder.Property(e => e.Name).HasMaxLength(100).HasColumnName("name");
    }
}
