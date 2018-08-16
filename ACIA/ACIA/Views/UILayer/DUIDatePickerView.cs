using System;
using ACIA.Converters;
using Xamarin.Forms;

namespace ACIA.Views.UILayer
{
    public class DUIDatePickerView : DUInputView
    {
        public DUIDatePickerView(Model.View view)
        {
            DatePicker datePicker = new DatePicker
            {
                Format = "dd/MM/yyyy",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            datePicker.DateSelected+= DatePicker_DateSelected;
            var layout = new StackLayout();
            //datePicker.IsVisible = view.default_visibility;
            layout.Children.Add(datePicker);
            CapturedValue = datePicker.Date.ToString("dd/MM/yyyy");
            Content = layout;
        }

        void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            CapturedValue = e.NewDate.ToString("dd/MM/yyyy");
        }
    }
}
