namespace Application.DTOs.Request.Product;

public class FilterProductRequestDTO
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public int CategoryId { get; set; }

    public int Page { get; set; } = 1;
    public int RecordsPerPage { get; set; } = 10;

    public bool IsDescending { get; set; } = true;
    public string OrderBy { get; set; } = "Name";
}
