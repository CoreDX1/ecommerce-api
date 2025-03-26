using Application.Common.Interfaces;
using Application.DTOs.Request.Product;
using Application.DTOs.Response.Product;
using Application.DTOs.Response.User;
using Ardalis.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Admin;

[Authorize(Roles = "admin")]
[ApiController]
[Route("api/[controller]")]
public class Admin : ControllerBase
{
    private readonly IProductServices _productService;
    private readonly IUserService _userService;
    private readonly IUserRolesService _userRolesService;

    public Admin(IProductServices productService, IUserService userSerice, IUserRolesService userRolesService)
    {
        _productService = productService;
        _userService = userSerice;
        _userRolesService = userRolesService;
    }

    [HttpGet] // GET: api/Administration
    [ProducesResponseType(typeof(IEnumerable<ProductResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetAllProducts()
    {
        return await _productService.GetAllProducts();
    }

    [HttpGet("users")]
    [ProducesResponseType(typeof(IEnumerable<UserResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<Result<IEnumerable<UserResponseDTO>>> GetAllUsers()
    {
        return await _userService.GetAllAsync();
    }

    [HttpGet("users/{userId}")]
    public async Task<Result<UserResponseDTO>> GetUserById(int userId)
    {
        return await _userService.GetByIdAsync(userId);
    }

    [HttpPost("products/create")]
    public async Task<Result<ProductResponseDTO>> CreateProduct([FromBody] CreateProductRequestDTO createProduct)
    {
        return await _productService.CreateProduct(createProduct);
    }

    [HttpPut("products/update/{productId}")]
    public async Task<Result<ProductResponseDTO>> UpdateProduct([FromBody] UpdateProductRequestDTO updateProduct)
    {
        return await _productService.UpdateProduct(updateProduct);
    }

    [HttpDelete("products/delete/{productId}")]
    public async Task<Result<ProductResponseDTO>> DeleteProduct(int productId)
    {
        return await _productService.DeleteProduct(productId);
    }

    [HttpPut] // PUT: api/Administration
    public Task<Result<string>> UpdateRoleForUser(int userId, string role)
    {
        throw new NotImplementedException();
    }

    [HttpGet("users/{userId}/roles")]
    public async Task<Result<IEnumerable<string>>> GetRolesForUser(int userId)
    {
        return await _userRolesService.GetRolesForUser(userId);
    }

    [HttpDelete] // DELETE: api/Administration
    public Task<Result<string>> DeleteRoleForUser(int userId)
    {
        throw new NotImplementedException();
    }
}
