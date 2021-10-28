using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Object = Google.Apis.Storage.v1.Data.Object;

namespace stock_quote_alert.GoogleCloudStorage
{
    public sealed class CloudStorage
    {
        private readonly Task<Object> _emailConfigFile;
        private readonly Task<Object> _apiConfigFile;

        #region Singleton
        private static CloudStorage _instance;
        private CloudStorage()
        {
            var googleCredential = GoogleCredential.FromFile(Config.Get("GoogleCredentialsFile"));
            var storageClient = StorageClient.Create(googleCredential);
            var bucketName = Config.Get("GoogleCloudStorageBucket");
            _emailConfigFile = storageClient.GetObjectAsync(bucketName, GcsFiles.EmailConfigFile);
            _apiConfigFile = storageClient.GetObjectAsync(bucketName, GcsFiles.ApiConfigFile);
        }

        public static CloudStorage Instance => _instance ??= new CloudStorage();

        #endregion
        
        public string GetFromEmailConfig(string key)
        {
            return _emailConfigFile.Result.Metadata[key];
        }
        
        public string GetFromApiConfig(string key)
        {
            return _apiConfigFile.Result.Metadata[key];
        }
    }
}