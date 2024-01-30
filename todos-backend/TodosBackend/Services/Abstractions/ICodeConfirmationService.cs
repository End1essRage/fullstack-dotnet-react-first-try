using TodosBackend.Data.Models;

namespace TodosBackend.Web.Services.Abstractions
{
    public interface ICodeConfirmationService
    {
        Task CreateAccountConfirmationAsync(User user);
        Task<bool> TryConfirmAccountAsync(int userId, string code);
        Task CreateLogInConfirmationAsync(User user);
        Task<bool> TryConfirmLogInAsync(int userId, string code);
    }
}
