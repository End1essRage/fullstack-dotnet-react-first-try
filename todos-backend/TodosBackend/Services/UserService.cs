using System.Security.Claims;
using TodosBackend.Data.DataAccess.Abstractions;
using TodosBackend.Data.Models;
using TodosBackend.Web.Services.Abstractions;
using TodosBackend.CommunicationModels.Tokens;

namespace TodosBackend.Web.Services
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
            _repository.Create(user);
            await _repository.SaveChangesAsync();
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
            int userId = GetUserIdFromClaim();
            if(userId >= 0)
            {
                return await _repository.GetOneAsync(userId);
            }

            return null;
        }
        public int GetCurrentUserId()
        {
            return GetUserIdFromClaim();
        }

        private int GetUserIdFromClaim()
        {
            //TODO rework
            int result = -1;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue("user_id"));
            }
            return result;
        }

        public async Task ConfirmUser(int id)
        {
            var user = await _repository.GetOneAsync(id);
            if(user != null)
            {
                user.Confirmed = true;
            }
            _repository.Update(user);
            await _repository.SaveChangesAsync();
        }
    }
}
