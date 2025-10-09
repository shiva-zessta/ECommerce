using ECommerce.Application.Dtos;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace ECommerce.Helper
{
    public interface IPasswordHashHelper
    {
        PasswordHashResultDto PasswordToHash(string password);
        bool VerifyPasswordHash(string passwordHash, string salt, string password);
    }
    public class PasswordHashHelper : IPasswordHashHelper
    {
        public PasswordHashResultDto PasswordToHash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return new PasswordHashResultDto { HashedPassword = hashed, Salt = Convert.ToBase64String(salt) };
        }

        public bool VerifyPasswordHash(string passwordHash, string salt, string password)
        {
            string storedHash = passwordHash;
            byte[] storedSalt = Convert.FromBase64String(salt);
            string enteredHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: storedSalt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,  // Same iteration count as used during the initial hashing
            numBytesRequested: 256 / 8));
            return storedHash == enteredHash;
        }
    }
}
