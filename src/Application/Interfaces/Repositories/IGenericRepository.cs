namespace Application.Interfaces.Repositories;

public interface IGenericRepository<TEntity>
    where TEntity : class
{
    public Task<TEntity> AddAsync(TEntity entity);
    public Task UpdateAsync(TEntity entity);
    public void DeleteAsync(TEntity entity);
    public Task<TEntity> GetByIdAsync(int id);
    public Task<List<TEntity>> GetAllAsync();
}
