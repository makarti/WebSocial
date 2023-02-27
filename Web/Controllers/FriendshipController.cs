using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers;

[Authorize]
[Route("Friendship")]
public class FriendshipController : Controller
{
    private readonly IFriendshipService _friendshipService;
    private readonly IAccountContext _accountContext;
    
    public FriendshipController(IFriendshipService friendshipService, IAccountContext accountContext)
    {
        _friendshipService = friendshipService;
        _accountContext = accountContext;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        var friends = await _friendshipService.GetsAsync(Core.Enum.FriendshipStatusType.RequestAccepted);
        return View(friends.Select(s => 
        {
            var f = _accountContext.Account.Id != s.AddresserId ? s.Addresser : s.Requester;
            return new ProfileViewModel
            {
                Id = f.Id,
                Login = f.Login,
                FirstName = f.FirstName,
                LastName = f.LastName,
                Age = f.Age,
                City = f.City,
                Gender = f.Gender == Core.Enum.GenderType.Female ? "Женский" : "Мужской",
                Interests = f.Interests  
            };
        }));
    }

    [Route("Incoming")]
    public async Task<IActionResult> Incoming()
    {
        var requests = (await _friendshipService.GetsAsync(Core.Enum.FriendshipStatusType.RequestSent))
                                                .Where(w => w.AddresserId == _accountContext.Account.Id)
                                                .Select(s => s.Requester)
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
            
        return View(requests);
    }

    [Route("SentRequests")]
    public async Task<IActionResult> SentRequests()
    {
        var requests = (await _friendshipService.GetsAsync(Core.Enum.FriendshipStatusType.RequestSent))
                                                .Where(w => w.RequesterId == _accountContext.Account.Id)
                                                .Select(s => s.Addresser)
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
        return View(requests);
    }

    [Route("AddFriend/{id}")]
    public async Task<IActionResult> AddFriend(Guid id)
    {
        await _friendshipService.AddAsync(id);
        return RedirectToAction("SentRequests");
    }

    [Route("AcceptFriend/{id}")]
    public async Task<IActionResult> AcceptFriend(Guid id)
    {
        await _friendshipService.AcceptAsync(id);
        return RedirectToAction("Incoming");
    }

    [Route("RemoveFriend/{id}")]
    public async Task<IActionResult> RemoveFriend(Guid id)
    {
        await _friendshipService.RemoveAsync(id);
        return RedirectToAction("Index");
    }
}