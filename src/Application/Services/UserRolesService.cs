using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Ardalis.Result;
using Domain.Common.Constants;

namespace Application.Services;

public class UserRolesService : IUserRolesService
{
    private readonly IUsersRolesRepository _usersRolesRepository;

    public UserRolesService(IUsersRolesRepository usersRolesRepository)
    {
        _usersRolesRepository = usersRolesRepository;
    }

    public async Task<Result<IEnumerable<string>>> GetRolesForUser(int userId)
    {
        var roles = await _usersRolesRepository.GetRoles(userId);

        if (roles == null)
            return Result.NotFound(ReplyMessages.Error.NotFound);

        return Result.Success(roles, ReplyMessages.Success.Query);
    }
}
