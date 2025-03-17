using System.Diagnostics;
using Application.DTOs.Response.Product;
using Application.Interfaces;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsControllers : ControllerBase
{
    private readonly IProductMapping _services;

    public ProductsControllers(IProductMapping productsServices)
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

    [HttpDelete("{productId}")] // DELETE: api/Products/5
    public Task DeleteProduct([FromRoute] int productId)
    {
        throw new NotImplementedException();
    }
}
