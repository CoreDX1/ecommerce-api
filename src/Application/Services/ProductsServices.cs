using Application.Interface;
using Ardalis.Result;
using Domain.Entity;
using Domain.Interfaces;

namespace Application.Services;

public class ProductsServices : IProductsServices
{
    // readonly IRepository _repository;
    readonly IUnitOfWork _unitOfWork;

    public ProductsServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<Product>>> GetAllProducts()
    {
        List<Product> products = await _unitOfWork.Product.GetAllAsync();

        // List<Product> products = await _repository.GetListAsync<Product>();

        if (products == null)
            return Result.NotFound("Products not found");

        return Result.Success(products, "Products retrieved successfully");
    }

    public async Task<Result<Product>> GetProductById(int productId)
    {
        Product product = await _unitOfWork.Product.GetByIdAsync(productId);

        if (product == null)
            return Result.NotFound("Product not found");

        return Result.Success(product, "Product retrieved successfully");
    }

    public async Task<Result<IEnumerable<Product>>> GetProductByName(string name)
    {
        IEnumerable<Product> products = await _unitOfWork.Product.GetProductByName(name);

        if (products == null)
            return Result.NotFound("Product not found");

        return Result.Success(products, "Products retrieved successfully");
    }

    public Task<Result> DeleteProduct(int productId)
    {
        throw new NotImplementedException();
    }
}
