using Domain.Entity;

namespace Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    public Task<string> GenerateTokenAsync(User user);
}
