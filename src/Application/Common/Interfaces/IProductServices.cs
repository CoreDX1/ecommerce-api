using Application.Common.Interfaces;
using Application.DTOs.Request.Product;
using Application.DTOs.Response.Product;
using Ardalis.Result;
using Domain.Entity;

namespace Application.Interfaces;

public interface IProductServices : IGenericServiceAsync<Product, ProductResponseDTO>
{
    Task<Result<IEnumerable<ProductResponseDTO>>> GetProductByName(string name);
    Task<Result<IEnumerable<ProductResponseDTO>>> GetByPaginationAsync(
        int page,
        int recordsPerPage
    );

    Task<Result<IEnumerable<ProductResponseDTO>>> GetProductsByFilter(
        FilterProductRequestDTO filter
    );
    Task<Result<IEnumerable<ProductResponseDTO>>> GetAllProducts();
}
