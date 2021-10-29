using System;
using ThreeFourteen.Finnhub.Client;

namespace stock_quote_alert
{
    public class StockSymbolException : FinnhubException
    {
        public StockSymbolException(int statusCode, string reasonPhrase) : base(statusCode, reasonPhrase)
        {
            Console.WriteLine(reasonPhrase);
            Console.WriteLine("Please, check if the stock symbol (the first parameter) was correctly typed.");
        }
    }
}