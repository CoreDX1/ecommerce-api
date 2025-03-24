using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Application.Configuration;
using Application.DTOs.Request.User;
using Application.DTOs.Response.User;
using Ardalis.Result;
using AutoMapper;
using Domain.Entity;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class UserServices : GenericServiceAsync<User, UserResponseDTO>, IUserService
{
    private readonly JwtConfig _jwtConfig;
    private readonly IValidator<LoginUserRequestDTO> _validator;

    public UserServices(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<LoginUserRequestDTO> validator,
        IOptions<JwtConfig> jwtConfig
    )
        : base(unitOfWork, mapper)
    {
        _validator = validator;
        _jwtConfig = jwtConfig.Value;
    }

    public async Task<Result<UserResponseDTO>> RegisterUser(CreateUserRequestDTO createUser)
    {
        User user = await _unitOfWork.User.RegisterUser(createUser);

        if (user == null)
            return Result.NotFound("User not found");

        var userResponse = _mapper.Map<UserResponseDTO>(user);

        return Result.Success(userResponse, "User created successfully");
    }

    private static List<ValidationError> GetValidationError(ValidationResult validationResult)
    {
        List<ValidationError> validationError = [];

        foreach (var error in validationResult.Errors)
        {
            validationError.Add(
                new ValidationError()
                {
                    ErrorMessage = error.ErrorMessage,
                    Identifier = error.PropertyName,
                    ErrorCode = error.ErrorCode,
                }
            );
        }

        return validationError;
    }

    public async Task<Result<UserResponseDTO>> LoginUser(LoginUserRequestDTO loginUser)
    {
        var validationResult = await _validator.ValidateAsync(loginUser);

        if (!validationResult.IsValid)
            return Result.Invalid(GetValidationError(validationResult));

        User user = await _unitOfWork.User.LoginUser(loginUser);

        if (user == null)
            return Result.NotFound("User not found");

        var userResponse = _mapper.Map<UserResponseDTO>(user);

        userResponse.VerificationToken = await GenerateJwtToken(user);

        return Result.Success(userResponse, "User created successfully");
    }

    private async Task<string> GenerateJwtToken(User loginUser)
    {
        List<string> roles = await _unitOfWork.UsersRoles.GetRoles(loginUser.UserId);

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sub, loginUser.Username),
            new(JwtRegisteredClaimNames.Jti, loginUser.UserId.ToString()),
            // Cuando se quiere validar el tiempo de caducidad del token
            // new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
            // Cuando se quiere validar el tiempo de caducidad del token
            // new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()),
        ];

        foreach (var role in roles)
        {
            claims.Add(new Claim("role", role));
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
