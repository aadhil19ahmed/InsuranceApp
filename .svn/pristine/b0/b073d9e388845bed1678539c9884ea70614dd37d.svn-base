using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ACIA.Model;

namespace ACIA.Views.UILayer
{
    /// <summary>
    /// DUIP icker view.
    /// </summary>
	public class DUIPickerView : DUInputView
	{
		public DUIPickerView(Model.View view)
		{
            Picker picker = new Picker
            {
                Title = "-- Please select an option --",
                TextColor = Color.LightGray,

            };
            List<string> items = new List<string>();

            foreach(var item in view.InputOptions)
            {
                items.Add(item.name);
            }
			picker.ItemsSource = items;
			var pickerStackLayout = new StackLayout();
            //picker.IsVisible = view.default_visibility;
			pickerStackLayout.Children.Add(picker);
			Content = pickerStackLayout;
            picker.SetBinding(Picker.SelectedItemProperty, "CapturedValue");
			picker.BindingContext = this;
		}
	}
}