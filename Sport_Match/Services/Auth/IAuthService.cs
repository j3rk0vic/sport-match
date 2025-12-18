using Sport_Match.Models;

namespace Sport_Match.Services.Auth
{
    public interface IAuthService
    {
        Task SignInAsync(HttpContext httpContext, User user, bool isPersistant = true, bool isPersistent = false);
        Task SignOutAsync(HttpContext httpContext);
    }
}
