using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Sport_Match.Models;

namespace Sport_Match.Services.Auth
{
    public class CookieAuthService : IAuthService
    {
        public async Task SignInAsync(
            HttpContext httpContext,
            User user,
            bool isPersistent = false)
        {
            var claims = new List<System.Security.Claims.Claim>
            {
                //new System.Security.Claims.Claim(
                //    System.Security.Claims.ClaimTypes.NameIdentifier,
                //    user.Id.ToString()),

                new System.Security.Claims.Claim(
                    System.Security.Claims.ClaimTypes.Email,
                    user.Email),

                new System.Security.Claims.Claim(
                    System.Security.Claims.ClaimTypes.Name,
                    user.Email)
            };

            var claimsIdentity = new System.Security.Claims.ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent
            };

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new System.Security.Claims.ClaimsPrincipal(claimsIdentity),
                authProperties
            );
        }

        public async Task SignOutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );
        }
    }
}
