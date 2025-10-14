using ECommerce.Application.Dtos;
using ECommerce.Domain.Entities;
using ECommerce.Helper;
using ECommerce.Infrastructure.Repository;  // IUserRepo (combine login/register repos)

namespace ECommerce.Application.Services
{
    public interface IAuthService
    {
        Task<ResponseHandler<UserOperationStatus, AuthResponseDto>> LoginAsync(string email, string password);
        Task<ResponseHandler<UserOperationStatus, AuthResponseDto>> RegisterAsync(string name, string email, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;  // Combined repo
        private readonly IPasswordHashHelper _hashHelper;
        private readonly IJwtService _jwtService;

        public AuthService(IUserRepository userRepo, IPasswordHashHelper hashHelper, IJwtService jwtService)
        {
            _userRepo = userRepo;
            _hashHelper = hashHelper;
            _jwtService = jwtService;
        }

        public async Task<ResponseHandler<UserOperationStatus, AuthResponseDto>> LoginAsync(string email, string password)
        {
            var response = new ResponseHandler<UserOperationStatus, AuthResponseDto>();
            var user = await _userRepo.GetByEmailAsync(email);
            if (user == null || !_hashHelper.VerifyPasswordHash(user.PasswordHash, user.Salt, password))
            {
                response.Status = UserOperationStatus.InvalidCredentials;
                response.Message = "Invalid credentials";
                response.Code = 401;
                return response;
            }

            var token = _jwtService.GenerateToken(user.Id, user.Email);
            response.Data = new AuthResponseDto { UserId = user.Id, Email = user.Email, Token = token };
            response.Status = UserOperationStatus.Success;
            response.Message = "Login successful";
            response.Code = 200;
            return response;
        }

        public async Task<ResponseHandler<UserOperationStatus, AuthResponseDto>> RegisterAsync(string name, string email, string password)
        {
            var response = new ResponseHandler<UserOperationStatus, AuthResponseDto>();
            var existingUser = await _userRepo.GetByEmailAsync(email);
            if (existingUser != null)
            {
                response.Status = UserOperationStatus.EmailAlreadyExists;
                response.Message = "Email already exists";
                response.Code = 400;
                return response;
            }

            var hashResult = _hashHelper.PasswordToHash(password);
            var user = new User
            {
                Email = email,
                Name = name,
                PasswordHash = hashResult.HashedPassword,
                Salt = hashResult.Salt,
            };
                //(name, email, hashResult.HashedPassword, hashResult.Salt);
            await _userRepo.AddAsync(user);

            var token = _jwtService.GenerateToken(user.Id, user.Email);
            response.Data = new AuthResponseDto { UserId = user.Id, Email = user.Email, Token = token };
            response.Status = UserOperationStatus.Success;
            response.Message = "Registration successful";
            response.Code = 201;
            return response;
        }
    }
}