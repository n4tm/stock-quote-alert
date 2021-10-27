using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Stocks.Client;
using Newtonsoft.Json;
using stock_quote_alert.GoogleCloudStorage;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace stock_quote_alert
{
    public class StockListener
    {
        private readonly StocksClient _stocksClient;
        private readonly dynamic _jsonData;
        public StockListener()
        {
            var apiConfigMetadata = CloudStorage.Instance.GetFileAsync("apiConfig.json").Result.Metadata;
            _stocksClient = new AlphaVantageClient(apiConfigMetadata["key"]).Stocks();
            _jsonData = GetApiJsonData();
        }

        public async Task DisplayStockQuote(string wantedStockSymbol)
        {
            
            var searchMatches = await _stocksClient.SearchSymbolAsync(wantedStockSymbol);
            //Console.WriteLine(searchMatches.Count == 0 ? "Nothing found." : searchMatches.First().;
        }

        private static dynamic GetApiJsonData()
        {
            const string queryUrl = "https://api.polygon.io/v2/aggs/ticker/AAPL/range/1/minute/2020-10-14/2020-10-14" +
                                     "?adjusted=true&sort=asc&limit=120&apiKey=lJmaWwqcYQclVqSlrHmsQRRVJcrIbx1S";
            var queryUri = new Uri(queryUrl);
            using var client = new WebClient();
            return JsonSerializer.Deserialize<Dictionary<string, dynamic>>(client.DownloadString(queryUri));
        }
    }
}