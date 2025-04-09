namespace Application.DTOs.Response.Order;

public class OrderResponseDTO
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty; // Nombre completo del cliente
    public DateTime OrderDate { get; set; }
    public string? ShippingAddress { get; set; }
    public string OrderStatus { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public List<OrderItemResponseDTO> Items { get; set; } = [];
    // Podrías añadir info resumida de Pagos o Envíos si es necesario
}
