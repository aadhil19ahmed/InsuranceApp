using System;
using System.Net.Http;
using System.Threading.Tasks;
using ACIA.Model;
using ACIA.Services.RequestProvider;

namespace ACIA.Services.SaveAndContinueService
{
    public class SaveAndContinueService : ISaveAndContinueService
    {
        private readonly IRequestProvider _requestProvider;

        public SaveAndContinueService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ACIAQuoteModel> SaveAndContinueAsync(string postData, string uri)
        {
            try
            {
                var model = await _requestProvider.PostAsync<ACIAQuoteModel>(uri, postData);
                return model;
            }
            catch (HttpRequestException ex)
            {
                return new ACIAQuoteModel() { Status = ex.Message };
            }
        }


        public async Task<TResult> GetDataAsync<TResult>(string uri)
        {
            var model = await _requestProvider.GetAsync<TResult>(uri);
            return model;
        }

        public async Task<TResult> PostDataAsync<TResult>(string uri, string postData, string token = "")
        {
            var model = await _requestProvider.PostAsync<TResult>(uri, postData, token);
            return model;
        }
    }
}