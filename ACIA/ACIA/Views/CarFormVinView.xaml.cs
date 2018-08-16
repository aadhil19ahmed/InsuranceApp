using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ACIA.Views
{
    public partial class CarFormVinView : ContentPage
    {
        bool isFirstTimeLaunch = true;
        public CarFormVinView()
        {
            InitializeComponent();
        }

        private void InitializeViews()
        {

            VINStack.WidthRequest = Section.Width / 2;
            VINEntry.WidthRequest = Section.Width / 2;
            PrimaryUseStack.WidthRequest = Section.Width / 2;
            PrimaryUsePicker.WidthRequest = Section.Width / 2;
            PrimaryZipStack.WidthRequest = Section.Width / 2;
            PrimaryZipEntry.WidthRequest = Section.Width / 2;
            OwnLeaseStack.WidthRequest = Section.Width / 2;
            OwnLeasePicker.WidthRequest = Section.Width / 2;
            SecurityAlarmStack.WidthRequest = Section.Width / 2;
            SecurityAlarmPicker.WidthRequest = Section.Width / 2;
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (isFirstTimeLaunch)
            {
                isFirstTimeLaunch = false;
                InitializeViews();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void OnClicked(object sender,EventArgs e)
        {
            Navigation.PushAsync(new DriverDetailsFormView());
        }
    }
}
