using Core.Repositories;
using Core.Services;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;

 namespace Web.Validator;

 public static class PrincipalValidator
{
    public static async Task ValidateAsync(CookieValidatePrincipalContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (context.Principal == null) throw new ArgumentNullException(nameof(context.Principal));

        var login = context.Principal.TryGetLogin();
        if (login == null)
        {
            context.RejectPrincipal();
            return;
        }
        
        var accountRep = context.HttpContext.RequestServices.GetRequiredService<IAccountRepository>();

        var account = await accountRep.GetAsync(login);
        if (account == null)
        {
            context.RejectPrincipal();
            return;
        }

        var accountContext = context.HttpContext.RequestServices.GetRequiredService<IAccountContext>();
        accountContext.Account = account;
    }
}