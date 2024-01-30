using Microsoft.Extensions.Caching.Distributed;
using TodosBackend.Web.Services.Abstractions;
using TodosBackend.Web.Services.Models;

namespace TodosBackend.Web.Services
{
    public class CodeConfirmationService : ICodeConfirmationService
    {
        private IDistributedCache _cache;
        private const string AccountConfirmationPrefix = "ACC";
        public CodeConfirmationService(IDistributedCache cache) 
        {
            _cache = cache;
        }

        public async Task CreateAccountConfirmation(int userId)
        {
            var code = new ConfirmCode(AccountConfirmationPrefix);
            code.Key = userId.ToString();
            code.Code = CreateCode();

            await _cache.RemoveAsync(code.GetKey());
            await _cache.SetStringAsync(code.GetKey(), code.Code);

            Console.WriteLine("confirmation code is " + code.Code);
            //send message
        }
        public async Task<bool> TryConfirmAccount(int userId, string code)
        {
            var expected = await _cache.GetStringAsync(AccountConfirmationPrefix + userId.ToString());
            Console.WriteLine($"Expected code: " + expected);
            if (expected == null)
                return false;

            if (expected != code)
                return false;

            await _cache.RemoveAsync(AccountConfirmationPrefix + userId.ToString());
            return true;
        }

        public Task CreateLogInConfirmation(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryConfirmLogIn(int userId, string code)
        {
            throw new NotImplementedException();
        }

        private string CreateCode()
        {
            Random rnd = new Random();
            return rnd.Next(100000, 999999).ToString();
        }
    }
}
