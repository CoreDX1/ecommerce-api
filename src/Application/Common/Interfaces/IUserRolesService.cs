using Ardalis.Result;

namespace Application.Common.Interfaces;

public interface IUserRolesService
{
    Task<Result<IEnumerable<string>>> GetRolesForUser(int userId);
}
