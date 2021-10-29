using System;
using System.Threading.Tasks;

namespace stock_quote_alert
{
    internal static class Program
    {
        private static readonly EmailSender EmailSender = new();
        private static StockListener _stockListener;
        private static bool _emailSent;
        private static async Task Main(string[] args)
        {
            string stockSymbol = args[0];
            _stockListener = new StockListener(stockSymbol);
            decimal sellPrice = Convert.ToDecimal(args[1]);
            decimal buyPrice = Convert.ToDecimal(args[2]);
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q) && !_emailSent)
            {
                await _stockListener.ListenToStock();
                var currentPrice = _stockListener.ChosenQuote.Current;
                if (currentPrice >= sellPrice) await SendEmailOfType(TransactionType.Sell);
                if (currentPrice <= buyPrice) await SendEmailOfType(TransactionType.Buy);
            }
        }

        private static async Task SendEmailOfType(TransactionType transactionType)
        {
            await EmailSender.SendEmailWarningAsync(_stockListener, transactionType);
            _emailSent = true;
        }
    }
}