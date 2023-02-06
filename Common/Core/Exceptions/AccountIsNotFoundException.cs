namespace Core.Exceptions;

public class AccountIsNotFoundException : Exception
{
    public AccountIsNotFoundException(string message) : base(message)
    {
    }
}