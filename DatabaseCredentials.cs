namespace stock_quote_alert
{
    public struct DatabaseCredentials
    {
        private const string Username = "n4tm";
        private const string Password = "Natanael7";
        private const string ClusterName = "cluster0";
        public static string Database => "stock_quote_db";
        public static string Connection =>
            $"mongodb+srv://{Username}:{Password}@{ClusterName}.oxwrt.mongodb.net/{Database}?retryWrites=true&w=majority";

    }
}