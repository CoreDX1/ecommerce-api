namespace Domain.Entity;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public int? CategoryId { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual Category? Category { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = [];
}
