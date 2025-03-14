namespace Domain.Entity;

public partial class Shipping
{
    public int ShippingId { get; set; }

    public int? OrderId { get; set; }

    public DateTime? ShippingDate { get; set; }

    public string? TrackingNumber { get; set; }

    public string? ShippingCarrier { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public virtual Order? Order { get; set; }
}
