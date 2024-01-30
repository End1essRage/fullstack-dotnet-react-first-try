using TodosBackend.CommunicationModels.DTOs;

namespace TodosBackend.Web.Services.Abstractions
{
    public interface IEmailService
    {
        public Task SendEmailAsync(EmailMessageDto request);
    }
}
