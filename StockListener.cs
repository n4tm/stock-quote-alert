using System;
using stock_quote_alert.GoogleCloudStorage;
using ThreeFourteen.Finnhub.Client;

namespace stock_quote_alert
{
    public class StockListener
    {
        public decimal CurrentQuotePrice { get; private set; }
        private readonly FinnhubClient _finnhubClient;
        public StockListener()
        {
            string apiKey = CloudStorage.Instance.GetFromApiConfig("key");
            _finnhubClient = new FinnhubClient(apiKey);
        }

        public void ListenToStock(string wantedStockSymbol, int timeInterval=1000)
        {
            var quote = _finnhubClient.Stock.GetQuote(wantedStockSymbol);
            quote.Wait();
            if (quote.Result.Current == CurrentQuotePrice) return;
            CurrentQuotePrice = quote.Result.Current;
            Console.WriteLine("{0:0.000}",quote.Result.Current);
        }
    }
}