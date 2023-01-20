using Core.Enum;

namespace Core.Entities;

public class Friendship
{
    public Guid RequestedId { get; set; }
    public Guid AddresserId { get; set; }
    public FriendshipStatusType Status { get; set; }
    public DateTime Created { get; set; }
}