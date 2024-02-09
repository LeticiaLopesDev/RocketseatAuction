using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;

namespace RocketseatAuction.API.Repositories.DataAccess;

public class UserRepository : IUserRepository
{
    private readonly RocketseatAuctionDbContext _dbContext;
    public UserRepository(RocketseatAuctionDbContext dbContext) => _dbContext = dbContext;
    public bool ExistUserWithEmail(string email)
    {
        return _dbContext.Users.Any(u => u.Email.Equals(email));
    }
    public User GetUserByEmail(string email) => _dbContext.Users.First(u => u.Email.Equals(email));
}
