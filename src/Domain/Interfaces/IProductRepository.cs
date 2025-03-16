using Domain.Entity;

namespace Domain.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    public Task<IEnumerable<Product>> GetProductByName(string name);
}
