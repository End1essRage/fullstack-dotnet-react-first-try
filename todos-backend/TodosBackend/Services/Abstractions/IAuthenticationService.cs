using TodosBackend.CommunicationModels.Tokens;

namespace TodosBackend.Web.Services.Abstractions
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> CreateAccessTokenAsync(string userName, string password);
        Task<TokenResponse> CreateAccessTokenAsync(string refreshToken);
    }
}
