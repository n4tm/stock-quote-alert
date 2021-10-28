using System;
using stock_quote_alert.GoogleCloudStorage;


namespace stock_quote_alert
{
    public class StockListener
    {
        public StockListener(string wantedStockSymbol)
        {
            string apiKey = CloudStorage.Instance.GetFromApiConfig("key");
            //var client = new FinnhubClient(x);
            Console.WriteLine(apiKey);
        }

        public void DisplayStockQuote()
        {
            
        }
    }
}