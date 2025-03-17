using Application.DTOs.Response.Product;
using Ardalis.Result;
using Domain.Entity;

namespace Application.Interfaces;

public interface IProductMapping : IReadServiceAsync<Product, ProductResponseDTO>
{
    Task<Result<IEnumerable<ProductResponseDTO>>> GetProductByName(string name);
}
