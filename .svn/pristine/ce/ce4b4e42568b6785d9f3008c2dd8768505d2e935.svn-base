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
    public partial class PoliciesListView : ContentPage
    {
       public PoliciesListView()
        {
            Title = "List of Policies";
            InitializeComponent();
            InsuranceLists.ItemsSource = Data.ProductList;
        }

        void OnItemTapped(object sender,ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            ((ListView)sender).SelectedItem = null;
            Navigation.PushAsync(new DetailedPolicyView());
        }
    }
}
