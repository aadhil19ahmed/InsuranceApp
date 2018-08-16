using System;
using ACIA.Model;
using Xamarin.Forms;

namespace ACIA.Views
{
    public partial class ACIAPage : MasterDetailPage
    {
        public ACIAPage()
        {
            InitializeComponent();
            //masterPage.ListView.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                //masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
