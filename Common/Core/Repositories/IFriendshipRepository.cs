namespace Core.Repositories;

public interface IFriendshipRepository
{
    Task Get();
    Task Add();
    Task Accept();
    Task Remove();
}