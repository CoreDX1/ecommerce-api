using Application.DTOs.Request.Product;
using Application.DTOs.Response.Product;
using Ardalis.Result;
using Domain.Entity;

namespace Application.Common.Interfaces;

public interface IProductServices : IGenericServiceAsync<Product, ProductResponseDTO>
{
    Task<Result<IEnumerable<ProductResponseDTO>>> GetProductByName(string name);
    Task<Result<IEnumerable<ProductResponseDTO>>> GetByPaginationAsync(int page, int recordsPerPage);

    Task<Result<IEnumerable<ProductResponseDTO>>> GetProductsByFilter(FilterProductRequestDTO filter);
    Task<Result<IEnumerable<ProductResponseDTO>>> GetAllProducts();

    Task<Result<ProductResponseDTO>> CreateProduct(CreateProductRequestDTO createProduct);
    Task<Result<ProductResponseDTO>> UpdateProduct(UpdateProductRequestDTO updateProduct);
    Task<Result<ProductResponseDTO>> DeleteProduct(int productId);
    Task<Result<ProductResponseDTO>> GetProductById(int productId);
    Task<Result<ProductResponseDTO>> GetProductByCategoryId(int categoryId);
}
