using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Sport_Match.Models;

namespace Sport_Match.Services.Auth
{
    public class ClaimsPrincipalFactory : IClaimsPrincipalFactory
    {
        public ClaimsPrincipal Create(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return new ClaimsPrincipal(identity);
        }
    }
}
