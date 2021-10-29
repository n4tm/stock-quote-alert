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
        private const string Subject = "Stock Quote Alert!";
        private readonly string _message = string.Empty;
        private readonly Dictionary<string, object> _smtpClient;

        public EmailSender()
        {
            _smtpClient = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(_cloudStorage.GetFromEmailConfig("SMTPClient"));
           _email = new MailMessage(_cloudStorage.GetFromEmailConfig("senderEmailAddress"),
               _cloudStorage.GetFromEmailConfig("recipientEmailAddress"), Subject, _message);
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
            decimal stockPrice = stockListener.CurrentQuotePrice;
            string emailRecipientName = _email.To.Count == 0 ? " "+_email.To[0].User : string.Empty;
            string stockSymbol = stockListener.StockSymbol;
            var stockCompany = await stockListener.GetChosenCompany();
            string companyName = stockCompany.Name;
            string companyCurrency = stockCompany.Currency;
            string transactionTypeString = transactionType.ToString().ToLower();
            _email.Body = $"Hello{emailRecipientName}, {stockSymbol} ({companyName}) price you were tracking is now costing {companyCurrency}{stockPrice}.\nDo not miss your chance to {transactionTypeString}!";
            await SendEmail();
        }
    }
}