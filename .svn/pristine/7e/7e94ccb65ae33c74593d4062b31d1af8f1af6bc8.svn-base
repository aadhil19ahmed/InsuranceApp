using System;
using System.Net.Http;
using System.Threading.Tasks;
using ACIA.Model;
using ACIA.Services.RequestProvider;

namespace ACIA.Services.GetUserId
{
    public class GetUserIdService : IGetUserIdService
    {
        private readonly IRequestProvider _requestProvider;

        public GetUserIdService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<UserIdModel> GetUserIdAsync(string postData, string uri)
        {
            try
            {
                var model = await _requestProvider.PostAsync<UserIdModel>(uri, postData);
                return model;
            }
            catch (HttpRequestException ex)
            {
                return new UserIdModel() { Status = ex.Message };
            }
        }
    }
}
