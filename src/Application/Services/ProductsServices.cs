using Application.DTOs.Response.Product;
using Application.Interfaces;
using Ardalis.Result;
using AutoMapper;
using Domain.Entity;
using Domain.Interfaces.Persistence;

namespace Application.Services;

public class ProductsServices : IProductsServices
{
    // readonly IRepository _repository;
    readonly IUnitOfWork _unitOfWork;
    readonly IMapper _mapper;

    public ProductsServices(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<ProductResponseDTO>>> GetAllProducts()
    {
        List<Product> products = await _unitOfWork.Product.GetAllAsync();

        // List<Product> products = await _repository.GetListAsync<Product>();

        var productsResponse = _mapper.Map<List<ProductResponseDTO>>(products);

        if (products == null)
            return Result.NotFound("Products not found");

        return Result.Success(productsResponse, "Products retrieved successfully");
    }

    public async Task<Result<ProductResponseDTO>> GetProductById(int productId)
    {
        Product product = await _unitOfWork.Product.GetByIdAsync(productId);

        if (product == null)
            return Result.NotFound("Product not found");

        var productResponse = _mapper.Map<ProductResponseDTO>(product);

        return Result.Success(productResponse, "Product retrieved successfully");
    }

    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetProductByName(string name)
    {
        IEnumerable<Product> products = await _unitOfWork.Product.GetProductByName(name);

        if (products == null)
            return Result.NotFound("Product not found");

        var productResponse = _mapper.Map<IEnumerable<ProductResponseDTO>>(products);

        return Result.Success(productResponse, "Product retrieved successfully");
    }

    public Task<Result> DeleteProduct(int productId)
    {
        throw new NotImplementedException();
    }
}
