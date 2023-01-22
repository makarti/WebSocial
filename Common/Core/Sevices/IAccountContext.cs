using Core.Entities;

namespace Core.Services;

public interface IAccountContext
{
    Account Account { get; set; }
}