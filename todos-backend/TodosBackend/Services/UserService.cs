using System.Security.Claims;
using TodosBackend.Data.Abstractions;
using TodosBackend.Models;
using TodosBackend.Services.Abstractions;

namespace TodosBackend.Services
{
    public class UserService: IUserService
    {
        private IUserRepository _repository;
        private IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateUserAsync(User user)
        {
            if (await _repository.FindByUserName(user.UserName) != null)
                return;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            await _repository.CreateUser(user);
        }

        public async Task UpdateUserRefreshToken(User user, RefreshToken refreshToken)
        {
            await _repository.UpdateRefreshToken(user, refreshToken);
        }

        public async Task<User> FindUserByRefreshToken(string refreshToken)
        {
            return await _repository.FindByRefreshToken(refreshToken);
        }

        public async Task<User> FindByUserNameAsync(string userName)
        {
            return await _repository.FindByUserName(userName);
        }

        public async Task<User> GetCurrentUser()
        {
            var userName = GetUserNameFromClaim();

            return await FindByUserNameAsync(userName);
        }

        private string GetUserNameFromClaim()
        {
            var result = String.Empty;

            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }

            return result;
        }
    }
}
