using Application.DTOs.Response.Product;
using Application.Interfaces;
using Ardalis.Result;
using AutoMapper;
using Domain.Entity;
using Domain.Interfaces.Persistence;

namespace Application.Services;

public class ProductMapping : ReadServiceAsync<Product, ProductResponseDTO>, IProductMapping
{
    public ProductMapping(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper) { }

    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetProductByName(string name)
    {
        IEnumerable<Product> products = await _unitOfWork.Product.GetProductByName(name);

        if (products == null)
            return Result.NotFound("Product not found");

        var productResponse = _mapper.Map<IEnumerable<ProductResponseDTO>>(products);

        return Result.Success(productResponse, "Product retrieved successfully");
    }
}
