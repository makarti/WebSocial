using System;
using System.Linq;
using System.Security.Claims;

namespace Infrastructure.Extensions;

public static class ClaimExtension
{
    public static long GetAccountId(this ClaimsPrincipal principal)
    {
        var accountId = principal.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.Id)?.Value;
        if (!long.TryParse(accountId, out var id)) throw new ArgumentException("Invalid id claim");
        return id;
    }
    public static string GetLogin(this ClaimsPrincipal principal)
    {
        var login = TryGetLogin(principal);
        if (string.IsNullOrEmpty(login)) throw new ArgumentException("Invalid login claim");
        return login;
    }
    public static string TryGetLogin(this ClaimsPrincipal principal)
    {
        return principal.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.Login)?.Value;
    }
}

public static class ClaimConstants
{
    public const string Id = "WebSocial.Id";
    public const string Login = "WebSocial.Login";
}