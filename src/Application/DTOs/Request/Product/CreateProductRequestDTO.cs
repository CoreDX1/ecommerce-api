namespace Application.DTOs.Request.Product;

public class CreateProductRequestDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public int CategoryId { get; set; }
}
