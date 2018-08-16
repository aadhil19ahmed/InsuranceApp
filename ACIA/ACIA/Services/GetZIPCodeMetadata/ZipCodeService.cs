using System;
using System.Net.Http;
using System.Threading.Tasks;
using ACIA.Model;
using ACIA.Services.RequestProvider;

namespace ACIA.Services.GetZIPCodeMetadata
{
    public class ZipCodeService : IZIPCodeService
    {
        private readonly IRequestProvider _requestProvider;

        public ZipCodeService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<GeoModel> GetZipCodeMetaDataAsync(string code)
        {
            string uri  = string.Format("{0}/maps/api/geocode/json?address={1}&key={2}",GlobalSetting.Instance.ZipCodeEndpoint, code, GlobalSetting.Instance.GoogleAPIKey);
            try{
                var model = await _requestProvider.GetAsync<GeoModel>(uri);
                return model;
            }catch(HttpRequestException ex){
                return new GeoModel() { status = ex.Message };
            }
        }
    }
}
