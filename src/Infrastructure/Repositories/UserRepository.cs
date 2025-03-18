using Application.DTOs.Request.User;
using Application.Interfaces.Repositories;
using BCrypt.Net;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly IGenericRepository<Customer> _customerRepository;

    public UserRepository(PostgresContext context, IGenericRepository<Customer> customerRepository)
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

    public async Task<User> LoginUser(LoginUserRequestDTO loginUser)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginUser.Email);

        if (!Verify(loginUser.Password, user!.PasswordHash))
        {
            return null!;
        }

        user.LastLogin = DateTime.Now;

        await _context.SaveChangesAsync();
        return user;
    }
}
