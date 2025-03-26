namespace Application.Common.Interfaces;

public interface IGenericServiceAsync<TEntity, TResponseDto> : IReadServiceAsync<TEntity, TResponseDto>
    where TEntity : class
    where TResponseDto : class
{
    Task AddAsync(TResponseDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(TResponseDto dto);
}
