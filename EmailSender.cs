using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using PanoramicData.ConsoleExtensions;

namespace stock_quote_alert
{
    public class EmailSender
    {
        private MailMessage _email;
        private readonly NetworkCredential _emailCredentials = new ();
        private readonly string _subject = "Stock Quote Alert!";
        private readonly string _message = "You may check out this stock: (content)";
        private string _host = string.Empty;

        public EmailSender()
        {
            TryToCreateEmail();
        }

        private void TryToCreateEmail()
        {
            ReadCredentials();
            try
            {
                _email = new MailMessage(_emailCredentials.UserName, _emailCredentials.UserName, _subject, _message);
                _host = _email.To.ToString().Contains("@outlook") || _email.To.ToString().Contains("@hotmail") ? "smtp-mail.outlook.com" : "smtp.gmail.com";
            }
            catch (FormatException)
            {
                Console.WriteLine("\nThe email address or password is incorrect. Please try again.");
                TryToCreateEmail();
            }
        }

        private void ReadCredentials()
        {
            Console.Write("email: ");
            _emailCredentials.UserName = Console.ReadLine() ?? string.Empty;
            Console.Write("password: ");
            _emailCredentials.Password = ConsolePlus.ReadPassword();
        }
        
        public async Task SendEmail()
        {
            using SmtpClient smtp = new SmtpClient
            {
                Host = _host,
                EnableSsl = true,
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = _emailCredentials
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