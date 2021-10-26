namespace stock_quote_alert
{
    internal static class Program
    {
        private static void Main()
        {
            var stockListener = new StockListener();
            stockListener.SearchForStock("AAPL").Wait();
            //EmailSender emailSender = new EmailSender();
            //emailSender.SendEmail().Wait();
        }
    }
}