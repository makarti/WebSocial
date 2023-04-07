using Bogus.DataSets;
using Core.Entities;
using Core.Repositories;
using Core.Utils;
using Dapper;
using Infrastructure.DAL;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DbContext _dbContext;
        public AccountRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private static Random random = new Random();

        public async Task<Account> GetAsync(string login)
        {
            const string sql =
                @"select *
                  from Account
                  where Login = @login;";

            return await _dbContext.ExecuteQueryAsync(async connection =>
            {
                var account = await connection.QuerySingleOrDefaultAsync<Account>(sql, new { login });
                return account;
            });
        }

        public async Task AddAsync(Account account)
        {
            const string sql =
                @"insert into Account (Id, Login, Password, FirstName, LastName, Age, Gender, Interests, City, CreateDate) 
                  values (@Id, @Login, @Password, @FirstName, @LastName, @Age, @Gender, @Interests, @City, @CreateDate);";


            await _dbContext.ExecuteQueryAsync(async connection =>
            {
                await connection.ExecuteAsync(sql, account);
            });
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

            await _dbContext.ExecuteQueryAsync(async connection =>
            {
                await connection.ExecuteAsync(sql, account);
            });
        }

        public async Task<IEnumerable<Account>> SearchAsync(string name)
        {
            const string sql = @"select * from Account
                where FirstName like @name or LastName like @name limit 50;";

            return await _dbContext.ExecuteQueryAsync(async connection =>
            {
                var accounts = await connection.QueryAsync<Account>(sql, new { name = "%" + name + "%" });
                return accounts.ToArray();
            });
        }
        public async Task<IEnumerable<Account>> SearchTestAsync()
        {
            //const string chars = "abcdefghijklmnopqrstuvwxyz";
            //var name = new string(Enumerable.Repeat(chars, 3).Select(s => s[random.Next(s.Length)]).ToArray());
            var name = "art";
            const string sql = @"select * from Account
                where FirstName like @name or LastName like @name;";

            return await _dbContext.ExecuteQueryAsync(async connection =>
            {
                var accounts = await connection.QueryAsync<Account>(sql, new { name = name + "%" });
                return accounts.ToArray();
            });
        }

        public async Task<Account> GetByIdAsync(Guid accountId)
        {
            const string sql = @"select * from Account
                where Id = @accountId;";

            return await _dbContext.ExecuteQueryAsync(async connection =>
            {
                return await connection.QuerySingleOrDefaultAsync<Account>(sql, new { accountId });
            });
        }
    }
}