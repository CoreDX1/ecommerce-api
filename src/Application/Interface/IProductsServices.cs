using Domain.Entity;

namespace Application.Interface;

public interface IProductsServices
{
    public Task<List<Product>> GetAllProducts();
}
