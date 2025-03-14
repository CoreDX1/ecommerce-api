using Application.Interface;
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
    public async Task<List<Product>> GetAllProducts()
    {
        return await _productsServices.GetAllProducts();
    }
}
