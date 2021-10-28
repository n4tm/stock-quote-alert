namespace stock_quote_alert
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            string stockSymbol = "IBM";
            var stockListener = new StockListener();
            while (true) stockListener.ListenToStock(stockSymbol).Wait();

            // EmailSender emailSender = new EmailSender();
            // emailSender.SendEmail().Wait();
        }
    }
}