using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {

        public async Task<Account> GetAsync(string login)
        {
            const string sql =
                @"select *
                  from Account
                  where Login = @login;";

            return new Account();
        }

        public async Task<ICollection<Account>> GetAllAsync()
        {
            const string sql = @"select * from Account;";

            return await _dbContext.ExecuteQueryAsync(async connection =>
            {
                var account = await connection.QueryAsync<Account>(sql);

                return users.ToArray();
            });
        }

        public async Task AddAsync(Account account)
        {
            const string sql =
                @"insert into Account (Login, Email, Password, CreateDate) values (@login, @email, @password, @CreateDate);";

            await _dbContext.AddCommandAsync(connection => connection.ExecuteAsync(sql, user));
        }
    }
}