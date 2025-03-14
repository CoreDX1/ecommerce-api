using Ardalis.Result;
using Domain.Entity;

namespace Application.Interface;

public interface IProductsServices
{
    public Task<Result<List<Product>>> GetAllProducts();
    public Task<Result<Product>> GetProductById(int productId);
}
