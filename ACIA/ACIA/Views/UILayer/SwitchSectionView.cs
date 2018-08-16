using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace ACIA.Views.UILayer
{ 
    public class SwitchSectionView : BaseSectionView
    {
        Label label;
        public SwitchSectionView()
        {
            
        }

		private void CreateSwitchView()
		{

			Label header = new Label
			{
				Text = "Switch",
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
				HorizontalOptions = LayoutOptions.Center
			};

            var switcher = new Switch
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

            switcher.Toggled += switcher_Toggled;

			var label = new Label
			{
				Text = "Switch is now False",
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			// Accomodate iPhone status bar.
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

			// Build the page.
			this.Content = new StackLayout
			{
				Children =
				{
					header,
					switcher,
					label
				}
			};
		}

		void switcher_Toggled(object sender, ToggledEventArgs e)
		{
			label.Text = String.Format("Switch is now {0}", e.Value);
		}

		public override void CreateView()
		{
			base.CreateView();
			this.CreateSwitchView();
		}
    }
}
