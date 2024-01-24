using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
                return new TokenResponse(false, "User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return new TokenResponse(false, "Password invalid");
            }

            var refreshToken = CreateRefreshToken(user);
            WriteRefreshToken(user, refreshToken);

            return new TokenResponse(true, "Ok", CreateAcessToken(user), refreshToken);
        }

        public async Task<TokenResponse> CreateAccessTokenAsync(string refreshToken)
        {
            Console.WriteLine("refreshing " + refreshToken);
            var user = await _userService.FindUserByRefreshToken(refreshToken);

            if (user == null)
            {
                return new TokenResponse(false, "Token Invalid");
            }
            if (user.TokenExpires < DateTime.Now)
            {
                return new TokenResponse(false, "Token Expired");
            }

            var newRefreshToken = CreateRefreshToken(user);
            WriteRefreshToken(user, newRefreshToken);

            return new TokenResponse(true, "Ok", CreateAcessToken(user), newRefreshToken);
        }

        private string CreateAcessToken(User user)
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
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void WriteRefreshToken(User user, RefreshToken token)
        {
            _userService.UpdateUserRefreshToken(user, token);
            Console.WriteLine("writing " + token.Token);
        }

        private RefreshToken CreateRefreshToken(User user)
        {
            var token = new RefreshToken
            {
                Id = user.Id,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(10)
            };
            return token;
        }
    }
}
