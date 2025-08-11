using CanThoTravel.Application.Security;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CanThoTravel.API.Middleware
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var user = UserContext.FromClaimsPrincipal(context.User);
            if (user != null)
            {
                context.Items["UserContext"] = user;
            }

            await _next(context);
        }
    }
}