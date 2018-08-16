using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace ACIA.Views.UILayer
{
    /// <summary>
    /// Picker section view.
    /// </summary>
    public class PickerSectionView : BaseSectionView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:DynamicUI.UILayer.PickerSectionView"/> class.
        /// </summary>
        public PickerSectionView()
        {
        }

		Dictionary<string, Color> nameToColor = new Dictionary<string, Color>
		{
			{ "Aqua", Color.Aqua }, { "Black", Color.Black },
            { "Blue", Color.Blue }, { "Fucshia", Color.Accent },
			{ "Gray", Color.Gray }, { "Green", Color.Green },
			{ "Lime", Color.Lime }, { "Maroon", Color.Maroon },
			{ "Navy", Color.Navy }, { "Olive", Color.Olive },
			{ "Purple", Color.Purple }, { "Red", Color.Red },
			{ "Silver", Color.Silver }, { "Teal", Color.Teal },
			{ "White", Color.White }, { "Yellow", Color.Yellow }
		};

        /// <summary>
        /// Creates the picker view.
        /// </summary>
		private void CreatePickerView()
		{
			Label header = new Label
			{
				Text = "Picker",
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
				HorizontalOptions = LayoutOptions.Center
			};

			Picker picker = new Picker
			{
				Title = "Color",
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			foreach (string colorName in nameToColor.Keys)
			{
				picker.Items.Add(colorName);
			}

			BoxView boxView = new BoxView
			{
				WidthRequest = 150,
				HeightRequest = 150,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			picker.SelectedIndexChanged += (sender, args) =>
				{
					if (picker.SelectedIndex == -1)
					{
						boxView.Color = Color.Default;
					}
					else
					{
						string colorName = picker.Items[picker.SelectedIndex];
						boxView.Color = nameToColor[colorName];
					}
				};

			// Accomodate iPhone status bar.
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

			// Build the page.
			this.Content = new StackLayout
			{
				Children =
				{
					header,
					picker,
					boxView
				}
			};
		}

		public override void CreateView()
		{
			base.CreateView();
			this.CreatePickerView();
		}
    }
}
