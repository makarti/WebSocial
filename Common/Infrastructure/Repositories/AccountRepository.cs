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
        public async Task EditAsync(Account account)
        {
            const string sql = @"update Account set 
                FirstName = @FirstName,
                LastName = @LastName,
                City = @City,
                Gender = @Gender,
                Age = @Age,
                Interests = @Interests
                where Login = @Login;";
                
            using(var connection = DBUtils.GetDBConnection())
            {
                await connection.ExecuteAsync(sql, account);
            }
        }

        public async Task<IEnumerable<Account>> SearchAsync(string name)
        {
            const string sql = @"select * from Account
                where FirstName like @name or LastName like @name;";
                
            using(var connection = DBUtils.GetDBConnection())
            {
                var accounts = await connection.QueryAsync<Account>(sql, new {name = "%" + name + "%"});
                return accounts.ToArray();
            }
        }

        public async Task<Account> GetByIdAsync(Guid accountId)
        {
            const string sql = @"select * from Account
                where Id = @accountId;";
                
            using(var connection = DBUtils.GetDBConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Account>(sql, new {accountId});
            }
        }
    }
}