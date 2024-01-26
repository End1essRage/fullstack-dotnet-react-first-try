using Microsoft.EntityFrameworkCore;
using TodosBackend.Data.Abstractions;
using TodosBackend.Models;

namespace TodosBackend.Data
{
    public class UserRepository : IUserRepository
    {

        private TodosDbContext _context;
        public UserRepository(TodosDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            var result = _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<User> FindById(int id)
        {
            return await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
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
            await _context.SaveChangesAsync();
        }
    }
}
