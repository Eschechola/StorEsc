using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StorEsc.Api.Token.Extensions;

public static class IdentityExtension
{
    public static string GetId(this ClaimsPrincipal principal)
        => principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public static string GetEmail(this ClaimsPrincipal principal)
        => principal.FindFirst(ClaimTypes.Email)?.Value;

    public static string GetRole(this ClaimsPrincipal principal)
        => principal.FindFirst(ClaimTypes.Role)?.Value;

    public static string GeTokenIdentifier(this ClaimsPrincipal principal)
        => principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
    
    public static string GetTokenNotBefore(this ClaimsPrincipal principal)
        => principal.FindFirst(JwtRegisteredClaimNames.Nbf)?.Value;
    
    public static string GetTokenIssuedAt(this ClaimsPrincipal principal)
        => principal.FindFirst(JwtRegisteredClaimNames.Iat)?.Value;
}