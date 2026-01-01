using System.Security.Claims;
using Sport_Match.Models;

namespace Sport_Match.Services.Auth
{
    public interface IClaimsPrincipalFactory
    {
        ClaimsPrincipal Create(User user);
    }
}
