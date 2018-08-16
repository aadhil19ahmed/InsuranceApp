using System;
using System.Collections.Generic;
using ACIA.Model;
using ACIA.Services.Dialog;
using ACIA.Services.GetDynamicPage;
using ACIA.Services.GetZIPCodeMetadata;
using ACIA.Services.RequestProvider;
using ACIA.Views.UILayer;
using Xamarin.Forms;
using System.Reflection;
using ACIA.Services.Navigation;
using ACIA.Services.Identity;
using ACIA.Helper;

namespace ACIA.Views
{
    public partial class DashboardPage : ContentPage
    {
        private bool _pbIndicator;
        public bool PBIndicator
        {
            get
            {
                return _pbIndicator;
            }
            set
            {
                _pbIndicator = value;
                OnPropertyChanged();
            }
        }
        IRequestProvider requestProvider;
        IIdentityService identityService;
        IDialogService dialogService;
        IGetDynamicPageService dynamicPageService;
        void PasswordEntry_Unfocused(object sender, FocusEventArgs e)
        {

        }

        Label messageLabel;
        MyEntry usernameEntry, passwordEntry;

        public DashboardPage()
        {
            InitializeComponent();
            requestProvider = new RequestProvider();
            identityService = new IdentityService(requestProvider);
            dynamicPageService = new GetDynamicPageService(requestProvider);
            dialogService = new DialogService();
            Title = "EPISERVER";

            var parentLayout = new AbsoluteLayout();

            var parentStackLayout = new StackLayout
            {

            };
            AbsoluteLayout.SetLayoutFlags(parentStackLayout, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(parentStackLayout, new Rectangle(0f, 0f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            var activityIndicator = new ActivityIndicator
            {
                Color = Color.Black,
                IsRunning = PBIndicator,
                IsVisible = PBIndicator
            };
            AbsoluteLayout.SetLayoutFlags(activityIndicator, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(activityIndicator, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, new Binding(nameof(PBIndicator)));
            activityIndicator.SetBinding(IsVisibleProperty, new Binding(nameof(PBIndicator)));
            activityIndicator.BindingContext = this;

            DynamicUIPage homePage = dynamicPageService.GetDynamicPage("HomePage.json");
            parentStackLayout.Children.Add(homePage.Content);

            var stackUserInteraction = new StackLayout
            {
                Padding = 20.0,
                Spacing = 10.0
            };

            messageLabel = new Label();
            var stackLayout = new StackLayout();

            var emptystackLayout = new StackLayout()
            {
                HeightRequest = 10.0

            };
            usernameEntry = new MyEntry
            {
                Placeholder = "User Name",
                PlaceholderColor = Color.LightGray,
                TextColor = Color.FromHex("FB6C5D"),
                FontFamily = "HelveticaMedium",
                Margin = 3,
                Text = "",
                Keyboard = Keyboard.Email,
                BackgroundColor = Color.Transparent,

            };

            //var userentrylabel = new Label
            //{
            //    HeightRequest = 1,
            //    BackgroundColor = Color.FromHex("FB6C5D"),
            //};

            usernameEntry.Focused += Entry_Focused;
            usernameEntry.Unfocused += Entry_Unfocused;

            passwordEntry = new MyEntry
            {
                Placeholder = "Password",
                TextColor = Color.FromHex("FB6C5D"),
                PlaceholderColor = Color.LightGray,
                FontFamily = "HelveticaMedium",
                IsPassword = true,
                Margin = 3,
                Text = "",
                BackgroundColor = Color.Transparent
            };

            //var passentrylabel = new Label
            //{
            //    HeightRequest = 1,
            //    BackgroundColor = Color.FromHex("FB6C5D"),

            //};

            passwordEntry.Focused += Entry_Focused;
            passwordEntry.Unfocused += Entry_Unfocused;

            void Entry_Focused(object sender, FocusEventArgs e)
            {
                Content.LayoutTo(new Rectangle(0, -100, Content.Bounds.Width, Content.Bounds.Height));
            }

            void Entry_Unfocused(object sender, FocusEventArgs e)
            {
                Content.LayoutTo(new Rectangle(0, 0, Content.Bounds.Width, Content.Bounds.Height));
            }

            stackLayout.Children.Add(emptystackLayout);
            stackUserInteraction.Children.Add(usernameEntry);
            //stackUserInteraction.Children.Add(userentrylabel);
            stackUserInteraction.Children.Add(passwordEntry);
            //stackUserInteraction.Children.Add(passentrylabel);
            stackLayout.Children.Add(stackUserInteraction);

            var submitstack = new StackLayout
            {
                HeightRequest = 50,
                Padding = 20.0
            };
            var submitButton = new Button()
            {
                Text = "LOG IN",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 100,
                HeightRequest = 30,
                BackgroundColor = Color.FromHex("FB6C5D")
            };

            submitButton.Clicked += SubmitButton_ClickedAsync;
            var tapgesture = new TapGestureRecognizer();

            var registerlabel = new Label
            {
                Text = "Sign Up | Forgot Passsword",
                FontFamily = "HelveticaMedium",
                TextColor = Color.FromHex("FB6C5D"),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontAttributes = FontAttributes.Bold,
            };

            registerlabel.GestureRecognizers.Add(tapgesture);
            stackLayout.Children.Add(submitButton);
            stackLayout.Children.Add(messageLabel);
            stackLayout.Children.Add(registerlabel);

            var basestackLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 100
            };

            var baseTopBox = new BoxView
            {
                HeightRequest = 2,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex("#e1e1e1"),
            };

            var baseStackHorizontalLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                MinimumWidthRequest = 400

            };

            var leftCornerBox = new BoxView
            {
                WidthRequest = 2,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex("#e1e1e1")
            };

            var newquoteLayout = new StackLayout
            {
                WidthRequest = 85
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                Navigation.InsertPageBefore(new InsuranceListingView(), this);
                Navigation.PopAsync();
                Application.Current.Properties["IsGuestUser"] = true;
                Application.Current.SavePropertiesAsync();
                //Navigation.PopModalAsync(true);
            };

            newquoteLayout.GestureRecognizers.Add(tapGestureRecognizer);

            var newquoteimage = new Image
            {
                Source = "pic_getquote",
                HeightRequest = 45,
                WidthRequest = 55,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            var newQuoteLabel = new Label
            {
                Text = "New Quote",
                FontSize = 12,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                Margin = new Thickness(0, 0, 0, 5)
            };

            var feedbackLayout = new StackLayout
            {
                WidthRequest = 85
            };

            var feedbackimage = new Image
            {
                Source = "pic_messages",
                HeightRequest = 45,
                WidthRequest = 55,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Aspect = Aspect.AspectFill,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            var feedbackLabel = new Label
            {
                Text = "Feedback",
                FontSize = 12,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                Margin = new Thickness(0, 0, 0, 5)

            };

            var newsfeedLayout = new StackLayout
            {
                WidthRequest = 85
            };

            var newsfeedimage = new Image
            {
                Source = "pic_newsfeed.png",
                HeightRequest = 60,
                WidthRequest = 70,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Aspect = Aspect.AspectFill,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            var newsfeedLabel = new Label
            {
                Text = "Newsfeed",
                FontSize = 12,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                Margin = new Thickness(0, 0, 0, 5)
            };

            var quickActionLayout = new StackLayout
            {
                WidthRequest = 85
            };

            var quickActionimage = new Image
            {
                Source = "pic_renew",
                HeightRequest = 50,
                WidthRequest = 55,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Aspect = Aspect.AspectFill,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            var quickActionLabel = new Label
            {
                Text = "Quick Action",
                FontSize = 11,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                Margin = new Thickness(0, 0, 0, 5)
            };

            var rightCornerBox = new BoxView
            {
                WidthRequest = 4,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex("#e1e1e1")
            };

            newquoteLayout.Children.Add(newquoteimage);
            newquoteLayout.Children.Add(newQuoteLabel);

            feedbackLayout.Children.Add(feedbackimage);
            feedbackLayout.Children.Add(feedbackLabel);

            newsfeedLayout.Children.Add(newsfeedimage);
            newsfeedLayout.Children.Add(newsfeedLabel);

            quickActionLayout.Children.Add(quickActionimage);
            quickActionLayout.Children.Add(quickActionLabel);

            baseStackHorizontalLayout.Children.Add(leftCornerBox);
            baseStackHorizontalLayout.Children.Add(newquoteLayout);
            baseStackHorizontalLayout.Children.Add(feedbackLayout);
            baseStackHorizontalLayout.Children.Add(newsfeedLayout);
            baseStackHorizontalLayout.Children.Add(quickActionLayout);
            baseStackHorizontalLayout.Children.Add(rightCornerBox);

            basestackLayout.Children.Add(baseTopBox);
            basestackLayout.Children.Add(baseStackHorizontalLayout);

            parentStackLayout.Children.Add(stackLayout);
            parentStackLayout.Children.Add(basestackLayout);

            parentLayout.Children.Add(parentStackLayout);
            parentLayout.Children.Add(activityIndicator);
            Content = new ScrollView{Content=parentLayout};
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        async void SubmitButton_ClickedAsync(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(usernameEntry.Text) || String.IsNullOrEmpty(passwordEntry.Text))
            {
                //await dialogService.ShowAlertAsync("Please enter UserName and Password", "Alert", "Ok");
                await DisplayAlert("Alert", "Enter Username and Password", "OK");
            }else{
                UserModel userModel = new UserModel();
                userModel.UserName = usernameEntry.Text;
                userModel.Password = passwordEntry.Text;
                var postData = GetAuthorizationPostData(userModel);
                await Application.Current.SavePropertiesAsync();
                //dialogService.ShowLoader();
                PBIndicator = true;
                var response = await identityService.CreateAuthorizationRequestAsync(postData, GlobalSetting.Instance.AuthorizationEndPoint);
                //dialogService.HideLoader();
                PBIndicator = false;
                if (response.Status.ToUpper() == "SUCCESS")
                {
                    Application.Current.Properties["IsGuestUser"] = false;
                    Application.Current.Properties["IsKeepMeLoggedIn"] = true;
                    Application.Current.Properties["UserName"] = usernameEntry.Text;
                    await Application.Current.SavePropertiesAsync();
                    var masterDetailPage = Application.Current.MainPage as MasterPage;
                    masterDetailPage.mainMenuItems.Add(new MainMenuItem { Title = "Logout", Icon = "" });
                    masterDetailPage = null;
                    App.CredentialsService.SaveCredentials(usernameEntry.Text, usernameEntry.Text, response.UserId, App.CurrentUser);
                    DynamicUIPage homePage = dynamicPageService.GetDynamicPage("LoggedInUser.json");
                    Navigation.InsertPageBefore(homePage, this);
                    await Navigation.PopAsync();
                }
                else
                {
                    //await dialogService.ShowAlertAsync(response.Status, "Alert", "Ok");
                    await DisplayAlert("Alert", response.Status, "Ok");
                }
            }
        }

        //GetAuthorizationPostData
        private string GetAuthorizationPostData(UserModel userModel)
        {
            return ServicesHelper.GetAuthorizationPostData(userModel);
        }
    }
}

