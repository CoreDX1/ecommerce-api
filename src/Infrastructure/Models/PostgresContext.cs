using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity => { });

        modelBuilder.Entity<Customer>(entity => { });

        modelBuilder.Entity<Order>(entity => { });

        modelBuilder.Entity<OrderItem>(entity => { });

        modelBuilder.Entity<Payment>(entity => { });

        modelBuilder.Entity<Product>(entity => { });

        modelBuilder.Entity<Shipping>(entity => { });

        modelBuilder.Entity<User>(entity => { });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
