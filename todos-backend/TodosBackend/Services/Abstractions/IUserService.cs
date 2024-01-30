using TodosBackend.Data.Models;
using TodosBackend.CommunicationModels.Tokens;

namespace TodosBackend.Web.Services.Abstractions
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);
        Task<User> FindByUserNameAsync(string userName);
        Task<User> GetCurrentUser();
        Task UpdateUserRefreshToken(User user, RefreshToken refreshToken);
        Task<User> FindUserByRefreshToken(string refreshToken);
        int GetCurrentUserId();
        Task ConfirmUser(int id);
    }
}
