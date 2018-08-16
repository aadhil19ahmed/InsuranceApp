using System;
using System.Collections.Generic;
using ACIA.Model;
using ACIA.Views.UILayer;
using Xamarin.Forms;

namespace ACIA.Views
{
    public partial class DynamicUIPage : ContentPage
    {
        public List<BaseSection> baseSections = new List<BaseSection>();

        public DynamicUIPage(DynamicPageModel pageModel)
        {
            InitializeComponent();
            //this.Title = "EPISERVER";
            this.Title = pageModel.Screenname;
            if (pageModel != null && pageModel.Sections != null)
                RenderSections(pageModel.Sections);
        }

        /// <summary>
        /// Renders the sections.
        /// </summary>
        /// <param name="sections">Sections.</param>
        public void RenderSections(List<BaseSection> sections)
        {
            var layout = new StackLayout()
            {
                Spacing = 7,
                //Margin = new Thickness(5),
                Padding = new Thickness(7),
                BackgroundColor = Color.FromHex("#D9D9D9")
            };

            //check If logged In User
            if (this.Title == "Dashboard")
            {
                var mainlayout = new StackLayout()
                {
                    Margin = new Thickness(2),
                    BackgroundColor = Color.White
                };

                var UserStack = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    HeightRequest = 120,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };

                var box = new BoxView
                {
                    WidthRequest = 18
                };

                var userImage = new Image
                {
                    Source = ImageSource.FromResource("ACIA.CommonResources.Images.loggeduser.png"),
                    Aspect = Aspect.AspectFit,
                    HorizontalOptions = LayoutOptions.Start,
                    Margin = new Thickness(5, 5, 5, 5),
                    VerticalOptions = LayoutOptions.Center
                };

                var labelStack = new StackLayout()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(20, 0, 0, 0)
                };

                var HelloText = new Label
                {
                    Text = "Hello",
                    FontSize = 14,
                    Style = Application.Current.Resources["ACIAThemeLabelStyle"] as Style
                };

                var UserName = new Label()
                {
                    Text = "Welcome Sudhakar",
                    FontSize = 14,
                    Style = Application.Current.Resources["ACIAThemeLabelStyle"] as Style
                };

                var visitedTime = new Label()
                {
                    Text = "Last visited 9,Jan 2018",
                    FontSize = 11,
                    Style = Application.Current.Resources["ACIAThemeLabelStyle"] as Style
                };

                labelStack.Children.Add(HelloText);
                labelStack.Children.Add(UserName);
                labelStack.Children.Add(visitedTime);

                UserStack.Children.Add(box);
                UserStack.Children.Add(userImage);
                UserStack.Children.Add(labelStack);

                mainlayout.Children.Add(UserStack);
                layout.Children.Add(mainlayout);
            }  
            var contentPage = new ContentPage();
            var ScrollView = new ScrollView();

            if (sections.Count == 0)
            {
                //AppUIConfig.ShowAlertView("Notification", "No Sections Found", "OK");
                this.DisplayAlert("Notification", "No Sections Found", "OK");
            }
            else
            {

                foreach (BaseSection section in sections)
                {
                    layout.Children.Add(section);
                    baseSections.Add(section);
                }
                ScrollView = new ScrollView
                {
                    Content = layout
                };
                Content = ScrollView;
            }
        }
    }
}
