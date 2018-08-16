using System;
using System.Net.Http;
using System.Threading.Tasks;
using ACIA.Model;
using ACIA.Services.RequestProvider;

namespace ACIA.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IRequestProvider _requestProvider;

        public IdentityService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<LoginModel> CreateAuthorizationRequestAsync(string postData, string uri)
        {
            try
            {
                var model = await _requestProvider.PostAsync<LoginModel>(uri, postData);
                return model;
            }
            catch (HttpRequestException ex)
            {
                return new LoginModel() { Status = ex.Message };
            }
        }

    }
}
