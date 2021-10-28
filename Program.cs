using System;

namespace stock_quote_alert
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            string stockSymbol = "IBM";
            decimal sellPrice = Convert.ToDecimal(args[1]);
            decimal buyPrice = Convert.ToDecimal(args[2]);
            var stockListener = new StockListener();
            while (true)
            {
                stockListener.ListenToStock(stockSymbol);
                //if (stockListener.CurrentQuotePrice >= sellPrice) Send email warning selling
                //if (stockListener.CurrentQuotePrice <= buyPrice) Send email warning buying
            }
            
            // EmailSender emailSender = new EmailSender();
            // emailSender.SendEmail().Wait();
        }
    }
}