using ECommerce.Application.Dtos;
using ECommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(GetModelErrors());

        var result = await _authService.LoginAsync(request.UserName, request.Password);  // Assume UserName is Email
        return result.Status == UserOperationStatus.Success ? Ok(result) : StatusCode(result.Code, result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto request)
    {
        if (!ModelState.IsValid) return BadRequest(GetModelErrors());

        var result = await _authService.RegisterAsync(request.Name, request.Email, request.Password);
        return result.Status == UserOperationStatus.Success ? Created("", result) : StatusCode(result.Code, result);
    }

    private object GetModelErrors() => new
    {
        Status = 400,
        Message = "Invalid request",
        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
    };
}