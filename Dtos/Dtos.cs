using ECommerce.Entities;
using ECommerce.Enums;

namespace ECommerce.Dtos
{
       public class PasswordHashResultDto
        {
            public string HashedPassword { get; set; }
            public string Salt { get; set; }
        }

    public class UserLoginRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserRegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }


    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }

    }

    public class UserLoginDto
    {
        public UserDto UserData { get; set; }
        public UserLoginStatus Status { get; set; }
    }
}

