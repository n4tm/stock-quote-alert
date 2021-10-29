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
        private readonly string _apiKey = CloudStorage.Instance.GetFromApiConfig("key");

        public StockListener(string wantedStockSymbol)
        {
            _finnhubClient = new FinnhubClient(_apiKey);
            StockSymbol = wantedStockSymbol;
        }

        public async Task ListenToStock()
        {
            try
            {
                var quote = await _finnhubClient.Stock.GetQuote(StockSymbol);
                var company = await GetChosenCompany();
                if (quote.Current == CurrentQuotePrice) return;
                CurrentQuotePrice = quote.Current;
                Console.WriteLine($"({DateTime.Now}) " + $"{StockSymbol}" + ": {0:0.0000} " + company.Currency,
                    quote.Current);
            }
            catch (FinnhubException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Please, check if the stock symbol (the first parameter) was correctly typed.");
                Console.WriteLine($"Possible results: https://finnhub.io/api/v1/search?q={StockSymbol}&token={_apiKey}");
                Environment.Exit(-1);
            }
        }

        public async Task<Company> GetChosenCompany()
        {
            return await _finnhubClient.Stock.GetCompany(StockSymbol);
        }
    }
}