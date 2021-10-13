namespace stock_quote_alert
{
    internal static class Program
    {
        private static void Main()
        {
            EmailSender emailSender = new EmailSender();
            var task = emailSender.SendGmail();
            task.Wait();
        }
    }
}