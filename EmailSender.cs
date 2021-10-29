using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using stock_quote_alert.GoogleCloudStorage;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace stock_quote_alert
{
    public class EmailSender
    {
        private readonly MailMessage _email;
        private readonly CloudStorage _cloudStorage = CloudStorage.Instance;
        private readonly string _subject = string.Empty;
        private readonly string _message = string.Empty;
        private readonly Dictionary<string, object> _smtpClient;

        public EmailSender()
        {
            _smtpClient = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(_cloudStorage.GetFromEmailConfig("SMTPClient"));
           _email = new MailMessage(_cloudStorage.GetFromEmailConfig("senderEmailAddress"),
               _cloudStorage.GetFromEmailConfig("recipientEmailAddress"), _subject, _message)
           {
               IsBodyHtml = true,
               BodyEncoding = Encoding.UTF8
           };
        }

        private async Task SendEmail()
        {
            using var smtp = new SmtpClient
            {
                Host = _smtpClient["hostAddress"].ToString(),
                EnableSsl = Convert.ToBoolean(_smtpClient["EnableSsl"].ToString()),
                Port = Convert.ToInt32(_smtpClient["Port"].ToString()),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = Convert.ToBoolean(_smtpClient["UseDefaultCredentials"].ToString()),
                Credentials = new NetworkCredential(_cloudStorage.GetFromEmailConfig("senderEmailAddress"), 
                    _cloudStorage.GetFromEmailConfig("senderEmailPassword"))
            };
            try
            {
                Console.WriteLine("Sending email...");
                await Task.Run(() => smtp.SendMailAsync(_email));
                Console.WriteLine("Email successfully sent.");
            }
            catch (SmtpException)
            {
                Console.WriteLine("Your credentials are incorrect. Please, contact the database administrator to fix your credentials before running the program.");
            }
        }

        public async Task SendEmailWarningAsync(StockListener stockListener, TransactionType transactionType)
        {
            string stockSymbol = stockListener.StockSymbol;
            var stockCompany = stockListener.ChosenCompany;
            var stockQuote = stockListener.ChosenQuote;
            var stockPrice = stockQuote.Current;
            string companyName = stockCompany.Name;
            string companyCurrency = stockCompany.Currency;
            string transactionTypeString = transactionType.ToString().ToLower();
            _email.Subject = $"Stock Quote Alert - {stockSymbol} price tracking";
            _email.Body = $"Hello, the {stockSymbol} ({companyName}) price you were tracking is now costing {stockPrice} {companyCurrency}<br/><br/>Do not miss your chance to {transactionTypeString}!";
            await SendEmail();
        }
    }
}