using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Email;

namespace TimeManagementAPI.Handlers.Email
{
    public class EmailSenderHandler : IRequestHandler<EmailSenderCommand, bool>
    {
        private readonly IConfiguration _configuration;

        public EmailSenderHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<bool> Handle(EmailSenderCommand request, CancellationToken cancellationToken)
        {
            var to = new MailAddress(request.DestinyEmail);
            var from = new MailAddress(_configuration["EmailCredentials:Email"]);
            var message = new MailMessage(from, to)
            {
                Subject = request.Subject,
                Body = request.Body
            };
            var client = new SmtpClient(_configuration.GetValue<string>("EmailServer:SmtpServer"),
                                              _configuration.GetValue<int>("EmailServer:SmtpPort"))
            {
                Credentials = new NetworkCredential(_configuration["EmailCredentials:Email"],
                                                    _configuration["EmailCredentials:Password"]),
                EnableSsl = true
            };

            try
            {
                client.Send(message);
                return System.Threading.Tasks.Task.FromResult(true);
            }
            catch (SmtpException)
            {
                return System.Threading.Tasks.Task.FromResult(false);
            }
        }
    }
}
