using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Core.Exceptions;
using Core.Services;
using Web.ViewModels;
using IAuthenticationService = Core.Services.IAuthenticationService;

namespace Web.Controllers;

[Route("Login")]
public class LoginController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public LoginController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View("Index", model);
        
        try
        {
            await _authenticationService.LoginAsync(model.Login, model.Password);
            return RedirectToAction("Index", "Home");
        }
        catch (AuthenticationException e)
        {
            ModelState.AddModelError("", e.Message);
        }
        return View("Index", model);
    }

    [HttpGet]
    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
