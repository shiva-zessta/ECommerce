using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email);
    Task AddAsync(User user);
}

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _context;
    public UserRepository(MyDbContext context) { _context = context; }

    public async Task<User> GetByEmailAsync(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}