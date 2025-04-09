namespace Application.DTOs.Request.Order;

public class CreateOrderRequestDTO
{
    public int CustomerId { get; set; }
    public string? ShippingAddress { get; set; } // Puede ser opcional si se toma del cliente
    public List<CreateOrderItemRequestDTO> Items { get; set; } = [];
}
