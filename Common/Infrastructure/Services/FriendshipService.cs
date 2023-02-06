using Core.Repositories;
using Core.Services;

namespace Infrastructure.Services;

public class FriendshipService : IFriendshipService
{
    public readonly IAccountContext _accountContext;
    public readonly IAccountRepository _accountRep;
    public readonly IFriendshipRepository _friendshipRep;
    public FriendshipService(IAccountContext accountContext, IAccountRepository accountRep, IFriendshipRepository friendshipRep)
    {
        _accountContext = accountContext;
        _accountRep = accountRep;
        _friendshipRep = friendshipRep;
    }

    public Task Get()
    {
        throw new NotImplementedException();
    }

    public Task Add()
    {
        throw new NotImplementedException();
    }

    public Task Accept()
    {
        throw new NotImplementedException();
    }

    public Task Remove()
    {
        throw new NotImplementedException();
    }
}