using TodosBackend.Models;

namespace TodosBackend.Data.Abstractions
{
    public interface IUserRepository
    {
        Task<User> FindByUserNameAsync(string userName);
        Task CreateUserAsync(User user);
    }
}
