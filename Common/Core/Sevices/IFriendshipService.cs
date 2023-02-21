using Core.Entities;
using Core.Enum;

namespace Core.Services;
public interface IFriendshipService
{
    Task<IEnumerable<Friendship>> GetsAsync(FriendshipStatusType statusType);
    Task AddAsync(Guid addresserId);
    Task AcceptAsync(Guid requesterId);
    Task RemoveAsync(Guid accountId);    
}