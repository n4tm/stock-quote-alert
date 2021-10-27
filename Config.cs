using System.IO;
using Microsoft.Extensions.Configuration;

namespace stock_quote_alert
{
    public static class Config
    {
        private static readonly IConfiguration Configuration;

        static Config()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
            Configuration = builder.Build();
        }

        public static string Get(string name)
        {
            return Configuration[name];
        }
    }
}