using TodosBackend.Data.Abstractions;
using TodosBackend.Models;

namespace TodosBackend.Data
{
    public class FakeUserRepository : IUserRepository
    {
        private FakeDbContext _context;
        public FakeUserRepository(FakeDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            int newId = _context.Users.Count > 0 ? _context.Users.Max(x => x.Id) + 1 : 0;
            user.Id = newId;
            _context.Users.Add(user);

            return await Task.Run(() => user);
        }

        public async Task<User> FindByUserNameAsync(string userName)
        {
            return await Task.Run(() => _context.Users.FirstOrDefault(x => x.UserName == userName));
        }

        public async Task UpdateRefreshToken(int userId, RefreshToken refreshToken)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                user.RefreshToken = refreshToken.Token;
                user.TokenCreated = refreshToken.Created;
                user.TokenExpires = refreshToken.Expires;
            }
        }
        public async Task<User> FindByRefreshToken(string refreshToken)
        {
            return await Task.Run(() => _context.Users.FirstOrDefault(x => x.RefreshToken == refreshToken));
        }

    }
}
