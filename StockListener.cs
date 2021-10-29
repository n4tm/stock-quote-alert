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

        public async Task ListenToStock()
        {
            var quote = await _finnhubClient.Stock.GetQuote(StockSymbol);
            if (quote.Current == CurrentQuotePrice) return;
            CurrentQuotePrice = quote.Current;
            Console.WriteLine("{0:0.000}",quote.Current);
        }

        public async Task<Company> GetChosenCompany()
        {
            return await _finnhubClient.Stock.GetCompany(StockSymbol);
        }
    }
}