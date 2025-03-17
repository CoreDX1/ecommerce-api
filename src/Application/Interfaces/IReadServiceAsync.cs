using System.Linq.Expressions;
using Ardalis.Result;

namespace Application.Interfaces;

public interface IReadServiceAsync<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    Task<Result<TDto>> GetByIdAsync(int id);
    Task<Result<IEnumerable<TDto>>> GetAllAsync();

    Task<Result<IEnumerable<TDto>>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate);
}
