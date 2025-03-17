namespace Application.DTOs.Request.Product;

public class FilterProductRequestDTO
{
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public string? Category { get; set; }

    public int Page { get; set; } = 1;
    public int RecordsPerPage { get; set; } = 10;

    public bool? IsDescending { get; set; } = true;
}
