using System;
using System.Threading.Tasks;

namespace stock_quote_alert
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            string stockSymbol = args[0];
            var stockListener = new StockListener(stockSymbol);
            var emailSender = new EmailSender();
            decimal sellPrice = Convert.ToDecimal(args[1]);
            decimal buyPrice = Convert.ToDecimal(args[2]);
            bool emailSent = false;
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q) && !emailSent)
            {
                await stockListener.ListenToStock();
                if (stockListener.CurrentQuotePrice >= sellPrice)
                {
                    await emailSender.SendEmailWarningAsync(stockListener, TransactionType.Sell);
                    emailSent = true;
                }
                if (stockListener.CurrentQuotePrice <= buyPrice)
                {
                    await emailSender.SendEmailWarningAsync(stockListener, TransactionType.Buy);
                    emailSent = true;
                }
            }
        }
    }
}