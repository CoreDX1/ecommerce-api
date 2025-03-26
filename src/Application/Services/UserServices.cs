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
using Domain.Common.Constants;
using Domain.Entity;
using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class UserServices : GenericServiceAsync<User, UserResponseDTO>, IUserService
{
    private readonly JwtConfig _jwtConfig;
    private readonly IValidator<LoginUserRequestDTO> _validator;
    private readonly IValidatorServices _validatorServices;

    public UserServices(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<LoginUserRequestDTO> validator,
        IOptions<JwtConfig> jwtConfig,
        IValidatorServices validatorServices
    )
        : base(unitOfWork, mapper)
    {
        _validator = validator;
        _jwtConfig = jwtConfig.Value;
        _validatorServices = validatorServices;
    }

    public async Task<Result<UserResponseDTO>> RegisterAsync(CreateUserRequestDTO createUser)
    {
        User newUser = await _unitOfWork.UserRepository.RegisterUser(createUser);

        if (newUser == null)
            return Result.NotFound(ReplyMessages.Error.NotFound);

        var userResponse = _mapper.Map<UserResponseDTO>(newUser);

        return Result.Success(userResponse, ReplyMessages.Success.Save);
    }

    public async Task<Result<UserResponseDTO>> LoginAsync(LoginUserRequestDTO loginUser)
    {
        var validationResult = await _validator.ValidateAsync(loginUser);

        if (!validationResult.IsValid)
            return Result.Invalid(_validatorServices.GetValidationError(validationResult));

        User authenticatedUser = await _unitOfWork.UserRepository.AuthenticateAsync(loginUser);

        if (authenticatedUser == null)
            return Result.NotFound(ReplyMessages.Validate.ValidateError);

        var userResponse = _mapper.Map<UserResponseDTO>(authenticatedUser);

        userResponse.VerificationToken = await GenerateJwtTokenAsync(authenticatedUser);

        return Result.Success(userResponse, ReplyMessages.Success.Query);
    }

    private async Task<string> GenerateJwtTokenAsync(User loginUser)
    {
        IEnumerable<string> userRoles = await _unitOfWork.UsersRolesRepository.GetRoles(loginUser.UserId);

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sub, loginUser.Username),
            new(JwtRegisteredClaimNames.Jti, loginUser.UserId.ToString()),
            // Cuando se quiere validar el tiempo de caducidad del token
            // new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
            // Cuando se quiere validar el tiempo de caducidad del token
            // new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()),
        ];

        foreach (var roleName in userRoles)
        {
            claims.Add(new Claim("role", roleName));
            claims.Add(new Claim(ClaimTypes.Role, roleName));
        }

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));

        SigningCredentials singningCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtToken = new(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: singningCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
