using System.Security.Claims;

namespace CanThoTravel.Application.Security
{
    public class UserContext
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        public static UserContext? FromClaimsPrincipal(ClaimsPrincipal principal)
        {
            if (principal.Identity?.IsAuthenticated != true)
            {
                return null;
            }

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
            {
                return null;
            }

            return new UserContext
            {
                Id = userId,
                Name = principal.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                Email = principal.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty,
                Type = principal.FindFirst("Type")?.Value ?? string.Empty
            };
        }
    }
}