using Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.ViewModels;

namespace Web.Controllers;

[Route("Home")]
public class HomeController : Controller
{
    private readonly IAccountRepository _accountRepository;
    public HomeController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public IActionResult Index()
    {
        return View();
    }

    [Route("SearchTest")]
    public async Task<IActionResult> SearchTest()
    {
        var accounts = (await _accountRepository.SearchTestAsync().ConfigureAwait(false))
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
        return Json(accounts);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
