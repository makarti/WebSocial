using Core.Entities;

namespace Core.Services;

public interface IAuthenticationService
{
    Task RegisterAsync(Account account, string password);
    Task<Account> LoginAsync(string login, string password);
}