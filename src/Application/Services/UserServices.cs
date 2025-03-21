using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs.Request.User;
using Application.DTOs.Response.User;
using Application.Interfaces;
using Application.Interfaces.Persistence;
using Ardalis.Result;
using AutoMapper;
using Domain.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class UserServices : GenericServiceAsync<User, UserResponseDTO>, IUserService
{
    private readonly IConfiguration _configuration;

    public UserServices(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        : base(unitOfWork, mapper)
    {
        _configuration = configuration;
    }

    public async Task<Result<UserResponseDTO>> RegisterUser(CreateUserRequestDTO createUser)
    {
        User user = await _unitOfWork.User.RegisterUser(createUser);

        if (user == null)
            return Result.NotFound("User not found");

        var userResponse = _mapper.Map<UserResponseDTO>(user);

        return Result.Success(userResponse, "User created successfully");
    }

    public async Task<Result<UserResponseDTO>> LoginUser(LoginUserRequestDTO loginUser)
    {
        User user = await _unitOfWork.User.LoginUser(loginUser);

        if (user == null)
            return Result.NotFound("User not found");

        var userResponse = _mapper.Map<UserResponseDTO>(user);

        userResponse.VerificationToken = await GenerateJwtToken(user);

        return Result.Success(userResponse, "User created successfully");
    }

    private async Task<string> GenerateJwtToken(User loginUser)
    {
        var roles = await _unitOfWork.UsersRoles.GetRoles(loginUser.UserId);

        foreach (var role in roles)
        {
            Console.WriteLine(role);
        }

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, loginUser.Username),
            new(JwtRegisteredClaimNames.Jti, loginUser.UserId.ToString()),
            // Cuando se quiere validar el tiempo de caducidad del token
            // new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
            // Cuando se quiere validar el tiempo de caducidad del token
            // new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim("role", role));
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
