using Core.Enum;

namespace Core.Entities;

public class Friendship
{
    public Guid RequesterId { get; set; }
    public Guid AddresserId { get; set; }
    public FriendshipStatusType Status { get; set; }
    public DateTime Created { get; set; }
    public Account Requester { get; set; }
    public Account Addresser { get; set; }
}