using System;
using ACIA.Model;
using ACIA.Views;
using Xamarin.Forms;

namespace ACIA.Services.Navigation
{
    /// <summary>
    /// DUIN avigation stack.
    /// </summary>
    public class DUINavigationStack
    {
        public DUINavigationStack()
        {
            
        }

        /// <summary>
        /// Gets the current page.
        /// </summary>
        /// <returns>The current page.</returns>

        public static DynamicUIPage GetCurrentPage()
        {
            DynamicUIPage currentPage;
            var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
            if (masterDetailPage.Detail.Navigation.NavigationStack.Count > 0)
			{
                int index = masterDetailPage.Detail.Navigation.NavigationStack.Count - 1;
                currentPage = (DynamicUIPage)masterDetailPage.Detail.Navigation.NavigationStack[index];
                return currentPage;
			}
            else
            {
                return new DynamicUIPage(new DynamicPageModel());
            }
        }

    }
}
