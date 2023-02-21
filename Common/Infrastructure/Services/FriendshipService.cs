using Core.Entities;
using Core.Enum;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Services;

public class FriendshipService : IFriendshipService
{
    public readonly IAccountContext _accountContext;
    public readonly IFriendshipRepository _friendshipRep;
    public FriendshipService(IAccountContext accountContext, IFriendshipRepository friendshipRep)
    {
        _accountContext = accountContext;
        _friendshipRep = friendshipRep;
    }

    public async Task<IEnumerable<Friendship>> GetsAsync(FriendshipStatusType statusType)
    {
        return await _friendshipRep.GetsAsync(_accountContext.Account.Id, statusType);
    }

    public async Task AddAsync(Guid addresserId)
    {
        await _friendshipRep.AddAsync(_accountContext.Account.Id, addresserId);
    }

    public async Task AcceptAsync(Guid requesterId)
    {
        await _friendshipRep.UpdateStatusAsync(requesterId, _accountContext.Account.Id, FriendshipStatusType.RequestAccepted);
    }

    public async Task RemoveAsync(Guid accountId)
    {
        var friendship = await _friendshipRep.GetAsync(_accountContext.Account.Id, accountId);
        if(friendship == null)
            friendship = await _friendshipRep.GetAsync(accountId, _accountContext.Account.Id);
        
        if(friendship != null)
            await _friendshipRep.RemoveAsync(friendship.RequesterId, friendship.AddresserId);
    }
}