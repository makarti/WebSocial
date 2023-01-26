using Core.Entities;
using Core.Repositories;
using Core.Utils;
using Dapper;

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

            using(var connection = DBUtils.GetDBConnection())
            {
                var account = await connection.QuerySingleOrDefaultAsync<Account>(sql, new {login});
                return account;
            }
        }

        public async Task<ICollection<Account>> GetAllAsync()
        {
            const string sql = @"select * from Account;";

            using(var connection = DBUtils.GetDBConnection())
            {
                var account = await connection.QueryAsync<Account>(sql);
                return account.ToArray();
            }
        }

        public async Task AddAsync(Account account)
        {
            const string sql =
                @"insert into Account (Id, Login, Password, FirstName, LastName, Age, Gender, Interests, City, CreateDate) 
                  values (@Id, @Login, @Password, @FirstName, @LastName, @Age, @Gender, @Interests, @City, @CreateDate);";

            using(var connection = DBUtils.GetDBConnection())
            {
                await connection.ExecuteAsync(sql, account);
            }
        }
    }
}