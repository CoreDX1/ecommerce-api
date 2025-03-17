using Application.DTOs.Response.Product;
using Ardalis.Result;
using Domain.Entity;

namespace Application.Interfaces;

public interface IProductServices : IGenericServiceAsync<Product, ProductResponseDTO>
{
    public Task<Result<IEnumerable<ProductResponseDTO>>> GetProductByName(string name);
    public Task<Result<IEnumerable<ProductResponseDTO>>> GetByPaginationAsync(int page, int recordsPerPage);
}
