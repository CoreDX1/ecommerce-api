using System.Reflection;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class PostgresContext : DbContext
{
    public PostgresContext() { }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options) { }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Shipping> Shippings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql(
            "Host=localhost; Database=postgres; Username=core; Password=index"
        );

    protected override void OnModelCreating(ModelBuilder builder)
    {
        OnModelCreatingPartial(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
