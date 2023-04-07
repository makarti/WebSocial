using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Services;

public class ProfileService : IProfileService
{
    private readonly IAccountRepository _accountRep;
    
    public ProfileService(IAccountRepository accountRep)
    {
        _accountRep = accountRep;
    }
    
    public async Task EditAsync(Account account)
    {
        var existingAccount = await _accountRep.GetAsync(account.Login);
        if (existingAccount == null) throw new AccountIsNotFoundException($"account {account.Login} is not found");

        await _accountRep.EditAsync(account);
    }

    public Task<IEnumerable<Account>> SearchAsync(string name)
    {
        return _accountRep.SearchAsync(name);
    }

    public Task<Account> GetByIdAsync(Guid accountId)
    {
        return _accountRep.GetByIdAsync(accountId);
    }
}