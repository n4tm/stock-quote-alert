using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using stock_quote_alert.GoogleCloudStorage;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace stock_quote_alert
{
    public class EmailSender
    {
        private readonly MailMessage _email;
        private readonly CloudStorage _cloudStorage = CloudStorage.Instance;
        private readonly string _subject = "Stock Quote Alert!";
        private readonly string _message = "You may check out this stock: (content)";
        private readonly Dictionary<string, object> _smtpClient;

        public EmailSender()
        {
            _smtpClient = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(_cloudStorage.GetFromEmailConfig("SMTPClient"));
           _email = new MailMessage(_cloudStorage.GetFromEmailConfig("senderEmailAddress"),
               _cloudStorage.GetFromEmailConfig("recipientEmailAddress"), _subject, _message);
        }

        public async Task SendEmail()
        {
            using var smtp = new SmtpClient
            {
                Host = _smtpClient?["hostAddress"].ToString(),
                EnableSsl = Convert.ToBoolean(_smtpClient?["EnableSsl"]),
                Port = Convert.ToInt32(_smtpClient?["Port"]),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = Convert.ToBoolean(_smtpClient?["UseDefaultCredentials"]),
                Credentials = new NetworkCredential(_smtpClient?["senderEmailAddress"].ToString(), 
                    _smtpClient?["senderEmailPassword"].ToString())
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
                await Task.Run(SendEmail);
            }
        }
    }
}