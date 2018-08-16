using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using ACIA.Helper.Prompt;
using ACIA.Services.Dialog;
using ACIA.Services.GetZIPCodeMetadata;
using ACIA.Services.RequestProvider;
using ACIA.Services.SaveAndContinueService;
using Acr.UserDialogs;
using Xamarin.Forms;
using ACIA.Model;
using static ACIA.Model.InsuranceModel;
using System.Net.Http;
using ACIA.Services;

namespace ACIA.Views
{
    public partial class InsuranceListingView : ContentPage, INotifyPropertyChanged, IPromptHelper
    {
        public List<AutoModel> autoCategories;
        public List<HomeModel> homeCategories;
        IDialogService dialogService = new DialogService();
        ISaveAndContinueService saveAndContinueService;

        //image switch variables
        private Image autodropdown;
        private Image homedropdown;

        private string quote_id;

        //visibility parameters
        private bool _autoListVisibility;
        private bool _homeListVisibility;

        public InsuranceListingView()
        {
            InitializeComponent();
            InitData();
            _autoListVisibility = false;
            _homeListVisibility = false;

            //image switch variables assigning
            autodropdown = AutoDropDown;
            homedropdown = HomeDropDown;

            //listview population

            Automobiletypes.ItemsSource = autoCategories;
            Hometypes.ItemsSource = homeCategories;
            BindingContext = this;
        }

        void InitData()
        {
            autoCategories = new List<AutoModel>()
            {
                new AutoModel { AutoName="Car",AutoImage="pic_car.png"},
                new AutoModel { AutoName="Motorbike",AutoImage="pic_rv.png"},
                new AutoModel { AutoName="Van",AutoImage="pic_van.png"},
                new AutoModel { AutoName="Recreational",AutoImage="pic_boat.png"}
            };

            homeCategories = new List<HomeModel>()
            {
                new HomeModel {HomeName="Condo",HomeImage="pic_condo.png" },
                new HomeModel {HomeName="Home Owner",HomeImage="pic_homeowner.png" },
                new HomeModel {HomeName="Landlord",HomeImage="pic_landlord.png" },
                new HomeModel {HomeName="Renters",HomeImage="pic_renters.png" }
            };
        }

        public bool AutoListVisibility
        {
            get
            {
                return _autoListVisibility;
            }
            set
            {
                _autoListVisibility = value;
                NotifyPropertyChanged();
            }
        }

        public bool HomeListVisibility
        {
            get
            {
                return _homeListVisibility;
            }
            set
            {
                _homeListVisibility = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void OnAutoTapped(object sender, EventArgs e)
        {
            if(HomeListVisibility==true){
                HomeListVisibility = false;
                homedropdown.Source = "ic_action_expand_dark.png";
            }
            if (AutoListVisibility == false)
            {
                AutoListVisibility = true;
                autodropdown.Source = "ic_action_collapse_dark.png";
            }
            else
            {
                AutoListVisibility = false;
                autodropdown.Source = "ic_action_expand_dark.png";

            }
        }

        void OnHealthTapped(object sender, EventArgs e)
        {
            if (AutoListVisibility == true)
            {
                AutoListVisibility = false;

            }
        }

        void OnTravelTapped(object sender, EventArgs e)
        {
            if (AutoListVisibility == true)
            {
                AutoListVisibility = false;

            }
        }

        void OnHomeTapped(object sender, EventArgs e)
        {
            if (AutoListVisibility == true)
            {
                AutoListVisibility = false;
                autodropdown.Source = "ic_action_expand_dark.png";
            }
            if(HomeListVisibility==false){
                HomeListVisibility = true;
                homedropdown.Source = "ic_action_collapse_dark.png";
            }else{
                HomeListVisibility = false;
                homedropdown.Source = "ic_action_expand_dark.png";
            }
        }

        //method for loader
        public void ShowIndicator()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    DependencyService.Get<IProgressIndicator>().ShowIndicator();
                    break;
                default:
                    dialogService.ShowLoader();
                    break;
            }

        }

        /** Use this method to  hide the activity indicator */
        public void HideIndicator()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    DependencyService.Get<IProgressIndicator>().HideIndicator();
                    break;
                default:
                    dialogService.HideLoader();
                    break;
            }

        }


        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = (AutoModel)e.SelectedItem;
            if (selectedItem.AutoName == "Car")
            {
                switch(Device.RuntimePlatform){
                    case Device.Android:
                        IRequestProvider requestProvider = new RequestProvider();
                        saveAndContinueService = new SaveAndContinueService(requestProvider);
                        try
                        {
                            if (Application.Current.Properties.ContainsKey("quoteid"))
                            {
                                quote_id = (string)Application.Current.Properties["quoteid"];
                            }
                            if(String.IsNullOrEmpty(quote_id))
                            {
                                var quoteID = await saveAndContinueService.GetDataAsync<GetQuoteIDModel>(GlobalSetting.Instance.GetGeneratedQuoteId);
                                Application.Current.Properties["quoteid"] = quoteID.GeneratedQuoteId;
                                await Navigation.PushAsync(new PersonalFormView());
                            }
                            else{
                                await Navigation.PushAsync(new PersonalFormView());
                            }

                        }
                        catch (HttpRequestException ex)
                        {
                            //dialogService.HideLoader();
                            //await dialogService.ShowAlertAsync(ex.Message, "Alert", "Ok");
                            await DisplayAlert("Alert", ex.Message, "Ok");
                        }
                        break;

                    default:
                        dialogService.ShowPromptAsync(this, "Enter ZipCode", "US-ZipCode", InputType.Number, 5, "Get Quote");
                        break;
                }
            }
            else
            {
                selectedItem = null;
            }

        }

        string _usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";

        private bool IsUsZipCode(string zipCode)
        {
            bool validZipCode = true;
            if ((!Regex.Match(zipCode, _usZipRegEx).Success))
            {
                validZipCode = false;
            }
            return validZipCode;
        }

        public async void PromptOkActionAsync(string zipCode)
        {
            bool isVaildUsZipCode = IsUsZipCode(zipCode.ToString());
            if (isVaildUsZipCode)
            {
                IRequestProvider requestProvider = new RequestProvider();
                //IZIPCodeService zipCodeService = new ZipCodeService(requestProvider);
                //dialogService.ShowLoader();

                //var geoModel = await zipCodeService.GetZipCodeMetaDataAsync(zipCode.ToString());
                //dialogService.HideLoader();
                //if (geoModel.status.ToLower() != "ok")
                //{
                    saveAndContinueService = new SaveAndContinueService(requestProvider);
                    try
                    {
                        

                        if (Application.Current.Properties.ContainsKey("quoteid"))
                        {
                            quote_id = (string)Application.Current.Properties["quoteid"];
                        }

                        if (String.IsNullOrEmpty(quote_id))
                        {
                            dialogService.ShowLoader();
                            var quoteID = await saveAndContinueService.GetDataAsync<GetQuoteIDModel>(GlobalSetting.Instance.GetGeneratedQuoteId);
                            dialogService.HideLoader();
                            Application.Current.Properties["quoteid"] = quoteID.GeneratedQuoteId;
                            await Navigation.PushAsync(new PersonalFormView());
                        }
                        else
                        {
                            await Navigation.PushAsync(new PersonalFormView());
                        }
                    }
                    catch(HttpRequestException ex)
                    {
                        dialogService.HideLoader();
                        await dialogService.ShowAlertAsync(ex.Message, "Alert", "Ok");
                    }
                //}
                //else
                //{
                //    await dialogService.ShowAlertAsync(geoModel.status, "Alert", "Ok");
                //}
            }
            else
            {
                await dialogService.ShowAlertAsync("Please enter valid US zipcode", "Invalid", "Ok");
            }
        }
    }
}

