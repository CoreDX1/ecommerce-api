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

    [HttpGet]
    [Route("all")]
    public async Task<Result<List<ProductResponseDTO>>> GetAllProducts()
    {
        return await _productsServices.GetAllProducts();
    }

    [HttpGet]
    [Route("{productId}")]
    public async Task<Result<ProductResponseDTO>> GetProductById([FromRoute] int productId)
    {
        return await _productsServices.GetProductById(productId);
    }

    [HttpGet]
    [Route("product/{name}")]
    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetProductByName(
        [FromRoute] string name
    )
    {
        return await _productsServices.GetProductByName(name);
    }

    [HttpDelete("{productId}")]
    public async Task<Result> DeleteProduct([FromRoute] int productId)
    {
        return await _productsServices.DeleteProduct(productId);
    }
}
