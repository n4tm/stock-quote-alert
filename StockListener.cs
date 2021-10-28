using System;
using System.Threading.Tasks;
using stock_quote_alert.GoogleCloudStorage;
using ThreeFourteen.Finnhub.Client;

namespace stock_quote_alert
{
    public class StockListener
    {
        private readonly FinnhubClient _finnhubClient;
        public StockListener()
        {
            string apiKey = CloudStorage.Instance.GetFromApiConfig("key");
            _finnhubClient = new FinnhubClient(apiKey);
        }

        public async Task ListenToStock(string wantedStockSymbol)
        {
            var quote = _finnhubClient.Stock.GetQuote(wantedStockSymbol);
            quote.Wait();
            await Task.Delay(1000);
            Console.WriteLine(quote.Result.Current);
        }
    }
}