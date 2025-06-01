using System.Security.Claims;

namespace FixTimeBack.Custom
{
    public interface IDecodeToken
    {
        ClaimsPrincipal GetPrincipalFromExpirationToken(string token);
    }
}
