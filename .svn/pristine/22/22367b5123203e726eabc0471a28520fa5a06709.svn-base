using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ACIA.Views
{
    public partial class DriverAddFormView : ContentPage
    {
        bool isFirstTimeLaunch = true;
        public DriverAddFormView()
        {
            InitializeComponent();
        }

        private void InitializeViews()
        {
            InputViewStack.WidthRequest = Section.Width / 2;
            TitlePicker.WidthRequest = Section.Width / 2;
            FirstNameEntry.WidthRequest = Section.Width / 2;
            FirstNameStack.WidthRequest = Section.Width / 2;
            LastNameEntry.WidthRequest = Section.Width / 2;
            LastNameStack.WidthRequest = Section.Width / 2;
            MiddleInitialNameEntry.WidthRequest = Section.Width / 2;
            MIStack.WidthRequest = Section.Width / 2;
            SuffixPicker.WidthRequest = Section.Width / 2;
            GenderPicker.WidthRequest = Section.Width / 2;
            GenderStack.WidthRequest = Section.Width / 2;
            DOBPicker.WidthRequest = Section.Width / 2;
            DOBStack.WidthRequest = Section.Width / 2;
            MartialStatusPicker.WidthRequest = Section.Width / 2;
            MartialStatusStack.WidthRequest = Section.Width / 2;
            MobileNumberEntry.WidthRequest = Section.Width / 2;
            MobileNumberStack.WidthRequest = Section.Width / 2;
            EmailAddressEntry.WidthRequest = Section.Width / 2;
            EmailAddressStack.WidthRequest = Section.Width / 2;
            ZipCodeEntry.WidthRequest = Section.Width / 2;
            ZipcodeStack.WidthRequest = Section.Width / 2;
            CityEntry.WidthRequest = Section.Width / 2;
            StateEntry.WidthRequest = Section.Width / 2;
            AddressEditor.WidthRequest = Section.Width / 2;
            AddressStack.WidthRequest = Section.Width / 2;
            CitizenShipEntry.WidthRequest = Section.Width / 2;
            CitizenShipStack.WidthRequest = Section.Width / 2;
            CitizenShipStartDateEntry.WidthRequest = Section.Width / 2;
            CitizenshipStartDateStack.WidthRequest = Section.Width / 2;
            LicenseTypePicker.WidthRequest = Section.Width / 2;
            LicenseTypeStack.WidthRequest = Section.Width / 2;
            LicenseDurationPicker.WidthRequest = Section.Width / 2;
            LicenseDurationStack.WidthRequest = Section.Width / 2;
            DrivingLicenseNumberEntry.WidthRequest = Section.Width / 2;
            DrivingLicenseNumberStack.WidthRequest = Section.Width / 2;
            ClaimDateEntry.WidthRequest = Section.Width / 2;
            ClaimDateStack.WidthRequest = Section.Width / 2;
            ClaimTypePicker.WidthRequest = Section.Width / 2;
            ClaimTypeStack.WidthRequest = Section.Width / 2;
            ClaimAmountEntry.WidthRequest = Section.Width / 2;
            ClaimAmountStack.WidthRequest = Section.Width / 2;
            ConvictionDateEntry.WidthRequest = Section.Width / 2;
            ConvictionDateStack.WidthRequest = Section.Width / 2;
            ConvictionTypePicker.WidthRequest = Section.Width / 2;
            ConvictionTypeStack.WidthRequest = Section.Width / 2;
            PointsIncurredEntry.WidthRequest = Section.Width / 2;
            BanLengthEntry.WidthRequest = Section.Width / 2;
            BreathalyserReadingEntry.WidthRequest = Section.Width / 2;
            BreathalyserReadingStack.WidthRequest = Section.Width / 2;
            FineIncurredEntry.WidthRequest = Section.Width / 2;

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

        void OnClicked(object sender,EventArgs e)
        {
            Navigation.PushAsync(new AdditionalFormView());
        }
    }
}
