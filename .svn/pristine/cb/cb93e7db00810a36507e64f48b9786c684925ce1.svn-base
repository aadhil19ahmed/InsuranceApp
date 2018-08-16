using System;
using Xamarin.Forms;

namespace ACIA.Views.UILayer
{
    public static class ACIANavigation
    {
        public static Page GetCurrentPage()
        {
            Page currentPage;

            var masterDetail = Application.Current.MainPage as MasterDetailPage;
            if (masterDetail.Detail.Navigation.NavigationStack.Count > 0)
            {
                int index = masterDetail.Detail.Navigation.NavigationStack.Count - 1;

                currentPage = masterDetail.Detail.Navigation.NavigationStack[index];
                return currentPage;
            }
            else
            {
                return new Page();
            }
        }
    }
}
