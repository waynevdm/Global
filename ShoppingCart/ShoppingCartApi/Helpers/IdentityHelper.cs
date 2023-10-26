using System.Security.Claims;

namespace ShoppingCartApi.Helpers
{
    public static class IdentityHelper
    {
        public static int GetUserId(ClaimsPrincipal user)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            return int.Parse(claimsIdentity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value);
        }
    }
}
