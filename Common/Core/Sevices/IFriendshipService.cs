namespace Core.Services;
public interface IFriendshipService
{
    Task Get();
    Task Add();
    Task Accept();
    Task Remove();    
}