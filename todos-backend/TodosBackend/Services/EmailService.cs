using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using TodosBackend.CommunicationModels.DTOs;
using TodosBackend.Web.Services.Abstractions;

namespace TodosBackend.Web.Services
{
    public class EmailService : IEmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(EmailMessageDto request)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "end1essrage@todos.ru"));
            emailMessage.To.Add(new MailboxAddress("", request.ReceiverEmail));
            emailMessage.Subject = request.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = request.Body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration.GetSection("SmtpSettings:Host").Value, 
                    Convert.ToInt32(_configuration.GetSection("SmtpSettings:Port").Value), false);

                await client.AuthenticateAsync(_configuration.GetSection("SmtpSettings:UserName").Value,
                    _configuration.GetSection("SmtpSettings:Password").Value);

                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
