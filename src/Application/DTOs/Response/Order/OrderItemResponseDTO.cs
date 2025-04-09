namespace Application.DTOs.Response.Order;

public class OrderItemResponseDTO
{
    public int OrderItemId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty; // Útil para mostrar en UI
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; } // Precio al momento de la compra
    public decimal Subtotal { get; set; }
}
