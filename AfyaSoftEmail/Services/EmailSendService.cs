using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfyaSoftEmail.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace AfyaSoftEmail.Services
{
    public class EmailSendService
    {
        private readonly string _email;
        private readonly string _password;

        public EmailSendService(IConfiguration _configuration)
        {
            _email = _configuration.GetSection("EmailService:Email").Get<string>();
            _password = _configuration.GetSection("EmailService:Password").Get<string>();
        }

        public async Task SendEmailAsync(UserMessage userMessage, string message)
        {
            MimeMessage mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("The Social Light", _email));

            mailMessage.To.Add(new MailboxAddress(userMessage.Name, userMessage.Email));

            mailMessage.Subject = "Welcome to The Social Light";

            var body  = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message.ToString()
            };

            mailMessage.Body = body;

            var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, false);

            await client.AuthenticateAsync(_email, _password);
            await client.SendAsync(mailMessage);
            await client.DisconnectAsync(true);
        }
    }
}