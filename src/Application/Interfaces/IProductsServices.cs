using Application.DTOs.Response.Product;
using Ardalis.Result;

namespace Application.Interfaces;

public interface IProductsServices
{
    public Task<Result<List<ProductResponseDTO>>> GetAllProducts();
    public Task<Result<ProductResponseDTO>> GetProductById(int productId);
    public Task<Result<IEnumerable<ProductResponseDTO>>> GetProductByName(string name);
    public Task<Result> DeleteProduct(int productId);
}
