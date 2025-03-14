using Application.Interface;
using Domain.Entity;
using TanvirArjel.EFCore.GenericRepository;

namespace Application.Services;

public class ProductsServices : IProductsServices
{
    readonly IRepository _repository;

    public ProductsServices(IRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Product>> GetAllProducts()
    {
        return _repository.GetListAsync<Product>();
    }
}
