﻿using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using System.Net;

namespace ACIA.Droid
{
    [Activity(Label = "ACIA.Droid", Icon = "@drawable/acia_icon", Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            ServicePointManager
           .ServerCertificateValidationCallback +=
           (sender, cert, chain, sslPolicyErrors) => true;
            
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            //UserDialogs.Init(()=>(Activity)Forms.Context);
            LoadApplication(new App());
        }
    }
}
