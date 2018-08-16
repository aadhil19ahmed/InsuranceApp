using System;
using System.Linq;
using Xamarin.Auth;

namespace ACIA.Services.Credentials
{
    public class CredentialsService : ICredentialsService
    {
        public Account GetValueForKey(string key, string serviceID)
        {
            var accounts = AccountStore.Create().FindAccountsForService(serviceID);
            foreach (Account account in accounts)
            {
                if (account.Username == key)
                {
                    return (account != null) ? account : null;
                }
            }
            return new Account();
        }

        public void DeleteCredentials(string key, string serviceID)
        {
            var accounts = AccountStore.Create().FindAccountsForService(serviceID);
            foreach (Account account in accounts)
            {
                if (account.Username == key)
                {
                    AccountStore.Create().Delete(account, serviceID);
                }
            }
        }

        public string GetQuoteIDForGuestUser()
        {
            var account = AccountStore.Create().FindAccountsForService(App.GuestUser).LastOrDefault();
            return (account != null) ? account.Properties["GuestUser"] : null;
        }

        public void SaveQuoteId(string quoteID)
        {
            Account account = new Account
            {
                Username = "Guest"
            };
            account.Properties.Add("GuestUser", quoteID);
            AccountStore.Create().Save(account, App.GuestUser); 
        }

        public void SaveCredentials(string name, string key, string valueForKey, string serviceID, string quoteID = "")
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(key))
            {
                Account account = new Account
                {
                    Username = name
                };
                account.Properties.Add(key, valueForKey);
                if(quoteID != string.Empty)
                {
                    account.Properties.Add("quoteID", quoteID);
                }
                AccountStore.Create().Save(account, serviceID);
            }

        }
    }
}
