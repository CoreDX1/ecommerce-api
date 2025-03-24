using Application.Common.Interfaces.Persistence;
using Application.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly PostgresContext _context;

    public IProductRepository ProductRepository { get; }

    public ICustomerRepository CustomerRepository { get; }

    public IUserRepository UserRepository { get; }

    public IUsersRolesRepository UsersRolesRepository { get; }

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
        IUsersRolesRepository usersRolesRepository
    )
    {
        _context = context;
        ProductRepository = productRepository;
        CustomerRepository = customerRepository;
        UserRepository = userRepository;
        UsersRolesRepository = usersRolesRepository;
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
