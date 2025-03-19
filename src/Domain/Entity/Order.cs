namespace Domain.Entity;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? ShippingAddress { get; set; }

    public string? OrderStatus { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = [];

    public virtual ICollection<Payment> Payments { get; set; } = [];

    public virtual ICollection<Shipping> Shippings { get; set; } = [];
}
