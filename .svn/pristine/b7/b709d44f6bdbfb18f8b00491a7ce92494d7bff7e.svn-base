using System;
namespace ACIA
{
    public class GlobalSetting
    {
        public const string DefaultEndpoint = "https://dpcmlab.aspiresys.com";
        //public const string DefaultEndpoint = "http://www.mocky.io/v2/5a5748e42e00001d2512009e";
        public const string GeoCodingEndpoint = "https://maps.googleapis.com";
        private string _baseEndpoint;
        private string _geoCodeBaseEndpoint;
        private static readonly GlobalSetting _instance = new GlobalSetting();

        public GlobalSetting()
        {
            AuthToken = "INSERT AUTHENTICATION TOKEN";
            GoogleAPIKey = "AIzaSyB1Xrqv4E7lFVBNCQ07qgdk_FXuljOxPDM";
            BaseEndpoint = DefaultEndpoint;
            GeoCodeBaseEndpoint = GeoCodingEndpoint;
        }

        public static GlobalSetting Instance
        {
            get { return _instance; }
        }

        public string BaseEndpoint
        {
            get { return _baseEndpoint; }
            set
            {
                _baseEndpoint = value;
                UpdateEndpoint(_baseEndpoint);
            }
        }

        public string GeoCodeBaseEndpoint
        {
            get { return _geoCodeBaseEndpoint; }
            set
            {
                _geoCodeBaseEndpoint = value;
                UpdateGeoEndpoint(_geoCodeBaseEndpoint);
            }
        }

        public string AuthToken { get; set; }
        public string GoogleAPIKey { get; set; }

        public string ZipCodeEndpoint { get; set; }
        public string SaveCarDetailsEndPoint { get; set; }
        public string SaveCarDriverDetailsEndPoint { get; set; }
        public string SaveCarMakeModelDetailsEndPoint { get; set; }
        public string SaveCarVINModelDetailsEndPoint { get; set; }
        public string SaveCarAdditionalInformationEndPoint { get; set; }
        public string SaveDriverClaimsEndPoint { get; set; }
        public string SaveDriverConvictionDetailsEndPoint { get; set; }
        public string GetGeneratedQuoteId { get; set; }
        public string GetCarSuggestedQuotes { get; set; }

        public string GetCarAdditionalInformation { get; set; }
        public string GetCarDetailsEndPoint { get; set; }
        public string GetCarDriverDetailsEndPoint { get; set; }
        public string GetCarMakeModelDetailsEndPoint { get; set; }
        public string GetCarVINModelDetailsEndPoint { get; set; }
        public string GetCarClaimDetailsEndPoint { get; set; }
        public string GetCarConvictionDetailsEndPoint { get; set; }
        public string AuthorizationEndPoint { get; set; }
        public string GetQuoteIdListEndPoint { get; set; }
        public string GetUserIdEndPoint { get; set; }
        public string SendMailEndPoint { get; set; }

        private void UpdateEndpoint(string baseEndpoint)
        {
            //SaveCarDetailsEndPoint = baseEndpoint;
            SaveCarDetailsEndPoint = $"{baseEndpoint}:4434/EpiInsuranceService.svc/SaveCarPersonalDetails";
            SaveCarDriverDetailsEndPoint = $"{baseEndpoint}:4434/EpiInsuranceService.svc/SaveCarDriverDetails";
            SaveCarMakeModelDetailsEndPoint = $"{baseEndpoint}:4434/EpiInsuranceService.svc/SaveCarMakeModelDetails";
            SaveCarVINModelDetailsEndPoint = $"{baseEndpoint}:4434/EpiInsuranceService.svc/SaveCarVINModelDetails";
            SaveCarAdditionalInformationEndPoint = $"{baseEndpoint}:4434/EpiInsuranceService.svc/SaveCarAdditionalInformation";
            SaveDriverConvictionDetailsEndPoint = $"{baseEndpoint}:4434/EpiInsuranceService.svc/SaveCarConvictionDetails";
            SaveDriverClaimsEndPoint = $"{baseEndpoint}:4434/EpiInsuranceService.svc/SaveCarDriverClaimDetails";
            GetGeneratedQuoteId = $"{baseEndpoint}:4434/EpiInsuranceService.svc/GetNewQuoteId";
            GetCarSuggestedQuotes = $"{baseEndpoint}:4434/EpiInsuranceService.svc/GetCarSuggestedQuotes";
            GetCarDetailsEndPoint = $"{baseEndpoint}:4434/EpiInsuranceService.svc/GetCarPersonalDetails?QuoteId=";
            GetCarAdditionalInformation = $"{baseEndpoint}:4434/EpiInsuranceService.svc/GetCarAdditionalInformation?QuoteId=";
            GetCarMakeModelDetailsEndPoint = $"{baseEndpoint}:4434/EpiInsuranceService.svc/GetCarMakeModelDetails?QuoteId=";
            GetCarVINModelDetailsEndPoint=$"{baseEndpoint}:4434/EpiInsuranceService.svc/GetCarVINModelDetails?QuoteId=";
            GetCarDriverDetailsEndPoint=$"{baseEndpoint}:4434/EpiInsuranceService.svc/GetCarDriverDetails?QuoteId=";
            GetCarClaimDetailsEndPoint = $"{baseEndpoint}:4434/EpiInsuranceService.svc/GetCarDriverClaimDetails?QuoteId=";
            GetCarConvictionDetailsEndPoint = $"{baseEndpoint}:4434/EpiInsuranceService.svc/GetCarDriverConvictionDetails?QuoteId=";
            AuthorizationEndPoint = $"{baseEndpoint}:4434/EpiInsuranceService.svc/AuthenticateUsers";
            GetQuoteIdListEndPoint = $"{baseEndpoint}:4434/EpiInsuranceService.svc/GetForms?UserId=";
            GetUserIdEndPoint = $"{baseEndpoint}:4434/EpiInsuranceService.svc/SendUserId";
            SendMailEndPoint = $"{baseEndpoint}:4434/EpiInsuranceService.svc/SendEmailId";
        }

        private void UpdateGeoEndpoint(string geoCodingEndPoint)
        {
            ZipCodeEndpoint = $"{geoCodingEndPoint}";
        }
    }
}