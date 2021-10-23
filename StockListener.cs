using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Stocks;
using AlphaVantage.Net.Stocks.Client;

namespace stock_quote_alert
{
    public class StockListener
    {
        //private string _selectedStockName;
        private readonly StocksClient _stocksClient;

        public StockListener(string stocksApiKey)
        {
            _stocksClient = new AlphaVantageClient(stocksApiKey).Stocks();
        }

        public async Task SearchForStock(string wantedStockSymbol)
        {
            ICollection<SymbolSearchMatch> searchMatches = await _stocksClient.SearchSymbolAsync(wantedStockSymbol);
            foreach (var sm in searchMatches)
            {
                Console.WriteLine(sm.Name);
            }
        }

    }
}