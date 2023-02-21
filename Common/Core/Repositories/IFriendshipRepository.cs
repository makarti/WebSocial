using Core.Entities;
using Core.Enum;

namespace Core.Repositories;

public interface IFriendshipRepository
{
    Task<IEnumerable<Friendship>> GetsAsync(Guid accountId, FriendshipStatusType statusType);
    Task<Friendship> GetAsync(Guid requesterId, Guid addresserId);
    Task AddAsync(Guid requesterId, Guid addresserId);
    Task UpdateStatusAsync(Guid requesterId, Guid addresserId, FriendshipStatusType statusType);
    Task RemoveAsync(Guid requesterId, Guid addresserId);
}