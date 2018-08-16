using System;
using System.Collections.Generic;
using ACIA.Services.GetDynamicPage;
using ACIA.Services.RequestProvider;
using Xamarin.Forms;

namespace ACIA.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            LogoImage.Source = ImageSource.FromResource("ACIA.CommonResources.Images.profilepicture.png");
        }

        async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            IRequestProvider requestProvider = new RequestProvider();
            IGetDynamicPageService dynamicPageService = new GetDynamicPageService(requestProvider);
            DynamicUIPage homePage = dynamicPageService.GetDynamicPage("LoggedInUser.json");
            Application.Current.Properties["IsLoggedInUser"] = true;
            await Navigation.PushAsync(homePage);
            await Navigation.PopModalAsync();
        }
    }
}

