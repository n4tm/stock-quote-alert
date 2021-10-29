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
        public Company ChosenCompany { get; }
        public Quote ChosenQuote { get; }
        private readonly FinnhubClient _finnhubClient;
        private readonly string _apiKey = CloudStorage.Instance.GetFromApiConfig("key");
        private decimal _currentQuotePrice;

        public StockListener(string wantedStockSymbol)
        {
            _finnhubClient = new FinnhubClient(_apiKey);
            StockSymbol = wantedStockSymbol;
            ChosenQuote = GetChosenQuote().Result;
            ChosenCompany = GetChosenCompany().Result;
        }

        public async Task ListenToStock()
        {
            try
            {
                if (ChosenQuote.Current == 0)
                    throw new StockSymbolException(-1, $"Unable to find {StockSymbol} data from API.");
                //if (quote.Current == CurrentQuotePrice) return;
                _currentQuotePrice = ChosenQuote.Current;
                WriteColorful($"({DateTime.Now}) ", ConsoleColor.DarkCyan);
                Console.Write($"{StockSymbol}: ");
                WriteColorful($"{_currentQuotePrice:0.0000} ", ConsoleColor.Green);
                Console.WriteLine(ChosenCompany.Currency);
                await Task.Delay(3000);
            }
            catch (StockSymbolException)
            {
                Console.WriteLine($"Possible results: https://finnhub.io/api/v1/search?q={StockSymbol}&token={_apiKey}");
                Console.WriteLine("Aborted\n");
                Environment.Exit(-1);
            }
        }

        private async Task<Company> GetChosenCompany()
        {
            return await _finnhubClient.Stock.GetCompany(StockSymbol);
        }

        private async Task<Quote> GetChosenQuote()
        {
            return await _finnhubClient.Stock.GetQuote(StockSymbol);
        }

        private void WriteColorful(string text, ConsoleColor color, bool lineBreak=false)
        {
            Console.ForegroundColor = color;
            if (lineBreak) Console.WriteLine(text);
            else Console.Write(text);
            Console.ResetColor();
        }
    }
}