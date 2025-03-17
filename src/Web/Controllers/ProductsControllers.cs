using Application.DTOs.Request.Product;
using Application.DTOs.Response.Product;
using Application.Interfaces;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsControllers : ControllerBase
{
    private readonly IProductServices _services;

    public ProductsControllers(IProductServices productsServices)
    {
        _services = productsServices;
    }

    [HttpGet("all")] // GET: api/Products
    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetAllProducts()
    {
        return await _services.GetAllAsync();
    }

    [HttpGet("{productId}")] // GET: api/Products/5
    public async Task<Result<ProductResponseDTO>> GetProductById([FromRoute] int productId)
    {
        return await _services.GetByIdAsync(productId);
    }

    [HttpGet("product/name/{name}")] // GET: api/Products/product/name/product-name
    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetProductByName([FromRoute] string name)
    {
        return await _services.GetProductByName(name);
    }

    [HttpGet("product/pagination/{page}/{recordsPerPage}")] // GET: api/Products/product/pagination/1/10
    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetProductByPagination([FromRoute] int page, [FromRoute] int recordsPerPage)
    {
        return await _services.GetByPaginationAsync(page, recordsPerPage);
    }

    [HttpDelete("{productId}")] // DELETE: api/Products/5
    public async Task DeleteProduct([FromRoute] int productId)
    {
        await _services.DeleteAsync(productId);
    }

    [HttpPost("product/filter")] // POST: api/Products/product/filter
    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetProductsByFilter([FromBody] FilterProductRequestDTO filter)
    {
        return await _services.GetProductsByFilter(filter);
    }
}
