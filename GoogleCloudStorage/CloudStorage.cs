using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Object = Google.Apis.Storage.v1.Data.Object;

namespace stock_quote_alert.GoogleCloudStorage
{
    public sealed class CloudStorage : ICloudStorage
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;

        #region Singleton
        private static CloudStorage _instance;
        private CloudStorage()
        {
            var googleCredential = GoogleCredential.FromFile(Config.Get("GoogleCredentialsFile"));
            _storageClient = StorageClient.Create(googleCredential);
            _bucketName = Config.Get("GoogleCloudStorageBucket");
        }

        public static CloudStorage Instance => _instance ??= new CloudStorage();

        #endregion

        public async Task<Object> GetFileAsync(string fileName)
        {
            return await _storageClient.GetObjectAsync(_bucketName, fileName);
        }
    }
}