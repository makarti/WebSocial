using Core.Entities;
using Core.Enum;
using Core.Repositories;
using Core.Utils;
using Dapper;

namespace Infrastructure.Repositories;

public class FriendshipRepository : IFriendshipRepository
{
    public async Task<IEnumerable<Friendship>> GetsAsync(Guid accountId, FriendshipStatusType statusType)
    {           
        const string sql = @"select friendship.*, requester.*, addresser.*
                    from Friendship friendship
                    join Account requester on requester.Id = friendship.RequesterId
                    join Account addresser on addresser.Id = friendship.AddresserId
                    where friendship.Status = @status and
                    (friendship.RequesterId = @accountId or 
                    friendship.AddresserId = @accountId);";
                
        using(var connection = DBUtils.GetDBConnection())
        {
            var friendships = await connection.QueryAsync<Friendship, Account, Account, Friendship>(sql,
                    (friendship, requester, addresser) =>
                    {
                        friendship.Requester = requester;
                        friendship.Addresser = addresser;

                        return friendship;
                    },
                    new { accountId, status =  (byte)statusType},
                    splitOn: "Id");
            return friendships.ToArray();
        }
    }
    public async Task<Friendship> GetAsync(Guid reqeusterId, Guid addressertId)
    {           
        const string sql = @"select friendship.*, requester.*, addresser.*
                    from Friendship
                    join Account requester on requester.Id = Friendship.RequesterId
                    join Account addresser on addresser.Id = Friendship.AddresserId
                    where friendship.RequesterId = @reqeusterId and 
                          friendship.AddresserId = @addressertId;";
                
        using(var connection = DBUtils.GetDBConnection())
        {
            var friendships = await connection.QueryAsync<Friendship, Account, Account, Friendship>(sql,
                    (friendship, requester, addresser) =>
                    {
                        friendship.Requester = requester;
                        friendship.Addresser = addresser;

                        return friendship;
                    },
                    new { reqeusterId, addressertId },
                    splitOn: "Id");
            return friendships.FirstOrDefault();
        }
    }
    

    public async Task AddAsync(Guid requesterId, Guid addresserId)
    {
        const string sql =
            @"insert into Friendship (RequesterId, AddresserId, Created, Status) 
                values (@requesterId, @addresserId, @created, @status);";

        using(var connection = DBUtils.GetDBConnection())
        {
            await connection.ExecuteAsync(sql, new 
            {
                requesterId, 
                addresserId, 
                created = DateTime.UtcNow,
                status = (byte)FriendshipStatusType.RequestSent
            });
        }
    }

    public async Task UpdateStatusAsync(Guid requesterId, Guid addresserId, FriendshipStatusType statusType)
    {        
        const string sql = @"update Friendship set 
            Status = @status
            where RequesterId = @requesterId and AddresserId = @addresserId;";
            
        using(var connection = DBUtils.GetDBConnection())
        {
            await connection.ExecuteAsync(sql, new { status = (byte)statusType, requesterId, addresserId });
        }
    }

    public async Task RemoveAsync(Guid requesterId, Guid addresserId)
    {        
        const string sql = @"delete from Friendship            
            where RequesterId = @requesterId and AddresserId = @addresserId LIMIT 1;";
            
        using(var connection = DBUtils.GetDBConnection())
        {
            await connection.ExecuteAsync(sql, new { requesterId, addresserId });
        }
    }
}