using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ACIA.Services.SaveAndContinueService;
using ACIA.ViewModels.Base;
using Xamarin.Forms;
using ACIA.Views.UILayer;
using ACIA.Views;
using ACIA.Helper.Prompt;

namespace ACIA.ViewModels
{
    public class DriverDetailsFormViewModel:ViewModelBase,IConfirmDelegate
    {
        Page currentPage = ACIANavigation.GetCurrentPage();
        //non-mandatory fields
        //private bool _isDriverAdd;
        private string quoteID;
        private bool isGuestUser = false;
        //binding passed variables
        private string _firstName;
        private string _lastName;

        private ISaveAndContinueService _saveAndContinueServive;

        public DriverDetailsFormViewModel(ISaveAndContinueService saveAndContinueServive)
        {
            _saveAndContinueServive = saveAndContinueServive;

            if (Application.Current.Properties.ContainsKey("quoteid"))
            {
                quoteID = (string)Application.Current.Properties["quoteid"];
            }
            if (Application.Current.Properties.ContainsKey("FirstName"))
            {
                FirstName = (string)Application.Current.Properties["FirstName"];
            }

            if (Application.Current.Properties.ContainsKey("LastName"))
            {
                LastName = (string)Application.Current.Properties["LastName"];
            }

            if (Application.Current.Properties.ContainsKey("IsGuestUser"))
            {
                isGuestUser = (bool)Application.Current.Properties["IsGuestUser"];
            }


        }

        //binding value to passed data
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => FirstName);
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                RaisePropertyChanged(() => LastName);
            }
        }

        public ICommand SaveCommand => new Command(async () => await SaveAsync());

        private async Task SaveAsync()
        {
              await currentPage.Navigation.PushAsync(new AdditionalFormView());
               
        }

        public ICommand SaveAndContinueLaterCommand => new Command(async () => await SaveAndContinueLaterAction());

        private async Task SaveAndContinueLaterAction()
        {
            if (isGuestUser)
            {
                await currentPage.DisplayAlert("Alert", "kindly login to enable this feature", "Ok");
            }
            else{
                await currentPage.DisplayAlert("Alert", "Form Saved Successfully, return to Dashboard?", "OK", "Cancel");
            }
           
            //await DialogService.ShowConfirmAsync(this, "Form Saved Successfully, return to Dashboard?", "Ok", "Cancel");

        }

        public ICommand PreviousCommand => new Command(async () => await PreviousCommandAction());

        private async Task PreviousCommandAction()
        {
            await currentPage.Navigation.PopAsync();
        }

        public async void OnConfirmDialogActionAsync()
        {
            await currentPage.Navigation.PopToRootAsync();
        }
    }
}
