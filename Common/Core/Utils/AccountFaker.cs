using Bogus;
using Core.Entities;
using Core.Enum;

namespace Core.Utils
{
    internal class AccountFaker
    {
        Faker<Account>? _accountFaker = null;

        public Faker<Account> GetAccountGenerator()
        {
            if (_accountFaker == null)
            {
                _accountFaker = new Faker<Account>()
                    .RuleFor(a => a.Id, f => Guid.NewGuid())
                    .RuleFor(a => a.FirstName, (f, u) => f.Name.FirstName())
                    .RuleFor(a => a.LastName, (f, u) => f.Name.LastName())
                    .RuleFor(a => a.Login, (f, u) => f.Internet.Email(u.FirstName, u.LastName, uniqueSuffix: $"{f.UniqueIndex}"))
                    .RuleFor(a => a.Password, (f, u) => f.Internet.Password())
                    .RuleFor(a => a.Age, (f, u) => f.Random.Byte(18, 80))
                    .RuleFor(a => a.Gender, (f, u) => (GenderType)f.Random.Byte(1, 2))
                    .RuleFor(a => a.Interests, (f, u) => f.Lorem.Sentence())
                    .RuleFor(a => a.City, (f, u) => f.Address.City())
                    .RuleFor(a => a.CreateDate, (f, u) => f.Date.Past());
            }

            return _accountFaker;
        }
    }
}
