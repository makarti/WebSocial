using Core.Entities;
using Core.Enum;
using Core.Repositories;
using Core.Utils;
using Dapper;
using Infrastructure.DAL;

namespace Infrastructure.Repositories;

public class FriendshipRepository : IFriendshipRepository
{
    private readonly DbContext _dbContext;
    public FriendshipRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Friendship>> GetsAsync(Guid accountId, FriendshipStatusType statusType)
    {           
        const string sql = @"select friendship.*, requester.*, addresser.*
                    from Friendship friendship
                    join Account requester on requester.Id = friendship.RequesterId
                    join Account addresser on addresser.Id = friendship.AddresserId
                    where friendship.Status = @status and
                    (friendship.RequesterId = @accountId or 
                    friendship.AddresserId = @accountId);"
        ;

        return await _dbContext.ExecuteQueryAsync(async connection =>
        {
            var friendships = await connection.QueryAsync<Friendship, Account, Account, Friendship>(sql,
                    (friendship, requester, addresser) =>
                    {
                        friendship.Requester = requester;
                        friendship.Addresser = addresser;

                        return friendship;
                    },
                    new { accountId, status = (byte)statusType },
                    splitOn: "Id");
            return friendships.ToArray();
        });
    }
    public async Task<Friendship> GetAsync(Guid reqeusterId, Guid addresserId)
    {           
        const string sql = @"select friendship.*, requester.*, addresser.*
                    from Friendship friendship
                    join Account requester on requester.Id = friendship.RequesterId
                    join Account addresser on addresser.Id = friendship.AddresserId
                    where friendship.RequesterId = @reqeusterId and 
                          friendship.AddresserId = @addresserId;";

        return await _dbContext.ExecuteQueryAsync(async connection =>
        {
            var friendships = await connection.QueryAsync<Friendship, Account, Account, Friendship>(sql,
                    (friendship, requester, addresser) =>
                    {
                        friendship.Requester = requester;
                        friendship.Addresser = addresser;

                        return friendship;
                    },
                    new { reqeusterId, addresserId },
                    splitOn: "Id");
            return friendships.FirstOrDefault();
        });
    }
    

    public async Task AddAsync(Guid requesterId, Guid addresserId)
    {
        const string sql =
            @"insert into Friendship (RequesterId, AddresserId, Created, Status) 
                values (@requesterId, @addresserId, @created, @status);";

        await _dbContext.ExecuteQueryAsync(async connection =>
        {
            await connection.ExecuteAsync(sql, new
            {
                requesterId,
                addresserId,
                created = DateTime.UtcNow,
                status = (byte)FriendshipStatusType.RequestSent
            });
        });
    }

    public async Task UpdateStatusAsync(Guid requesterId, Guid addresserId, FriendshipStatusType statusType)
    {        
        const string sql = @"update Friendship set 
            Status = @status
            where RequesterId = @requesterId and AddresserId = @addresserId;";

        await _dbContext.ExecuteQueryAsync(async connection =>
        {
            await connection.ExecuteAsync(sql, new { status = (byte)statusType, requesterId, addresserId });
        });
    }

    public async Task RemoveAsync(Guid requesterId, Guid addresserId)
    {        
        const string sql = @"delete from Friendship            
            where RequesterId = @requesterId and AddresserId = @addresserId LIMIT 1;";

        await _dbContext.ExecuteQueryAsync(async connection =>
        {
            await connection.ExecuteAsync(sql, new { requesterId, addresserId });
        });
    }
}