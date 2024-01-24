using TodosBackend.Models;

namespace TodosBackend.Services.Abstractions
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);
        Task<User> FindByUserNameAsync(string userName);
        Task<User> GetCurrentUser();
        Task UpdateUserRefreshToken(User user, RefreshToken refreshToken);
        Task<User> FindUserByRefreshToken(string refreshToken);
    }
}
