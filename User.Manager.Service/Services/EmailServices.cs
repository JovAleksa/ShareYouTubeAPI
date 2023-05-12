using MailKit.Net.Smtp;
using MimeKit;
using User.Manager.Service.Models;

namespace User.Manager.Service.Services
{
    

    public class EmailServices : IEmailServices

    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailServices(EmailConfiguration emailConfiguration) =>_emailConfiguration = emailConfiguration;
        
    
        public void SendEmail(Message message)
        {
            var emailMessage = CreateNewMessage(message);
            Send(emailMessage);
        }

        private MimeMessage CreateNewMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject=message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Contet };
            return emailMessage;
         }
        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port,true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);    
                client.Send(mailMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
