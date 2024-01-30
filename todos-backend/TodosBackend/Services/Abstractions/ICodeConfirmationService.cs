namespace TodosBackend.Web.Services.Abstractions
{
    public interface ICodeConfirmationService
    {
        Task CreateAccountConfirmation(int userId);
        Task<bool> TryConfirmAccount(int userId, string code);
        Task CreateLogInConfirmation(int userId);
        Task<bool> TryConfirmLogIn(int userId, string code);
    }
}
