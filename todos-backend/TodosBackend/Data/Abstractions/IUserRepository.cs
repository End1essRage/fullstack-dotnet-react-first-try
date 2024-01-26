using TodosBackend.Models;

namespace TodosBackend.Data.Abstractions
{
    public interface IUserRepository
    {
        Task<User> FindByUserName(string userName);
        Task<User> CreateUser(User user);
        Task UpdateRefreshToken(User user, RefreshToken refreshToken);
        Task<User> FindByRefreshToken(string refreshToken);
        Task<User> FindById(int id);
    }
}
