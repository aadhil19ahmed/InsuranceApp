using System;

using System.Collections.Generic;
using System.Linq;
using ACIA.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ACIA.Helper
{
    public static class ServicesHelper
    {
        private static List<string> saveCarPersonalDetailsKeys = new List<string>{
            "QuoteId",
            "Title",
            "FirstName",
            "LastName",
            "MiddleInitial",
            "Suffix",
            "Gender",
            "DateOfBirth",
            "MaritalStatus",
            "MobilePhone",
             "EmailId",
            "ZipCode",
            "City",
            "State",
            "Address",
            "Citizenship",
            "CitizenByBirth",
            "CitizenshipStartDate",
            "OccupationType",
            "Industry",
            "JobFunctions",
            "IsNamedDriver",
            "LicenseType",
            "LicenseDuration",
            "LicenseNo",
            "IsHaveAccidentsOrClaims",
            "IsHaveConviction",
            "IsHaveNominee",
            "NomineeTitle",
            "NomineeFirstName",
            "NomineeLastName",
            "NomineeDOB",
            "NomineeGender",
            "NomineeRelationship",
            "AppointeeName",
            "IsHomeOwner",
            "IsHaveChild",
            "NoOfCars"
        };

        private static List<string> saveCarAdditionalInformationKeys = new List<string>
        {
            "AddBreakdownCoverage",
            "AddCNGFitment",
            "AddElectricalAccessories",
            "AddNamedPassenger",
            "AddNonElectricalAccessories",
            "AddPaidDriverCover",
            "AddPersonalAccidentCover",
            "AddUnNamedPassenger",
            "AdditionalInformationId",
            "AnotherVechile",
            "CarUsage",
            "CoverStartDate",
            "CoverType",
            "DayTimeParking",
            "ExcessPay",
            "ExistingCoverType",
            "HasExistingPolicyUs",
            "HasNCB",
            "HasProtectedNCB",
            "Mileage",
            "NCBDuration",
            "NeedToProtectNCB",
            "NightTimeParking",
            "NumberOfDrivers",
            "NumberOfNamedPassenger",
            "NumberofUnNamedPassenger",
            "PaymentMethod",
            "PolicyEndDate",
            "PolicyNumber",
            "PolicyStartDate",
            "QuoteId",
            "QuoteType",
            "SumNamedPassenger",
            "SumPaidDriver",
            "SumUnNamedPassenger",
            "ValueCNGFitment",
            "ValueElectricalAccessories",
            "ValueNonElectricalAccessories"
        };

        public static List<string> GetServiceKeysForID(string serviceID)
        {
            switch (serviceID)
            {
                case "Personal":

                    return saveCarPersonalDetailsKeys;

                case "CarAdditionalInformation":

                    return saveCarAdditionalInformationKeys;

                default:

                    return new List<string>();
            }

        }

        public static string GetEndpointToSaveForm(string pageTitle)
        {
            switch (pageTitle)
            {
                case "Personal":

                    return GlobalSetting.Instance.SaveCarDetailsEndPoint;

                case "CarMakeModel":

                    return GlobalSetting.Instance.SaveCarMakeModelDetailsEndPoint;

                case "CarAdditionalInformation":

                    return GlobalSetting.Instance.SaveCarAdditionalInformationEndPoint;

                case "CarDriverDetails":

                    return GlobalSetting.Instance.SaveCarDriverDetailsEndPoint;

                case "CarVINModel":

                    return GlobalSetting.Instance.SaveCarVINModelDetailsEndPoint;

                case "DriverConvictionDetails" :

                    return GlobalSetting.Instance.SaveDriverConvictionDetailsEndPoint;
                case "DriverClaimDetails":
                    return GlobalSetting.Instance.SaveDriverClaimsEndPoint;

                default:

                    return string.Empty;
            }
        }

        public static string GetPostData(List<string> keys, List<object> capturedValues)
        {
            var postDictionary = new Dictionary<string, object>();
            int capturedValuesIndex = 0;
            foreach (var key in keys)
            {
                postDictionary.Add(key, capturedValues[capturedValuesIndex]);
                capturedValuesIndex++;
            }
            capturedValuesIndex = 0;
            var content = JsonConvert.SerializeObject(postDictionary);
            return content;
        }

        public static string GetCarPersonalFormPostData(SaveCarPersonalModel model)
        {
            JObject postData = new JObject(new JProperty("UserId", model.UserId),
                                           new JProperty("QuoteId", model.QuoteId),
                                           new JProperty("Title", model.Title),
                                           new JProperty("FirstName", model.FirstName),
                                           new JProperty("LastName", model.LastName),
                                           new JProperty("MiddleInitial", model.MiddleInitial),
                                           new JProperty("Suffix", model.Suffix),
                                           new JProperty("Gender", model.Gender),
                                           new JProperty("DateOfBirth", model.DateOfBirth),
                                           new JProperty("MaritalStatus", model.MaritalStatus),
                                           new JProperty("MobilePhone", model.MobilePhone),
                                           new JProperty("EmailId", model.EmailId),
                                           new JProperty("ZipCode", model.ZipCode),
                                           new JProperty("City", model.City),
                                           new JProperty("State", model.State),
                                           new JProperty("Address", model.Address),
                                           new JProperty("Citizenship", model.Citizenship),
                                           new JProperty("CitizenByBirth", model.CitizenByBirth),
                                           new JProperty("CitizenshipStartDate", model.CitizenshipStartDate),
                                           new JProperty("OccupationType", model.OccupationType),
                                           new JProperty("Industry", model.Industry),
                                           new JProperty("JobFunctions", model.JobFunctions),
                                           new JProperty("IsNamedDriver", model.IsNamedDriver),
                                           new JProperty("LicenseType", model.LicenseType),
                                           new JProperty("LicenseDuration", model.LicenseDuration),
                                           new JProperty("LicenseNo", model.LicenseNo),
                                           new JProperty("IsHaveAccidentsOrClaims", model.IsHaveAccidentsOrClaims),
                                           new JProperty("IsHaveConviction", model.IsHaveConviction),
                                           new JProperty("IsHaveNominee", model.IsHaveNominee),
                                           new JProperty("NomineeTitle", model.NomineeTitle),
                                           new JProperty("NomineeFirstName", model.NomineeFirstName),
                                           new JProperty("NomineeLastName", model.NomineeLastName),
                                           new JProperty("NomineeDOB", model.NomineeDOB),
                                           new JProperty("NomineeGender", model.NomineeGender),
                                           new JProperty("NomineeRelationship", model.NomineeRelationship),
                                           new JProperty("AppointeeName", model.AppointeeName),
                                           new JProperty("IsHomeOwner", model.IsHaveChild),
                                           new JProperty("IsHaveChild", model.IsHaveChild),
                                           new JProperty("NoOfCars", model.NoOfCars),
                                           new JProperty("PersonalClaimsList", new JArray(from p in model.PersonalClaimsList
                                                                                          select new JObject(new JProperty("QuoteId", p.QuoteId),
                                                                                                             new JProperty("ClaimDate", p.ClaimDate),
                                                                                                             new JProperty("ClaimAmount", p.ClaimAmount),
                                                                                                             new JProperty("ClaimType", p.ClaimType),
                                                                                                             new JProperty("EmailId", p.EmailId)))),

                                           new JProperty("PersonalConvictionsList", new JArray(from p in model.PersonalConvictionsList
                                                                                               select new JObject(new JProperty("QuoteId", p.QuoteId),
                                                                                                                  new JProperty("ConvictionDate", p.ConvictionDate),
                                                                                                                  new JProperty("ConvictionType", p.ConvictionType),
                                                                                                                  new JProperty("PointsIncurred", p.PointsIncurred),
                                                                                                                  new JProperty("FineIncurred", p.FineIncurred),
                                                                                                                  new JProperty("BanLength", p.BanLength),
                                                                                                                  new JProperty("BreathalysedReading", p.BreathalysedReading),
                                                                                                                  new JProperty("IsBreathalysed", p.IsBreathalysed),
                                                                                                                  new JProperty("IsAccident", p.IsAccident),
                                                                                                                  new JProperty("EmailId", p.EmailId)))));

            return postData.ToString();
        }

        public static string GetSaveCarDriverDetailsPostData(List<CarDriverModel> capturedValues)
        {
            JArray postData = new JArray(from p in capturedValues
                                         select new JObject(new JProperty("UserId", p.UserId),
                                                            new JProperty("Address", p.Address),
                                                            new JProperty("CitizenByBirth", p.CitizenByBirth),
                                                            new JProperty("Citizenship", p.Citizenship),
                                                            new JProperty("CitizenshipStartDate", p.CitizenshipStartDate),
                                                            new JProperty("City", p.City),
                                                            new JProperty("DateOfBirth", p.DateOfBirth),
                                                            new JProperty("EmailId", p.EmailId),
                                                            new JProperty("FirstName", p.FirstName),
                                                            new JProperty("Gender", p.Gender),
                                                            new JProperty("IsHaveAccidentsOrClaims", p.IsHaveAccidentsOrClaims),
                                                            new JProperty("IsHaveConviction", p.IsHaveConviction),
                                                            new JProperty("LastName", p.LastName),
                                                            new JProperty("LicenseDuration", p.LicenseDuration),
                                                            new JProperty("LicenseNo", p.LicenseNo),
                                                            new JProperty("LicenseType", p.LicenseType),
                                                            new JProperty("MaritalStatus", p.MaritalStatus),
                                                            new JProperty("MiddleInitial", p.MiddleInitial),
                                                            new JProperty("MobilePhone", p.MobilePhone),
                                                            new JProperty("QuoteId", p.QuoteId),
                                                            new JProperty("State", p.State),
                                                            new JProperty("Suffix", p.Suffix),
                                                            new JProperty("Title", p.Title),
                                                            new JProperty("ZipCode", p.ZipCode)));

            return postData.ToString();
        }

        public static string GetDriverConvictionsDetailsPostData(List<DriverConvictionsList> capturedValues){
            JArray postData =new JArray(from p in capturedValues
                                        select new JObject(new JProperty("UserId", p.UserId),
                                                            new JProperty("QuoteId", p.QuoteId),
                                                            new JProperty("ConvictionDate", p.ConvictionDate),
                                                            new JProperty("ConvictionType", p.ConvictionType),
                                                            new JProperty("PointsIncurred", p.PointsIncurred),
                                                            new JProperty("FineIncurred", p.FineIncurred),
                                                            new JProperty("BanLength", p.BanLength),
                                                            new JProperty("BreathalysedReading", p.BreathalysedReading),
                                                            new JProperty("IsBreathalysed", p.IsBreathalysed),
                                                            new JProperty("IsAccident", p.IsAccident),
                                                            new JProperty("EmailId", p.EmailId)));
            return postData.ToString();
        }

        public static string GetDriverClaimPostData(List<DriverClaimsList> capturedValues)
        {
            JArray postData = new JArray(from p in capturedValues
                                         select new JObject(new JProperty("UserId", p.UserId),
                                                            new JProperty("QuoteId", p.QuoteId),
                                                            new JProperty("ClaimDate", p.ClaimDate),
                                                            new JProperty("ClaimAmount", p.ClaimAmount),
                                                            new JProperty("ClaimType", p.ClaimType),
                                                            new JProperty("EmailId", p.EmailId)));
            return postData.ToString();
        }

        public static string GetCarVINModelDetailsPostData(List<CarVINModel> capturedValues)
        {
            JArray postData = new JArray(from p in capturedValues
                                         select new JObject(new JProperty("UserId", p.UserId),
                                                            new JProperty("QuoteId", p.QuoteId),
                                                            new JProperty("VINNumber", p.VINNumber),
                                                            new JProperty("PrimaryUse", p.PrimaryUse),
                                                            new JProperty("ZipCode", p.ZipCode),
                                                            new JProperty("OwnLease", p.OwnLease),
                                                            new JProperty("SecurityAlarm", p.SecurityAlarm)));

            return postData.ToString();
        }

        public static string GetCarMakeModelDetailsPostData(List<CarMakeModel> capturedValues)
        {
            JArray postData = new JArray(from p in capturedValues
                                         select new JObject(new JProperty("UserId", p.UserId),
                                                            new JProperty("QuoteId", p.QuoteId),
                                                            new JProperty("RegistrationNumber", p.RegistrationNumber),
                                                            new JProperty("RegistrationDate", p.RegistrationDate),
                                                            new JProperty("ChassisNumber", p.ChassisNumber),
                                                            new JProperty("EngineNumber", p.EngineNumber),
                                                            new JProperty("ManufactureName", p.ManufactureName),
                                                            new JProperty("ModelName", p.ModelName),
                                                            new JProperty("YearofManufacture", p.YearofManufacture),
                                                            new JProperty("BodyType", p.BodyType),
                                                            new JProperty("DriverType", p.DriverType),
                                                            new JProperty("SafetyFeatures", p.SafetyFeatures),
                                                            new JProperty("SecurityFeatures", p.SecurityFeatures),
                                                            new JProperty("Accessories", p.Accessories),
                                                            new JProperty("BodyModifications", p.BodyModifications),
                                                            new JProperty("Brakes", p.Brakes),
                                                            new JProperty("EngineOrTransmission", p.EngineOrTransmission),
                                                            new JProperty("Paintwork", p.Paintwork),
                                                            new JProperty("Spoilers", p.Spoilers),
                                                            new JProperty("Wheels", p.Wheels),
                                                            new JProperty("NeedPolicyInName", p.NeedPolicyInName),
                                                            new JProperty("HasModificaiton", p.HasModification),
                                                            new JProperty("OwnershipCar", p.OwnershipCar)));

            return postData.ToString();
        }

        public static string GetCarClaimPostData(List<string> capturedValues)
        {
            JArray postData = new JArray(new JObject(new JProperty("QuoteId", capturedValues[0]),
                                                     new JProperty("ClaimDate", capturedValues[1]),
                                                     new JProperty("ClaimType", capturedValues[2]),
                                                     new JProperty("ClaimAmount", capturedValues[3]),
                                                     new JProperty("EmailId", capturedValues[4])));

            return postData.ToString();
        }

        public static string GetAdditionalInfoPostData(CarAdditionalInfoModel model)
        {
            JObject postData = new JObject(new JProperty("UserId", model.UserId),
                                           new JProperty("QuoteId", model.QuoteId),
                                           new JProperty("AddBreakdownCoverage", model.AddBreakdownCoverage),
                                           new JProperty("AddCNGFitment", model.AddCNGFitment),
                                           new JProperty("AddNamedPassenger", model.AddNamedPassenger),
                                           new JProperty("AddElectricalAccessories", model.AddElectricalAccessories),
                                           new JProperty("AddNonElectricalAccessories", model.AddNonElectricalAccessories),
                                           new JProperty("AddPaidDriverCover", model.AddPaidDriverCover),
                                           new JProperty("AddPersonalAccidentCover", model.AddPersonalAccidentCover),
                                           new JProperty("AddUnNamedPassenger", model.AddUnNamedPassenger),
                                           new JProperty("AnotherVechile", model.AnotherVechile),
                                           new JProperty("CarUsage", model.CarUsage),
                                           new JProperty("CoverStartDate", model.CoverStartDate),
                                           new JProperty("CoverType", model.CoverType),
                                           new JProperty("DayTimeParking", model.DayTimeParking),
                                           new JProperty("ExcessPay", model.ExcessPay),
                                           new JProperty("HasExistingPolicyUs", model.HasExistingPolicyUs),
                                           new JProperty("HasNCB", model.HasNCB),
                                           new JProperty("HasProtectedNCB", model.HasProtectedNCB),
                                           new JProperty("NeedToProtectNCB", model.NeedToProtectNCB),
                                           new JProperty("ExistingCoverType", model.ExistingCoverType),
                                           new JProperty("Mileage", model.Mileage),
                                           new JProperty("NCBDuration", model.NCBDuration),
                                           new JProperty("NightTimeParking", model.NightTimeParking),
                                           new JProperty("NumberOfDrivers", model.NumberOfDrivers),
                                           new JProperty("NumberOfNamedPassenger", model.NumberOfNamedPassenger),
                                           new JProperty("NumberofUnNamedPassenger", model.NumberofUnNamedPassenger),
                                           new JProperty("PaymentMethod", model.PaymentMethod),
                                           new JProperty("PolicyEndDate", model.PolicyEndDate),
                                           new JProperty("PolicyNumber", model.PolicyNumber),
                                           new JProperty("PolicyStartDate", model.PolicyStartDate),
                                           new JProperty("QuoteType", model.QuoteType),
                                           new JProperty("SumNamedPassenger", model.SumNamedPassenger),
                                           new JProperty("SumUnNamedPassenger", model.SumUnNamedPassenger),
                                           new JProperty("SumPaidDriver", model.SumPaidDriver),
                                           new JProperty("ValueCNGFitment", model.ValueCNGFitment),
                                           new JProperty("ValueElectricalAccessories", model.ValueElectricalAccessories),
                                           new JProperty("ValueNonElectricalAccessories", model.ValueNonElectricalAccessories));



            return postData.ToString();
        }

        public static string GetAuthorizationPostData(UserModel userModel)
        {
            JObject postData = new JObject(new JProperty("UserName", userModel.UserName),
                                           new JProperty("Password", userModel.Password));
            return postData.ToString();
        }

        public static string GetUserIdPostData(string quoteId)
        {
            JObject postData = new JObject(new JProperty("QuoteId", quoteId));
            return postData.ToString();
        }

        public static string GetTriggerEmailPostData(TriggerEmailModel model)
        {
            JObject postData = new JObject(new JProperty("MailTo", model.MailTo),
                                           new JProperty("QuoteId", model.QuoteId));
            return postData.ToString();
        }
    }
}