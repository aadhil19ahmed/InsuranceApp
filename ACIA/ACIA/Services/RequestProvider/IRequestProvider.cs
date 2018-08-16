using System.Threading.Tasks;
using ACIA.Model;

namespace ACIA.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri, string token = "");

        DynamicModel GetDynamicPage(string formID);

        Task<TResult> PostAsync<TResult>(string uri, string postData, string token = "");

        Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret);

        Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "", string header = "");

        Task DeleteAsync(string uri, string token = "");
    }
}