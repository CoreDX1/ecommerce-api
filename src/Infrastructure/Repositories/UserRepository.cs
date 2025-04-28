using Application.Common.Interfaces.Repositories;
using Application.DTOs.Request.User;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly IRepository<Customer> _customerRepository;

    public UserRepository(PostgresContext context, IRepository<Customer> customerRepository)
        : base(context)
    {
        _customerRepository = customerRepository;
    }

    public async Task<User> RegisterUser(RegisterUserRequestDTO createUser)
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
            PasswordHash = PasswordHash(createUser.Password),
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

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await SingleOrDefaultAsync(x => x.Email == email);
        if (user == null)
            return null!;

        return user;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        User? user = await SingleOrDefaultAsync(x => x.Username == username);

        if (user == null)
            return null!;

        return user;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await AnyAsync(x => x.Email == email);
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await AnyAsync(x => x.Username == username);
    }

    public async Task<bool> UpdatePasswordAsync(int userId, string newPasswordHash)
    {
        var user = await GetByIdAsync(userId);
        if (user == null)
            return false;

        user.PasswordHash = PasswordHash(newPasswordHash);
        await UpdateAsync(user);
        return true;
    }

    public async Task<bool> UpdateUserProfileAsync(User user)
    {
        try
        {
            await UpdateAsync(user);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeactivateUserAsync(int userId)
    {
        var user = await GetByIdAsync(userId);
        if (user == null)
            return false;

        user.IsActive = false;
        await UpdateAsync(user);
        return true;
    }

    public async Task<bool> ReactivateUserAsync(int userId)
    {
        var user = await GetByIdAsync(userId);
        if (user == null)
            return false;

        user.IsActive = true;
        await UpdateAsync(user);
        return true;
    }

    public Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
    {
        throw new NotImplementedException();
    }

}
