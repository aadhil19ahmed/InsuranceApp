using System;
using ACIA.Droid;
using ACIA.Services;
using Android.App;
using AndroidHUD;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(ProgressBar_Droid))]
namespace ACIA.Droid
{
    public class ProgressBar_Droid:IProgressIndicator
    {
        public ProgressBar_Droid()
        {
        }

        public void HideIndicator()
        {
            var activity = (Activity)Forms.Context;
            AndHUD.Shared.Show(activity);
        }

        public void ShowIndicator()
        {
            var activity = (Activity)Forms.Context;
            AndHUD.Shared.Dismiss(activity);
        }
    }
}
