using Application.Interface;
using Ardalis.Result;
using Domain.Entity;
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
    public async Task<Result<List<Product>>> GetAllProducts()
    {
        return await _productsServices.GetAllProducts();
    }

    [HttpGet]
    [Route("{productId}")]
    public async Task<Result<Product>> GetProductById([FromRoute] int productId)
    {
        return await _productsServices.GetProductById(productId);
    }
}
