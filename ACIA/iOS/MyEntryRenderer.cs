using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;
using System;

[assembly: ExportRenderer(typeof(ACIA.Views.UILayer.MyEntry), typeof(ACIA.iOS.MyEntryRenderer))]
namespace ACIA.iOS
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // do whatever you want to the UITextField here!

                //Control.BorderStyle = UITextBorderStyle.None;

                //Control.Frame = new CGRect()
                //{
                //    X = 10.0f,
                //    Y = 20.0f,
                //    Width = 200.0f,
                //    Height = 30.0f,
                //};

            }
        }
    }
}
