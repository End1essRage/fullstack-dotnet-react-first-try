using TodosBackend.Data.Models;
using TodosBackend.CommunicationModels.Tokens;

namespace TodosBackend.Data.DataAccess.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByUserName(string userName);
        Task UpdateRefreshToken(User user, RefreshToken refreshToken);
        Task<User> FindByRefreshToken(string refreshToken);
    }
}
