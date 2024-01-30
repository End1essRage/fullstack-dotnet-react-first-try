using Microsoft.Extensions.Caching.Distributed;
using TodosBackend.CommunicationModels.DTOs;
using TodosBackend.Data.Models;
using TodosBackend.Web.Services.Abstractions;
using TodosBackend.Web.Services.Models;

namespace TodosBackend.Web.Services
{
    public class CodeConfirmationService : ICodeConfirmationService
    {
        private IDistributedCache _cache;
        private IEmailService _emailService;
        private const string AccountConfirmationPrefix = "ACC";
        public CodeConfirmationService(IDistributedCache cache, IEmailService emailService) 
        {
            _cache = cache;
            _emailService = emailService;
        }

        public async Task CreateAccountConfirmationAsync(User user)
        {
            var code = new ConfirmCode(AccountConfirmationPrefix);
            code.Key = user.Id.ToString();
            code.Code = CreateCode();

            await _cache.RemoveAsync(code.GetKey());
            await _cache.SetStringAsync(code.GetKey(), code.Code);

            var mailRequest = new EmailMessageDto()
            {
                ReceiverEmail = user.Email,
                Subject = "Код подтверждения",
                Body = code.Code
            };

            await _emailService.SendEmailAsync(mailRequest);
        }
        public async Task<bool> TryConfirmAccountAsync(int userId, string code)
        {
            var expected = await _cache.GetStringAsync(AccountConfirmationPrefix + userId.ToString());
            
            if (expected == null)
                return false;

            if (expected != code)
                return false;

            await _cache.RemoveAsync(AccountConfirmationPrefix + userId.ToString());
            return true;
        }

        public Task CreateLogInConfirmationAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryConfirmLogInAsync(int userId, string code)
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
