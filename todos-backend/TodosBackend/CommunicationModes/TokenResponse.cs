using TodosBackend.Models;

namespace TodosBackend.CommunicationModes
{
    public class TokenResponse: BaseResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public RefreshToken? RefreshToken { get; set; }

        public TokenResponse(bool success, string message) : base(success, message) { }

        public TokenResponse(bool success, string message, string accessToken, RefreshToken refreshToken) : base(success, message)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
