using Core.Entities;

namespace Core.Services;

public interface IProfileService
{    
    Task EditAsync(Account account);
    Task<IEnumerable<Account>> SearchAsync(string name);
    Task<Account> GetByIdAsync(Guid accountId);
}