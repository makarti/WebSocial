using Core.Entities;
using Core.Services;

namespace Infrastructure.Services;

public class AccountContext : IAccountContext
{
    public Account Account { get; set; }
}