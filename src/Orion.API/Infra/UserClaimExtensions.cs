using System.Security.Claims;

namespace Orion.API.Infra
{
    public static class UserClaimExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user) => user?.FindFirst("sub")?.Value!;
    }
}
