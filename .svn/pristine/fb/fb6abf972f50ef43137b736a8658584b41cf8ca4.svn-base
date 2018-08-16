using System;
using System.Collections.Generic;
using Xamarin.Forms;
namespace ACIA.Views.UILayer
{
    /// <summary>
    /// Entry view.
    /// </summary>
	public class EntryView : DUInputView
	{
        public EntryView(Model.View view)
		{
			var entry = new Entry();
            entry.Placeholder = "Type here";
            entry.PlaceholderColor = Color.LightGray;
            entry.TextColor = Color.Gray;
			entry.SetBinding(Entry.TextProperty, "CapturedValue", BindingMode.TwoWay);
			entry.BindingContext = this;
			var stack = new StackLayout();
            //entry.IsVisible = view.default_visibility;
			stack.Children.Add(entry);
			Content = stack;
		}
	}
}
