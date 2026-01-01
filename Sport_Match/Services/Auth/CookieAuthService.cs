using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Sport_Match.Models;
using System.Security.Claims;

namespace Sport_Match.Services.Auth
{
    public class CookieAuthService : IAuthService
    {
<<<<<<< Updated upstream
        private bool isPersistent;

        public async Task SignInAsync(HttpContext httpContext, User user, bool isPersistant = true, bool isPersistent = false)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var claimsIdentity = new ClaimsIdentity(
               claims,
               CookieAuthenticationDefaults.AuthenticationScheme
           );
=======
        private readonly IClaimsPrincipalFactory _claimsFactory;

        public CookieAuthService(IClaimsPrincipalFactory claimsFactory)
        {
            _claimsFactory = claimsFactory;
        }

        public async Task SignInAsync(HttpContext httpContext, User user, bool isPersistent = true)
        {
            var principal = _claimsFactory.Create(user);
>>>>>>> Stashed changes

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent
            };

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
<<<<<<< Updated upstream
                new ClaimsPrincipal(claimsIdentity),
=======
                principal,
>>>>>>> Stashed changes
                authProperties
            );
        }

        public async Task SignOutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
