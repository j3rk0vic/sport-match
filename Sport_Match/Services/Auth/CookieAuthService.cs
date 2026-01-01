using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Sport_Match.Models;

namespace Sport_Match.Services.Auth
{
    public class CookieAuthService : IAuthService
    {
        private readonly IClaimsPrincipalFactory _claimsFactory;

        public CookieAuthService(IClaimsPrincipalFactory claimsFactory)
        {
            _claimsFactory = claimsFactory;
        }

        public async Task SignInAsync(HttpContext httpContext, User user, bool isPersistent = true)
        {
            var principal = _claimsFactory.Create(user);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent
            };

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authProperties
            );
        }

        public async Task SignOutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
