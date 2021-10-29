using System;

namespace stock_quote_alert
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            string stockSymbol = "IBM";
            var stockListener = new StockListener(stockSymbol);
            var emailSender = new EmailSender();
            emailSender.SendEmailWarningAsync(stockListener, TransactionType.Buy).Wait();
            /*
            //string stockSymbol = args[0];
            //decimal sellPrice = Convert.ToDecimal(args[1]);
            //decimal buyPrice = Convert.ToDecimal(args[2]);
            var stockListener = new StockListener();
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q))
            {
                stockListener.ListenToStock(stockSymbol);
                //if (stockListener.CurrentQuotePrice >= sellPrice) Send email warning selling
                //if (stockListener.CurrentQuotePrice <= buyPrice) Send email warning buying
            }
            */
        }
    }
}