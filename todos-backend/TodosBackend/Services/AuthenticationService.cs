using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodosBackend.CommunicationModes;
using TodosBackend.Models;
using TodosBackend.Services.Abstractions;

namespace TodosBackend.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private IUserService _userService;
        public IConfiguration _configuration;

        public AuthenticationService(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<TokenResponse> CreateAccessTokenAsync(string userName, string password)
        {
            var user = await _userService.FindByUserNameAsync(userName);

            if (user == null)
            {
                return new TokenResponse(false, "User not found", "");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return new TokenResponse(false, "Password invalid", "");
            }

            return new TokenResponse(true, "Ok", CreateToken(user));
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
