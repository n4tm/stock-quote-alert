using System;
using System.Linq;
using System.Threading.Tasks;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Stocks.Client;

namespace stock_quote_alert
{
    public class StockListener
    {
        //private string _selectedStockName;
        private readonly StocksClient _stocksClient;
        
        public StockListener()
        {
            Authenticator.ReadApiKey();
            _stocksClient = new AlphaVantageClient(Authenticator.ApiKey).Stocks();
        }

        public async Task SearchForStock(string wantedStockSymbol)
        {
            var searchMatches = await _stocksClient.SearchSymbolAsync(wantedStockSymbol);
            Console.WriteLine(searchMatches.Count == 0 ? "Nothing found." : searchMatches.First().Name);
        }
    }
}