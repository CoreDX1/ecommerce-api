using Application.Interface;
using Ardalis.Result;
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

    public async Task<Result<List<Product>>> GetAllProducts()
    {
        List<Product> products = await _repository.GetListAsync<Product>();

        if (products == null)
            return Result.NotFound("Products not found");

        return Result.Success(products, "Products retrieved successfully");
    }

    public async Task<Result<Product>> GetProductById(int productId)
    {
        Product product = await _repository.GetByIdAsync<Product>(productId);

        if (product == null)
            return Result.NotFound("Product not found");

        return Result.Success(product, "Product retrieved successfully");
    }

    public async Task<Result<List<Product>>> GetProductByName(string name)
    {
        var products = await _repository.GetListAsync<Product>(x =>
            x.Name.ToLower().Contains(name.ToLower())
        );

        if (products == null)
            return Result.NotFound("Product not found");

        return Result.Success(products, "Products retrieved successfully");
    }

    public Task<Result> DeleteProduct(int productId)
    {
        throw new NotImplementedException();
    }
}
