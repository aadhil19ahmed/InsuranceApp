using System;
using Xamarin.Auth;

namespace ACIA.Services.Credentials
{
    public interface ICredentialsService
    {
        Account GetValueForKey(string key, string serviceID);

        void SaveCredentials(string name, string key, string valueForKey, string serviceID, string quoteID = "");

        void SaveQuoteId(string quoteID);

        string GetQuoteIDForGuestUser();

    }
}
