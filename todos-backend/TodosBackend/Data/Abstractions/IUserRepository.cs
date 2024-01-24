using TodosBackend.Models;

namespace TodosBackend.Data.Abstractions
{
    public interface IUserRepository
    {
        Task<User> FindByUserNameAsync(string userName);
        Task<User> CreateUserAsync(User user);
        Task UpdateRefreshToken(int userId, RefreshToken refreshToken);
        Task<User> FindByRefreshToken(string refreshToken);
    }
}
