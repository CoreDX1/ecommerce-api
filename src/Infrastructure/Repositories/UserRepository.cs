using System.Linq.Expressions;
using Application.Common.Interfaces.Repositories;
using Application.DTOs.Request.User;
using Application.DTOs.Response.User;
using Application.Interfaces.Repositories;
using Ardalis.Result;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserServices
{
    private readonly IRepository<Customer> _customerRepository;

    public UserRepository(PostgresContext context, IRepository<Customer> customerRepository)
        : base(context)
    {
        _customerRepository = customerRepository;
    }

    public async Task<User> RegisterUser(CreateUserRequestDTO createUser)
    {
        Customer customer = new()
        {
            Address = createUser.Address,
            Email = createUser.Email,
            FirstName = createUser.Username,
            LastName = createUser.UserLastName,
            PhoneNumber = createUser.PhoneNumber,
        };

        await _customerRepository.AddAsync(customer);

        Customer? customerCreated = await _context.Customers.FirstOrDefaultAsync(x => x.Email == createUser.Email);

        User user = new()
        {
            CustomerId = customerCreated!.CustomerId,
            Username = createUser.Username,
            PasswordHash = PasswordHash(createUser.PasswordHash),
            Email = createUser.Email,
            IsActive = true,
        };

        await AddAsync(user);

        User? userCreated = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == createUser.Email);

        return userCreated!;
    }

    private static string PasswordHash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    private static bool Verify(string password, string passwordHash) => BCrypt.Net.BCrypt.Verify(password, passwordHash);

    public async Task<User> AuthenticateAsync(LoginUserRequestDTO loginUser)
    {
        User? user = await SingleOrDefaultAsync(x => x.Email == loginUser.Email);

        if (user == null || !Verify(loginUser.Password, user!.PasswordHash))
        {
            return null!;
        }

        user.LastLogin = DateTime.Now;

        await _context.SaveChangesAsync();
        return user;
    }

    public Task AddAsync(UserResponseDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UserResponseDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<UserResponseDTO>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<UserResponseDTO>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<UserResponseDTO>>> GetByConditionAsync(Expression<Func<User, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}
