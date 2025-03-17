using Application.DTOs.Response.Product;
using Application.Interfaces;
using Ardalis.Result;
using AutoMapper;
using Domain.Entity;
using Domain.Interfaces;

namespace Application.Services;

public class ProductMapping : ReadServiceAsync<Product, ProductResponseDTO>, IProductMapping
{
    public ProductMapping(IGenericRepository<Product> repository, IMapper mapper)
        : base(repository, mapper)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<ProductResponseDTO>>> GetProductByName(string name)
    {
        throw new NotImplementedException();
    }
}
