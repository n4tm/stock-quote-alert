using System;
using System.Net;
using PanoramicData.ConsoleExtensions;

namespace stock_quote_alert
{
    public static class Authenticator
    {
        public static NetworkCredential EmailCredentials { get; } = new();
        public static string ApiKey { get; private set; }

        public static void ReadUserCredentials()
        {
            Console.Write("email: ");
            EmailCredentials.UserName = Console.ReadLine() ?? string.Empty;
            EmailCredentials.Password = ReadPassword("password: ");
        }

        public static void ReadApiKey()
        {
            ApiKey = ReadPassword("API key: ");
        }

        private static string ReadPassword(string outputTxt)
        {
            Console.Write(outputTxt);
            string passwd = ConsolePlus.ReadPassword();
            Console.Write('\n');
            return passwd;
        }
    }
}