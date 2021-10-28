namespace stock_quote_alert
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            //Console.WriteLine(obj.Result.Metadata["SMTPClient"]);
            var emailSender = new EmailSender();
            string stockSymbol = "IBM";
            var stockListener = new StockListener(stockSymbol);
            stockListener.DisplayStockQuote();
            // EmailSender emailSender = new EmailSender();
            // emailSender.SendEmail().Wait();
        }
    }
}