using TodosBackend.Models;

namespace TodosBackend.Data.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByUserName(string userName);
        Task UpdateRefreshToken(User user, RefreshToken refreshToken);
        Task<User> FindByRefreshToken(string refreshToken);
    }
}
