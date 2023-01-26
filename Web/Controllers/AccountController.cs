using Core.Entities;
using Core.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;
using IAuthenticationService = Core.Services.IAuthenticationService;

namespace Web.Controllers;

[Route("Account")]
public class AccountController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public AccountController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpGet]
    [Route("Index")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("Login")]
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

    [HttpGet]   
    [Route("Register")] 
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View("Index", model);

        var createdAccount = new Account {
            Login = model.Login,
            FirstName = model.FirstName,
            LastName = model.LastName,
            City = model.City,
            Age = model.Age,
            Gender = (Core.Enum.GenderType)model.Gender,
            Interests = model.Interests
        };

        try
        {
            await _authenticationService.RegisterAsync(createdAccount, model.Password);
            await _authenticationService.LoginAsync(model.Login, model.Password);

            return RedirectToAction("CreateProfile");
        }
        catch (AuthenticationException e)
        {
            ModelState.AddModelError("", e.Message);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
        }


        return View("Index", model);
    }
}
