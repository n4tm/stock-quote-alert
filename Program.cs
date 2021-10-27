namespace stock_quote_alert
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            //Console.WriteLine(obj.Result.Metadata["SMTPClient"]);
            var emailSender = new EmailSender();
            string stockSymbol = "AAPL";
            var stockListener = new StockListener();
            stockListener.DisplayStockQuote(stockSymbol).Wait();
            // EmailSender emailSender = new EmailSender();
            // emailSender.SendEmail().Wait();
        }
    }
}