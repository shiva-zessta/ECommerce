using ECommerce.Application.Dtos;
using ECommerce.Application.Services;
using ECommerce.Enums;
using ECommerce.Helper;
using ECommerce.Infrastructure.Persistence;

namespace ECommerce.Middelware
{
    public class ValidateUserMiddleware : BaseService
    {
        private readonly RequestDelegate _next;

        public ValidateUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, MyDbContext dbContext)
        {
            // Run only for authenticated users
            if (context.User?.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdClaim, out int userId))
                {
                    var userExists = await dbContext.Users.FindAsync(userId);

                    if (userExists == null)
                    {
                        // User not found — return standardized response
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                         BuildResponse<AddressStatus, AddressDto>("Error-------", AddressStatus.Error, 400);
                        return;
                    }
                }
            }

            await _next(context);
        }
    }

}
