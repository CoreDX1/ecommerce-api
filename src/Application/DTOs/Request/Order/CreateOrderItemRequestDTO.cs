namespace Application.DTOs.Request.Order;

public class CreateOrderItemRequestDTO
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    // UnitPrice podría obtenerse del producto en el servicio, no necesariamente venir en el request
}
