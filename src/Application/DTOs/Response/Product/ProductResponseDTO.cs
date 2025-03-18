namespace Application.DTOs.Response.Product;

public class ProductResponseDTO
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string CategoryName { get; set; } = null!;
}
