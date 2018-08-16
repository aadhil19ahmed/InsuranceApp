using System;
using System.Collections.Generic;
using ACIA.ViewModels;
using Xamarin.Forms;

namespace ACIA.Views
{
    public partial class CarFormChoiceView : ContentPage
    {
        bool isFirstTimeLaunch = true;
        public CarFormChoiceView()
        {
            InitializeComponent();

        }

        private void InitializeViews()
        {
            AddVehicleStack.WidthRequest = Section.Width / 2;
            AddVehiclePicker.WidthRequest = Section.Width / 2;
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

    }
}
