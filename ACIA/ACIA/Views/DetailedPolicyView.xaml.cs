using System;
using System.Collections.Generic;
using System.Net.Http;
using ACIA.Model;
using ACIA.Services.Dialog;
using ACIA.Services.RequestProvider;
using ACIA.Services.SaveAndContinueService;
using Xamarin.Forms;

namespace ACIA.Views
{
    public partial class DetailedPolicyView : ContentPage
    {

        IDialogService
        dialogService;
        IRequestProvider requestProvider;
        ISaveAndContinueService saveAndContinueService;
        public DetailedPolicyView()
        {
            Title = "Policy Details";
            InitializeComponent();
            InsuranceSplitDetails.ItemsSource = PolicyCoverageData.policyLists;

            for (int index = 0; index < PolicyCoverageData.policyLists.Count; index++)
            {
                if (index % 2 != 0)
                {
                    PolicyCoverageData.policyLists[index].BackgroundColor = "#ffffff";
                }
                else
                {
                    PolicyCoverageData.policyLists[index].BackgroundColor = "#FFFFFF";
                }
            }

            ToolbarItems.Add(new ToolbarItem("Done", null, () =>
            {
                Navigation.PushAsync(new CongratsView());
            }));

            GetQuotesAsync();
        }

        private async void GetQuotesAsync()
        {
            //dialogService = new DialogService();
            requestProvider = new RequestProvider();
            saveAndContinueService = new SaveAndContinueService(requestProvider);
            try
            {
                //dialogService.ShowLoader();
                var quotes = await saveAndContinueService.GetDataAsync<List<GetCarSuggestedQuotesModel>>(GlobalSetting.Instance.GetCarSuggestedQuotes);
                //dialogService.HideLoader();
                InsuranceSplitDetails.ItemsSource = quotes;
            }
            catch (HttpRequestException ex)
            {
                //dialogService.HideLoader();
                //await dialogService.ShowAlertAsync(ex.Message, "Alert", "Ok");
                await DisplayAlert("Alert", ex.Message, "Ok");
            }
        }

        public void OnItemTapped(object sender, ItemTappedEventArgs e)

        {
            if (e.Item == null) return;
            ((ListView)sender).SelectedItem = null;
        }
    }
}

