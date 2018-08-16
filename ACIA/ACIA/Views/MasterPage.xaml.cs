using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ACIA.Services.GetDynamicPage;
using ACIA.Services.RequestProvider;
using ACIA.Views.UILayer;
using Xamarin.Forms;

namespace ACIA.Views
{
    public partial class MasterPage : MasterDetailPage
    {
        public ObservableCollection<MainMenuItem> mainMenuItems;
        public MasterPage()
        {
            InitializeComponent();
            InitItems();
            bool IsKeepMeLoggedIn = false;
            if (Application.Current.Properties.ContainsKey("IsKeepMeLoggedIn"))
            {
                IsKeepMeLoggedIn = (bool)Application.Current.Properties["IsKeepMeLoggedIn"];
            }

            if(IsKeepMeLoggedIn){
                IRequestProvider requestProvider = new RequestProvider();
                IGetDynamicPageService dynamicPageService = new GetDynamicPageService(requestProvider);
                DynamicUIPage homePage = dynamicPageService.GetDynamicPage("LoggedInUser.json");
                mainMenuItems.Add(new MainMenuItem { Title = "Logout", Icon = "" });
                Detail = new NavigationPage(homePage);
            }else{
                Detail = new NavigationPage(new DashboardPage());
            }


            MenuListView.ItemsSource = mainMenuItems;
            MenuListView.ItemSelected += MenuListView_ItemSelectedAsync;
        }

        async void MenuListView_ItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = (MainMenuItem)e.SelectedItem;
            bool isGuestUser = false;
            if(Application.Current.Properties.ContainsKey("IsGuestUser"))
            {
                isGuestUser = (bool)Application.Current.Properties["IsGuestUser"];
            }

            if (isGuestUser && selectedItem.Title == "Home")
            {
                await Detail.Navigation.PopToRootAsync(true);
                Page rootPage = Detail.Navigation.NavigationStack[0] as Page;
                Detail.Navigation.InsertPageBefore(new DashboardPage(), rootPage);
                await Detail.Navigation.PopAsync();
            }
            else if (selectedItem.Title == "Home")
            {
                await Detail.Navigation.PopToRootAsync(true);
            }
            else if (selectedItem.Title == "Logout")
            {
                Application.Current.Properties["UserName"] = string.Empty;
                Application.Current.Properties["IsKeepMeLoggedIn"] = false;
                await Application.Current.SavePropertiesAsync();
                await Detail.Navigation.PopToRootAsync();
                Page rootPage = Detail.Navigation.NavigationStack[0] as Page;
                Detail.Navigation.InsertPageBefore(new DashboardPage(), rootPage);
                await Detail.Navigation.PopAsync();
                if(mainMenuItems.Count > 0)
                {
                    mainMenuItems.RemoveAt(4);
                }
            }
        }

        private void InitItems()
        {
            mainMenuItems = new ObservableCollection<MainMenuItem>() 
            {
                new MainMenuItem{Title="Home", Icon=""},
                new MainMenuItem{Title="Support", Icon=""},
                new MainMenuItem{Title="Feedback", Icon=""},
                new MainMenuItem{Title="News Feed", Icon=""}
            };
        }
    }
}
