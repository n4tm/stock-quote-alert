using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace stock_quote_alert
{
    public class EmailSender
    {
        private MailMessage _email;
        private string _host = string.Empty;
        private readonly string _subject = "Stock Quote Alert!";
        private readonly string _message = "You may check out this stock: (content)";

        public EmailSender()
        {
            TryToCreateEmail();
        }

        private void TryToCreateEmail()
        {
            Authenticator.ReadUserCredentials();
            try
            {
                _email = new MailMessage(Authenticator.EmailCredentials.UserName, Authenticator.EmailCredentials.UserName, _subject, _message);
                _host = _email.To.ToString().Contains("@outlook") || _email.To.ToString().Contains("@hotmail") ? "smtp-mail.outlook.com" : "smtp.gmail.com";
            }
            catch (FormatException)
            {
                Console.WriteLine("\nThe email address or password is incorrect. Please try again.");
                TryToCreateEmail();
            }
        }
        
        public async Task SendEmail()
        {
            using var smtp = new SmtpClient
            {
                Host = _host,
                EnableSsl = true,
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = Authenticator.EmailCredentials
            };
            try
            {
                Console.WriteLine("\nConnecting...");
                await Task.Run(() => smtp.SendMailAsync(_email));
                Console.WriteLine("Email successfully sent.");
            }
            catch (SmtpException)
            {
                Console.WriteLine("Your credentials are incorrect. Please, enter your credentials again.");
                TryToCreateEmail();
                await Task.Run(SendEmail);
            }
        }
    }
}