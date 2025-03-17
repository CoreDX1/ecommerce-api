using Application.DTOs.Response.Product;
using Application.Interfaces;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsControllers : ControllerBase
{
    private readonly IProductsServices _productsServices;

    public ProductsControllers(IProductsServices productsServices)
    {
        _productsServices = productsServices;
    }

    [HttpGet("all")] // GET: api/Products
    public async Task<Result<List<ProductResponseDTO>>> GetAllProducts()
    {
        return await _productsServices.GetAllProducts();
    }

    [HttpGet("{productId}")] // GET: api/Products/5
    public async Task<Result<ProductResponseDTO>> GetProductById([FromRoute] int productId)
    {
        return await _productsServices.GetProductById(productId);
    }

    [HttpGet("product/name/{name}")] // GET: api/Products/product/name/product-name
    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetProductByName(
        [FromRoute] string name
    )
    {
        return await _productsServices.GetProductByName(name);
    }

    [HttpDelete("{productId}")] // DELETE: api/Products/5
    public async Task<Result> DeleteProduct([FromRoute] int productId)
    {
        return await _productsServices.DeleteProduct(productId);
    }
}
