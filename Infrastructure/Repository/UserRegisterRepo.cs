using ECommerce.Domain.Entities;
using ECommerce.Enums;
using ECommerce.Helper;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repository
{
    public interface IUserRegisterRepo
    {
        Task<UserRegistrationStatus> RegisterUser(string name, string email, string password);
    }
    public class UserRegisterRepo : IUserRegisterRepo
    {

        private readonly MyDbContext _context;

        public UserRegisterRepo(MyDbContext context)
        {
            _context = context;
        }

        public async Task<UserRegistrationStatus> RegisterUser(string name, string email, string password)
        {
                User existingUser = await _context.Users.Where(t => t.Email == email).FirstOrDefaultAsync();
                if (existingUser != null) return UserRegistrationStatus.EmailAlreadyExists;
                PasswordHashHelper passwordHashHelper = new PasswordHashHelper();
                var hashedPassword = passwordHashHelper.PasswordToHash(password);
                User user = new User();
                user.Email = email;
                user.PasswordHash = hashedPassword.HashedPassword;
                user.Salt = hashedPassword.Salt;
                user.Name = name;
                _context.Users.Add(user);
                _context.SaveChanges();
                return UserRegistrationStatus.Success;
        }
    }
}

