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

        public Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            return Task.CompletedTask;
        }

        public async Task<User> FindByUserNameAsync(string userName)
        {
            return await Task.Run(() => _context.Users.FirstOrDefault(x => x.UserName == userName));
        }
    }
}
