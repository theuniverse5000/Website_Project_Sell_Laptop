using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Shop_Models.Dto;

namespace Shop_API.Utilities
{
    public class SendEmail
    {
        private readonly IConfiguration _config;

        public SendEmail(IConfiguration config)
        {
            _config = config;
        }
        public bool SendEmailTo(EmailDto request)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

                using var smtp = new SmtpClient();
                smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
