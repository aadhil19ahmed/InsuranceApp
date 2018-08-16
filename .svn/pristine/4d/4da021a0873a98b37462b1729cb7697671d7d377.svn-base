using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace ACIA.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            ServicePointManager
            .ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => true;
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
        {
            if(userActivity.ActivityType == NSUserActivityType.BrowsingWeb)
            {
                var URL = userActivity.WebPageUrl;
                var urlParameters = GetUrlParameters(URL);
            }
            return base.ContinueUserActivity(application, userActivity, completionHandler);
        }

       public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            var parameters = GetUrlParameters(url);
            var quoteid = parameters["QuoteId"];
            MessagingCenter.Send<string, string>("", "quoteid", quoteid);
            return true;
        }

        //get url parameters
        private Dictionary<string, string> GetUrlParameters(NSUrl url)
        {
            var urlComponents = new NSUrlComponents(url, false);
            var queryParams = new Dictionary<string, string>();
            foreach (NSUrlQueryItem queryItem in urlComponents.QueryItems)
            {
                if(queryItem.Value == null)
                {
                    continue;
                }

                queryParams[queryItem.Name] = queryItem.Value;
            }
            return queryParams;
        }
    }
}
