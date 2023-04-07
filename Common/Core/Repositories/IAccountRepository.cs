using Core.Entities;

namespace Core.Repositories;

public interface IAccountRepository
{
    Task<Account> GetAsync(string login);
    Task<IEnumerable<Account>> SearchAsync(string name);
    Task<IEnumerable<Account>> SearchTestAsync();
    Task<Account> GetByIdAsync(Guid accountId);
    Task AddAsync(Account account);
    Task EditAsync(Account account);
}