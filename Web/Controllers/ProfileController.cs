using Core.Entities;
using Core.Exceptions;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers;

[Authorize]
[Route("Profile")]
public class ProfileController : Controller
{    
    private readonly IProfileService _profileService;

    private readonly IAccountContext _accountContext;

    public ProfileController(IAccountContext accountContext, IProfileService profileService)
    {
        _accountContext = accountContext;
        _profileService = profileService;
    }
    
    [Route("Index")]
    public IActionResult Index()
    {
        var account = _accountContext.Account;
        ViewBag.Login = account.Login;
        return View(new ProfileViewModel{
            FirstName = account.FirstName,
            LastName = account.LastName,
            Age = account.Age,
            City = account.City,
            Gender = (byte)account.Gender,
            Interests = account.Interests
        });
    }

    [HttpPost]
    [Route("Edit")]
    public async Task<IActionResult> Edit(ProfileViewModel model)
    {
        if (!ModelState.IsValid) return View("Index", model);

        try
        {
            await _profileService.EditAsync(new Account {                
                Login = _accountContext.Account.Login,
                FirstName = model.FirstName,
                LastName = model.LastName,
                City = model.City,
                Age = model.Age,
                Gender = (Core.Enum.GenderType)model.Gender,
                Interests = model.Interests
            });

            return RedirectToAction("Index");
        }
        catch (AccountIsNotFoundException e)
        {
            ModelState.AddModelError("", e.Message);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
        }
        return View("Edit", model);
    }
}