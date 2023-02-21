
using Core.Handlers;
using Core.Repositories;
using Core.Services;
using Dapper;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Web.Validator;

var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;

services.AddControllersWithViews();
services.AddHttpContextAccessor();

SqlMapper.AddTypeHandler(new SqlGuidTypeHandler());

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.Events = new CookieAuthenticationEvents
            {
                OnValidatePrincipal = PrincipalValidator.ValidateAsync
            };
        });
        
services.AddScoped<ISignInManager, SignInManager>();
services.AddScoped<IAuthenticationService, AuthenticationService>();
services.AddScoped<IProfileService, ProfileService>();
services.AddScoped<IAccountContext, AccountContext>();
services.AddScoped<IAccountRepository, AccountRepository>();
services.AddScoped<IFriendshipRepository, FriendshipRepository>();
services.AddScoped<IFriendshipService, FriendshipService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
