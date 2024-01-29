using Microsoft.EntityFrameworkCore;
using TodosBackend.Data.DataAccess.Abstractions;
using TodosBackend.Data.Models;
using TodosBackend.CommunicationModels.Tokens;

namespace TodosBackend.Data.DataAccess
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        private TodosDbContext _context;
        public UserRepository(TodosDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> FindByRefreshToken(string refreshToken)
        {
            var user = await _context.Users.Where(u => u.RefreshToken == refreshToken).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> FindByUserName(string userName)
        {
            var user = await _context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();
            return user;
        }

        public async Task UpdateRefreshToken(User us, RefreshToken refreshToken)
        {
            var user = await FindByUserName(us.UserName);
            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expires;
            
            _context.Entry(user).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
