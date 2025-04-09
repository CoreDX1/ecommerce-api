using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Application.Configuration;
using Domain.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtConfig _jwtConfig;

    public JwtTokenGenerator(IUnitOfWork unitOfWork, IOptions<JwtConfig> jwtConfig)
    {
        _unitOfWork = unitOfWork;
        _jwtConfig = jwtConfig.Value;
    }

    public async Task<string> GenerateTokenAsync(User user)
    {
        IEnumerable<string> userRoles = await _unitOfWork.UsersRolesRepository.GetRoles(user.UserId);

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sub, user.Username),
            new(JwtRegisteredClaimNames.Jti, user.UserId.ToString()),
            // Cuando se quiere validar el tiempo de caducidad del token
            // new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
            // Cuando se quiere validar el tiempo de caducidad del token
            // new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()),
        ];

        foreach (var roleName in userRoles)
        {
            // claims.Add(new Claim("role", roleName));
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
