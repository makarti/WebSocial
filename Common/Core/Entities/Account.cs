using Core.Enum;

namespace Core.Entities;

public class Account
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte Age { get; set; }
    public GenderType Gender { get; set; }
    public string Interests { get; set; }
    public string City { get; set; }

    public DateTime CreateDate { get; set; }
}

