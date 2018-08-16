using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ACIA.Views
{
    public partial class CarFormMMYView : ContentPage
    {
        bool isFirstTimeLaunch = true;
        public CarFormMMYView()
        {
            InitializeComponent();
        }

        private void InitializeViews()
        {

            RegistrationStack.WidthRequest = Section.Width / 2;
            RegistrationEntry.WidthRequest = Section.Width / 2;
            ChassisStack.WidthRequest = Section.Width / 2;
            ChassisEntry.WidthRequest = Section.Width / 2;
            ChassisStack.WidthRequest = Section.Width / 2;
            RegistrationDateStack.WidthRequest = Section.Width / 2;
            RegistrationDatePicker.WidthRequest = Section.Width / 2;
            EngineStack.WidthRequest = Section.Width / 2;
            EngineEntry.WidthRequest = Section.Width / 2;
            MakeStack.WidthRequest = Section.Width / 2;
            MakePicker.WidthRequest = Section.Width / 2;
            ModelStack.WidthRequest = Section.Width / 2;
            ModelPicker.WidthRequest = Section.Width / 2;
            YearStack.WidthRequest = Section.Width / 2;
            YearPicker.WidthRequest = Section.Width / 2;
            BodyTypeStack.WidthRequest = Section.Width / 2;
            BodyTypePicker.WidthRequest = Section.Width / 2;
            DriverStack.WidthRequest = Section.Width / 2;
            DriverPicker.WidthRequest = Section.Width / 2;
            FuelStack.WidthRequest = Section.Width / 2;
            FuelPicker.WidthRequest = Section.Width / 2;
            SafetyPicker.WidthRequest = Section.Width / 2;
            SecurityPicker.WidthRequest = Section.Width / 2;
            //ModificationsStack.WidthRequest = Section.Width / 2;
            //ModificationsPicker.WidthRequest = Section.Width / 2;
            AccessoriesPicker.WidthRequest = Section.Width / 2;
            BodyModificationsPicker.WidthRequest = Section.Width / 2;
            BrakesPicker.WidthRequest = Section.Width / 2;
            EnginePicker.WidthRequest = Section.Width / 2;
            PaintworkPicker.WidthRequest = Section.Width / 2;
            SpoilersPicker.WidthRequest = Section.Width / 2;
            WheelsPicker.WidthRequest = Section.Width / 2;
            OwnershipTypeStack.WidthRequest = Section.Width / 2;
            OwnershipPicker.WidthRequest = Section.Width / 2;
            RegisteredKeeperStack.WidthRequest = Section.Width / 2;
            RegisteredKeeper.WidthRequest = Section.Width / 2;
            RegisteredKeeperNameStack.WidthRequest = Section.Width / 2;
            RegisteredKeeperNameEntry.WidthRequest = Section.Width / 2;
            LegalOwnerStack.WidthRequest = Section.Width / 2;
            LegalOwnerPicker.WidthRequest = Section.Width / 2;
            LegalOwnerNameStack.WidthRequest = Section.Width / 2;
            LegalOwnerName.WidthRequest = Section.Width / 2;
            //PolicyNameStack.WidthRequest = Section.Width / 2;
            //PolicyNamePicker.WidthRequest = Section.Width / 2;
            
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

        void OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CarFormVinView());
        }

    }
}
