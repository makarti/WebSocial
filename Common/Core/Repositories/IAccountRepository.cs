using Core.Entities;

namespace Core.Repositories;

public interface IAccountRepository
{
    Task<Account> GetAsync(string login);
    Task<ICollection<Account>> GetAllAsync();
    Task AddAsync(Account account);
    Task EditAsync(Account account);
}