using Application.DTOs.Response.Product;
using Application.Interfaces;
using Ardalis.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Administration;

[Authorize(Roles = "admin")]
[Route("api/[controller]")]
[ApiController]
public class Administration : ControllerBase
{
    private readonly IProductServices _productServices;

    public Administration(IProductServices productServices)
    {
        _productServices = productServices;
    }

    [HttpGet]
    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetAllProducts()
    {
        return await _productServices.GetAllProducts();
    }
}
