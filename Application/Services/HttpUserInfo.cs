using System.Security.Claims;
using ECommerce.Application;
using Microsoft.AspNetCore.Http;

namespace CommServer.Services
{
    public class HttpUserInfo : ServiceInterfaces.IUserInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpUserInfo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;


        public int UserId
        {
            get
            {
                var claimValue = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return int.TryParse(claimValue, out var userId) ? userId : 0;
            }
        }

        public string Email =>
            User?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;

    }
}
