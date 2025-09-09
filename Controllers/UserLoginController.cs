using ECommerce.Dtos;
using ECommerce.Enums;
using ECommerce.Helper;
using ECommerce.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{

    [ApiController]
    [Route("/")]
    public class UserLoginController : ControllerBase
    {
        private readonly IUserLoginRepo _userLoginRepo;

        private readonly IUserRegisterRepo _userRegisterRepo;

        public UserLoginController(IUserLoginRepo userLoginRepo, IUserRegisterRepo userRegisterRepo)
        {
            _userLoginRepo = userLoginRepo;
            _userRegisterRepo = userRegisterRepo;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginRequestDto userLoginReqDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var response = new
                {
                    Status = 400,
                    Message = "Invalid login request. Please check your input.",
                    Errors = errors
                };

                return BadRequest(response);
            }
            var result = await _userLoginRepo.Login(userLoginReqDto.UserName, userLoginReqDto.Password);
            if (result.Status == UserLoginStatus.Error)
            {
                return NotFound(result);

            }

            ResponseHandler<UserLoginStatus, UserDto> resHandler = new ResponseHandler<UserLoginStatus, UserDto>();
            resHandler.Message = "Login Successful";
            resHandler.Status = UserLoginStatus.Success;
            resHandler.Data = result.UserData;

            return Ok(result);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterDto userRegisterReqDto)
        {
            var result = await _userRegisterRepo.RegisterUser(userRegisterReqDto.Name, userRegisterReqDto.Email, userRegisterReqDto.Password);
            if (result == UserRegistrationStatus.Error )
            {
                return NotFound(result);

            } else if (result == UserRegistrationStatus.EmailAlreadyExists)
            {
                ResponseHandler<UserRegistrationStatus, UserRegistrationStatus> resErrHandler = new ResponseHandler<UserRegistrationStatus, UserRegistrationStatus>();
                resErrHandler.Message = "Email already exists";
                resErrHandler.Status = UserRegistrationStatus.EmailAlreadyExists;
                resErrHandler.Data = result;
                return Ok(resErrHandler);
            }
            ResponseHandler<UserRegistrationStatus, UserRegistrationStatus> resHandler = new ResponseHandler<UserRegistrationStatus, UserRegistrationStatus>();
            resHandler.Message = "Register Successful";
            resHandler.Status = UserRegistrationStatus.Success;
            resHandler.Data = result;
            return Ok(resHandler);
        }

    }
}



