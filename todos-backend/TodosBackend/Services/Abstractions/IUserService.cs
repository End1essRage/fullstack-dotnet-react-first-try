using TodosBackend.Data.Models;
using TodosBackend.CommunicationModels.Tokens;

namespace TodosBackend.Web.Services.Abstractions
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);
        Task<User> FindByUserNameAsync(string userName);
        Task<User> GetCurrentUserAsync();
        Task UpdateUserRefreshTokenAsync(User user, RefreshToken refreshToken);
        Task<User> FindUserByRefreshTokenAsync(string refreshToken);
        int GetCurrentUserId();
        Task ConfirmUserAsync(int id);
    }
}
