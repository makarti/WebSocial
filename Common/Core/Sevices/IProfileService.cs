using Core.Entities;

namespace Core.Services;

public interface IProfileService
{    
    Task EditAsync(Account account);
}