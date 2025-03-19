using System.Linq.Expressions;

namespace Application.Interfaces.Repositories;

/// <summary>
/// THE base class for all entities.
/// </summary>
/// <typeparam name="TKey">The type of the key for the entity.</typeparam>
public abstract class Entity<TKey>
{
    private TKey _id;

    public Entity()
        : this(default(TKey)!) { }

    public Entity(TKey id)
    {
        _id = id;
    }

    public Entity(Entity<TKey> source)
        : this(default(TKey)!)
    {
        if (source != null)
        {
            this._id = source._id;
        }
    }

    public TKey Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public bool IsTransient()
    {
        return Id!.Equals(default(TKey));
    }
}

public class FindOptions
{
    public bool IsIgnoreAutoIncludes { get; set; }
    public bool IsAsNoTracking { get; set; }
}

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(FindOptions? findOptions = null, CancellationToken cancellationToken = default);
    Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> expression, FindOptions? findOptions = null, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    // IQueryable<TEntity> GetAll<T, Tkey>()
    //     where T : Entity<Tkey>;
}
