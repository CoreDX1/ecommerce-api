namespace Application.DTOs.Response.Product;

public class ProductResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public decimal Price { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}
