using ACIA.Helper;
using ACIA.Services.Credentials;
using ACIA.Services.Dialog;
using ACIA.Services.GetDynamicPage;
using ACIA.Services.GetUserId;
using ACIA.Services.RequestProvider;
using ACIA.ViewModels.Base;
using ACIA.Views;
using ACIA.Views.UILayer;
using Xamarin.Forms;

namespace ACIA
{
    public partial class App : Application

    {
        public static int test { get; set; }
        public static string CurrentUser { get { return "ACIA"; } }
        public static string ContinueLaterUser { get { return "acia"; } }
        public static string GuestUser { get { return "AciaGuest"; } }
        public static ICredentialsService CredentialsService { get; private set; }
        private IRequestProvider requestProvider;
        private IGetUserIdService getUserIdService;
        private IDialogService dialogService;

        public App()
        {
            CredentialsService = new CredentialsService();
            InitializeComponent();
            InitApp();

            if(Application.Current.Resources == null)
            {
                Application.Current.Resources = new ResourceDictionary();
            }

            var SectionTitleLabelStyle = new Style(typeof(Label))
            {
                Setters = 
                {
                    new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FC5646") },
                    new Setter { Property = VisualElement.BackgroundColorProperty, Value = Color.White },
                    new Setter { Property = VisualElement.HeightRequestProperty, Value = 40 },
                    new Setter { Property = Label.FontFamilyProperty, Value = "HelveticaNeue-Medium" },
                    new Setter { Property = Label.FontSizeProperty, Value = 20 },
                    new Setter { Property = Label.MarginProperty, Value = new Thickness(10,10,0,0) }
                }
            };
            Application.Current.Resources.Add("SectionTitleLabel", SectionTitleLabelStyle);

            //MasterDetailPage
            Application.Current.MainPage = new MasterPage();
            MessagingCenter.Subscribe<string, string>(this, "quoteid", async (sender, arg) =>
            {
                Application.Current.Properties["quoteid"] = arg;
                requestProvider = new RequestProvider();
                getUserIdService = new GetUserIdService(requestProvider);
                dialogService = new DialogService();
                var postData = ServicesHelper.GetUserIdPostData(arg);
                dialogService.ShowLoader();
                var userIdModel = await getUserIdService.GetUserIdAsync(postData,GlobalSetting.Instance.GetUserIdEndPoint);
                dialogService.HideLoader();
                var masterDetailPage = Application.Current.MainPage as MasterPage;
                await masterDetailPage.Detail.Navigation.PopToRootAsync(true);
                Page rootPage = masterDetailPage.Detail.Navigation.NavigationStack[0] as Page;
                if (!string.IsNullOrEmpty(userIdModel.UserId))
                {
                    Application.Current.Properties["UserName"] = userIdModel.UserName;
                    //App.CredentialsService.SaveCredentials(userIdModel.UserName, userIdModel.UserName, userIdModel.UserId, App.CurrentUser);
                    var currentUserAccount = App.CredentialsService.GetValueForKey(userIdModel.UserName, App.CurrentUser);
                    if (currentUserAccount.Properties.ContainsKey(userIdModel.UserName))
                    {
                        if (masterDetailPage.mainMenuItems.Count <= 4)
                        {
                            masterDetailPage.mainMenuItems.Add(new MainMenuItem { Title = "Logout", Icon = "" });
                        }
                        IGetDynamicPageService dynamicPageService = new GetDynamicPageService(requestProvider);
                        DynamicUIPage homePage = dynamicPageService.GetDynamicPage("LoggedInUser.json");
                        masterDetailPage.Detail.Navigation.InsertPageBefore(homePage, rootPage);
                        await masterDetailPage.Detail.Navigation.PopAsync();
                        await masterDetailPage.Detail.Navigation.PushAsync(new PersonalFormView());
                    }
                    else
                    {
                        if(masterDetailPage.mainMenuItems.Count > 4)
                        {
                            masterDetailPage.mainMenuItems.RemoveAt(4);
                        } 
                        masterDetailPage.Detail.Navigation.InsertPageBefore(new DashboardPage(), rootPage);
                        await masterDetailPage.Detail.Navigation.PopAsync();
                    }
                }
                else{
                    if(masterDetailPage.mainMenuItems.Count > 4)
                    {
                        masterDetailPage.mainMenuItems.RemoveAt(4);
                    }    
                    masterDetailPage.Detail.Navigation.InsertPageBefore(new DashboardPage(), rootPage);
                    await masterDetailPage.Detail.Navigation.PopAsync();
                }
            });
        }

        private void InitApp()
        {
            ViewModelLocator.RegisterDependencies();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Application.Current.Properties["quoteid"] = string.Empty;
            Application.Current.Properties["IsGuestUser"] = false;
            Application.Current.Properties["IsNamedDriver"] = false;
            Application.Current.Properties["FirstName"] = string.Empty;
            Application.Current.Properties["LastName"] = string.Empty;
            Application.Current.Properties["EmailId"] = string.Empty;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
