using System;
using System.Threading.Tasks;
using ACIA.Model;

namespace ACIA.Services.SaveAndContinueService
{
    public interface ISaveAndContinueService
    {
        Task<ACIAQuoteModel> SaveAndContinueAsync(string postData, string uri);
        Task<TResult> GetDataAsync<TResult>(string uri);
        Task<TResult> PostDataAsync<TResult>(string uri, string postData, string token = "");
    }
}
