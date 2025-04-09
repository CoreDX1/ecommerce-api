namespace Application.DTOs.Request.Order;

public class UpdateOrderStatusRequestDTO
{
    public int OrderId { get; set; }
    public string NewStatus { get; set; } = string.Empty; // E.g., "Processing", "Shipped", "Cancelled"
}
