using System;
using System.Threading.Tasks;
using stock_quote_alert.GoogleCloudStorage;
using ThreeFourteen.Finnhub.Client;
using ThreeFourteen.Finnhub.Client.Model;

namespace stock_quote_alert
{
    public class StockListener
    {
        public string StockSymbol { get; }
        public decimal CurrentQuotePrice { get; private set; }
        private readonly FinnhubClient _finnhubClient;

        public StockListener(string wantedStockSymbol)
        {
            string apiKey = CloudStorage.Instance.GetFromApiConfig("key");
            _finnhubClient = new FinnhubClient(apiKey);
            StockSymbol = wantedStockSymbol;
        }

        public void ListenToStock(int timeInterval=1000)
        {
            var quote = _finnhubClient.Stock.GetQuote(StockSymbol);
            quote.Wait();
            if (quote.Result.Current == CurrentQuotePrice) return;
            CurrentQuotePrice = quote.Result.Current;
            Console.WriteLine("{0:0.000}",quote.Result.Current);
        }

        public async Task<Company> GetChosenCompany()
        {
            return await _finnhubClient.Stock.GetCompany(StockSymbol);
        }
    }
}