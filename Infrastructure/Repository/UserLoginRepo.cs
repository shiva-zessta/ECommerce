using AutoMapper;
using ECommerce.Application.Dtos;
using ECommerce.Enums;
using ECommerce.Helper;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repository
{
    public interface IUserLoginRepo
    {
        Task<UserLoginDto> Login(string email, string password);
    }
    public class UserLoginRepo : IUserLoginRepo
    {
        private readonly MyDbContext _dbContext;

        private readonly IMapper _mapper;

        public UserLoginRepo(MyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<UserLoginDto> Login(string email, string password)
        {

            UserLoginDto userLoginDto = new UserLoginDto();
            var userDetails = await _dbContext.Users.Where(t => t.Email == email).FirstOrDefaultAsync();
            if (userDetails == null)
            {
                userLoginDto.UserData = null;
                userLoginDto.Status = UserLoginStatus.UserNotExists;
                return userLoginDto;
            }
            PasswordHashHelper passwordHashHelper = new PasswordHashHelper();
            var isPasswordCorrect = passwordHashHelper.VerifyPasswordHash(userDetails.PasswordHash, userDetails.Salt, password);
            if (isPasswordCorrect)
            {
                var userDto = _mapper.Map<UserDto>(userDetails);

                userLoginDto.UserData = userDto;
                userLoginDto.Status = UserLoginStatus.Success;
                return userLoginDto;
            }
            userLoginDto.Status = UserLoginStatus.Error;
            userLoginDto.UserData = null;


            return userLoginDto;
        }

    }
}
