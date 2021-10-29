using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace stock_quote_alert
{
    public static class Config
    {
        private static readonly IConfiguration Configuration;
        public static readonly string AssemblyDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        static Config()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AssemblyDirectory)
                .AddJsonFile("appsettings.json", true, true);
            Configuration = builder.Build();
        }

        public static string Get(string name)
        {
            return Configuration[name];
        }
    }
}