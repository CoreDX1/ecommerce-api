using Application.Common.Interfaces;
using Application.DTOs.Request.Product;
using Application.DTOs.Response.Product;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Product;

[ApiController]
[Route("api/[controller]")]
public class Product : ControllerBase
{
    private readonly IProductServices _services;

    public Product(IProductServices productsServices)
    {
        _services = productsServices;
    }

    /// <summary>
    /// Get all products
    /// </summary>
    /// <returns> A list of products </returns>
    [HttpGet("all")] // GET: api/Products
    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetAllProducts()
    {
        return await _services.GetAllProducts();
    }

    /// <summary>
    /// Get a product by id
    /// </summary>
    /// <param name="productId"> The id of the product </param>
    [HttpGet("{productId}")] // GET: api/Products/5
    public async Task<Result<ProductResponseDTO>> GetProductById([FromRoute] int productId)
    {
        return await _services.GetByIdAsync(productId);
    }

    /// <summary>
    /// Get a product by name
    /// </summary>
    /// <param name="name">the name of the product</param>
    /// <returns></returns>
    [HttpGet("name/{name}")] // GET: api/Products/product/name/product-name
    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetProductByName([FromRoute] string name)
    {
        return await _services.GetProductByName(name);
    }

    /// <summary>
    /// Get a product by pagination
    /// </summary>
    /// <param name="page">the page number</param>
    /// <param name="recordsPerPage">the number of records per page</param>
    /// <returns> A list of products </returns>
    [HttpGet("pagination/{page}/{recordsPerPage}")] // GET: api/Products/product/pagination/1/10
    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetProductByPagination([FromRoute] int page, [FromRoute] int recordsPerPage)
    {
        return await _services.GetByPaginationAsync(page, recordsPerPage);
    }

    /// <summary>
    /// Delete a product by id
    /// </summary>
    /// <param name="productId">The id of the product</param>
    /// <returns></returns>
    [HttpDelete("{productId}")] // DELETE: api/Products/5
    public async Task DeleteProduct([FromRoute] int productId)
    {
        await _services.DeleteAsync(productId);
    }

    /// <summary>
    /// Filter products
    /// </summary>
    /// <param name="filter">The filter to apply</param>
    /// <returns> A list of products </returns>
    [HttpPost("filter")] // POST: api/Products/product/filter
    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetProductsByFilter([FromBody] FilterProductRequestDTO filter)
    {
        return await _services.GetProductsByFilter(filter);
    }
}
