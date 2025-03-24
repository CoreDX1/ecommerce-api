using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.DTOs.Request.Product;
using Application.DTOs.Response.Product;
using Application.DTOs.Response.User;
using Application.Interfaces;
using Ardalis.Result;
using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Administration;

[Authorize(Roles = "admin")]
[Route("api/[controller]")]
[ApiController]
public class Administration : ControllerBase
{
    private readonly IProductServices _productServices;
    private readonly IUserServices _userServices;

    public Administration(IProductServices productServices, IUserServices userRepository)
    {
        _productServices = productServices;
        _userServices = userRepository;
    }

    [HttpGet] // GET: api/Administration
    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetAllProducts()
    {
        return await _productServices.GetAllProducts();
    }

    [HttpGet("users")]
    public async Task<Result<IEnumerable<UserResponseDTO>>> GetAllUsers()
    {
        return await _userServices.GetAllAsync();
    }

    [HttpGet("users/{userId}")]
    public async Task<Result<UserResponseDTO>> GetUserById(int userId)
    {
        return await _userServices.GetByIdAsync(userId);
    }

    [HttpPost("products/create")]
    public async Task<Result<ProductResponseDTO>> CreateProduct([FromBody] CreateProductRequestDTO createProduct)
    {
        return await _productServices.CreateProduct(createProduct);
    }

    [HttpPut("products/update/{productId}")]
    public async Task<Result<ProductResponseDTO>> UpdateProduct([FromBody] UpdateProductRequestDTO updateProduct)
    {
        return await _productServices.UpdateProduct(updateProduct);
    }

    [HttpDelete("products/delete/{productId}")]
    public async Task<Result<ProductResponseDTO>> DeleteProduct(int productId)
    {
        return await _productServices.DeleteProduct(productId);
    }

    [HttpPut] // PUT: api/Administration
    public Task<Result<string>> UpdateRoleForUser(int userId, string role)
    {
        throw new NotImplementedException();
    }

    [HttpGet("users/{userId}/roles")]
    public Task<Result<IEnumerable<string>>> GetRolesForUser(int userId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete] // DELETE: api/Administration
    public Task<Result<string>> DeleteRoleForUser(int userId)
    {
        throw new NotImplementedException();
    }
}
