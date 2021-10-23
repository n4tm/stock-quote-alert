using MongoDB.Driver;

namespace stock_quote_alert
{
    internal static class Program
    {
        private static IMongoDatabase _db = new MongoClient(DatabaseCredentials.Connection).GetDatabase(DatabaseCredentials.Database); 
        private static void Main()
        {
            
            //StockListener stockListener = new StockListener();
            //EmailSender emailSender = new EmailSender();
            //emailSender.SendEmail().Wait();
        }
    }
}