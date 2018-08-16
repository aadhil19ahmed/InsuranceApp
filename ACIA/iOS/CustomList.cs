using System;
using ACIA.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ListView),typeof(CustomList))]
namespace ACIA.iOS
{
    
    public class CustomList:ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            if (this.Control == null) return;
            this.Control.TableFooterView = new UIKit.UIView();
        }
    }
}
