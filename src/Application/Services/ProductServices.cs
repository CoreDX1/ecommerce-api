using Application.Common.Interfaces.Persistence;
using Application.DTOs.Request.Product;
using Application.DTOs.Response.Product;
using Application.Interfaces;
using Ardalis.Result;
using AutoMapper;
using Domain.Entity;

namespace Application.Services;

public class ProductServices : GenericServiceAsync<Product, ProductResponseDTO>, IProductServices
{
    public ProductServices(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper) { }

    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetProductByName(string name)
    {
        IEnumerable<Product> products = await _unitOfWork.ProductRepository.GetProductByName(name);

        if (products == null)
            return Result.NotFound("Product not found");

        var productResponse = _mapper.Map<IEnumerable<ProductResponseDTO>>(products);

        return Result.Success(productResponse, "Product retrieved successfully");
    }

    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetByPaginationAsync(
        int page,
        int recordsPerPage
    )
    {
        IEnumerable<Product> products = await _unitOfWork.ProductRepository.GetByPaginationAsync(
            page,
            recordsPerPage
        );

        if (products == null)
            return Result.NotFound("Product not found");

        var productResponse = _mapper.Map<IEnumerable<ProductResponseDTO>>(products);

        return Result.Success(productResponse, "Product retrieved successfully");
    }

    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetProductsByFilter(
        FilterProductRequestDTO filter
    )
    {
        IEnumerable<Product> products = await _unitOfWork.ProductRepository.GetProductsByFilter(
            filter
        );

        if (products == null)
            return Result.NotFound("Product not found");

        var productResponse = _mapper.Map<IEnumerable<ProductResponseDTO>>(products);

        return Result.Success(productResponse, "Product retrieved successfully");
    }

    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetAllProducts()
    {
        IEnumerable<Product> products = await _unitOfWork.ProductRepository.GetAllProducts();

        if (products == null)
            return Result.NotFound("Product not found");

        var productResponse = _mapper.Map<IEnumerable<ProductResponseDTO>>(products);

        return Result.Success(productResponse, "Product retrieved successfully");
    }
}
