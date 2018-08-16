using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ACIA.Views
{
    public partial class PersonalFormView : ContentPage
    {
        bool isFirstTimeLaunch = true;
        public PersonalFormView()
        {
            InitializeComponent();
        }

        private void InitializeViews()
        {
            InputViewStack.WidthRequest = Content.Width / 2;
            FirstNameStack.WidthRequest = Section.Width / 2;
            LastNameStack.WidthRequest = Section.Width / 2;
            MIStack.WidthRequest = Section.Width / 2;
            SuffixPicker.WidthRequest = Section.Width / 2;
            GenderStack.WidthRequest = Section.Width / 2;
            DOBStack.WidthRequest = Section.Width / 2;           
            MartialStatusStack.WidthRequest = Section.Width / 2;           
            MobileNumberStack.WidthRequest = Section.Width / 2;           
            EmailAddressStack.WidthRequest = Section.Width / 2;           
            ZipcodeStack.WidthRequest = Section.Width / 2;
            CityEntry.WidthRequest = Section.Width / 2;
            StateEntry.WidthRequest = Section.Width / 2;
            AddressStack.WidthRequest = Section.Width / 2;
            CitizenShipStack.WidthRequest = Section.Width / 2;           
            CitizenshipStartDateStack.WidthRequest = Section.Width / 2;
            OccupationTypeStack.WidthRequest = Section.Width / 2;
            IndustryStack.WidthRequest = Section.Width / 2;
            JobFunctionsStack.WidthRequest = Section.Width / 2;           
            LicenseTypeStack.WidthRequest = Section.Width / 2;           
            LicenseDurationStack.WidthRequest = Section.Width / 2;           
            DrivingLicenseNumberStack.WidthRequest = Section.Width / 2;           
            ClaimDateStack.WidthRequest = Section.Width / 2;
            ClaimTypeStack.WidthRequest = Section.Width / 2;           
            ClaimAmountStack.WidthRequest = Section.Width / 2;
            ConvictionDateStack.WidthRequest = Section.Width / 2;
            PointsIncurredEntry.WidthRequest = Section.Width / 2;
            BanLengthEntry.WidthRequest = Section.Width / 2;
            BreathalyserReadingStack.WidthRequest = Section.Width / 2;
            FineIncurredEntry.WidthRequest = Section.Width / 2;           
            ConvictionTypeStack.WidthRequest = Section.Width / 2;           
            NomineeTitleStack.WidthRequest = Section.Width / 2;
            NomineeRelationshipStack.WidthRequest = Section.Width / 2;
            NomineeGenderStack.WidthRequest = Section.Width / 2;
            NomineeFirstNameStack.WidthRequest = Section.Width / 2;          
            NomineeDOBStack.WidthRequest = Section.Width / 2;
            NomineeLastNameStack.WidthRequest = Section.Width / 2;           
            AppointeeNameStack.WidthRequest = Section.Width / 2;
            NoOfCarsEntry.WidthRequest = Section.Width / 2;
            SecurityNumberEntry.WidthRequest = Section.Width / 2;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if(isFirstTimeLaunch)
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
            Navigation.PushAsync(new CarFormChoiceView());
        }
    }
}
