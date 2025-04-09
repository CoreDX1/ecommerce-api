using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly PostgresContext _context;

    public IProductRepository ProductRepository { get; }

    public ICustomerRepository CustomerRepository { get; }

    public IUserRepository UserRepository { get; }

    public IUsersRolesRepository UsersRolesRepository { get; }

    public IOrderRepository OrderRepository { get; }

    public IRepository<TEntity> Repository<TEntity>()
        where TEntity : class
    {
        return new Repository<TEntity>(_context);
    }

    public UnitOfWork(
        PostgresContext context,
        IProductRepository productRepository,
        ICustomerRepository customerRepository,
        IUserRepository userRepository,
        IUsersRolesRepository usersRolesRepository,
        IOrderRepository orderRepository
    )
    {
        _context = context;
        ProductRepository = productRepository;
        CustomerRepository = customerRepository;
        UserRepository = userRepository;
        UsersRolesRepository = usersRolesRepository;
        OrderRepository = orderRepository;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
