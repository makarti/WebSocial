using Core.Entities;
namespace Core.Services;

public interface ISignInManager
{
    Task SignInAsync(Account account);
}