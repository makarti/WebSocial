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
        return View(new EditProfileViewModel{
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
    public async Task<IActionResult> Edit(EditProfileViewModel model)
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

    [Route("Search")]
    public async Task<IActionResult> Search(string name)
    {
        var accounts = (await _profileService.SearchAsync(name))
                                             .Select(s => new ProfileViewModel
                                             {
                                                 Id = s.Id,
                                                 Login = s.Login,
                                                 FirstName = s.FirstName,
                                                 LastName = s.LastName,
                                                 Age = s.Age,
                                                 City = s.City,
                                                 Gender = s.Gender == Core.Enum.GenderType.Female ? "Женский" : "Мужской",
                                                 Interests = s.Interests
                                             });
        return View(accounts);
    }

    [Route("Get/{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var account = await _profileService.GetByIdAsync(id);
        return View(new ProfileViewModel{
            Id = account.Id,
            Login = account.Login,
            FirstName = account.FirstName,
            LastName = account.LastName,
            Age = account.Age,
            City = account.City,
            Gender = account.Gender == Core.Enum.GenderType.Female ? "Женский" : "Мужской",
            Interests = account.Interests
        });
    }
}