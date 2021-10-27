using System.Threading.Tasks;
using Google.Apis.Storage.v1.Data;

namespace stock_quote_alert.GoogleCloudStorage
{
    public interface ICloudStorage
    {
        Task<Object> GetFileAsync(string fileName);
    }
}