using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ACIA.Services;
using ACIA.Services.SaveAndContinueService;
using ACIA.Validations;
using ACIA.ViewModels.Base;
using ACIA.Views;
using ACIA.Views.UILayer;
using Xamarin.Forms;

namespace ACIA.ViewModels
{
    public class CarFormChoiceViewModel:ViewModelBase
    {
        Page currentPage = ACIANavigation.GetCurrentPage();

        private bool _isValid;
        private string quoteID;

        //bindable attributes depending on passed data from other page
        private string _mmycarMake;
        private string _vincarMake;

        private ValidatableObject<string> _vehicleChoice;

        //control visibility
        //private bool _vehicleChoiceVisibility;

        private ISaveAndContinueService _saveAndContinueServive;

        public CarFormChoiceViewModel(ISaveAndContinueService saveAndContinueServive)
        {
            
            _saveAndContinueServive = saveAndContinueServive;
            _vehicleChoice = new ValidatableObject<string>();
            //VehicleChoiceVisibility = false;

            if (Application.Current.Properties.ContainsKey("quoteid"))
            {
                quoteID = (string)Application.Current.Properties["quoteid"];
            }

            if (Application.Current.Properties.ContainsKey("MMYCarMake"))
            {
                MMYCarMake = (string)Application.Current.Properties["MMYCarMake"];
                VehicleChoice.Value = "Year Make Model";
            }
            else if (Application.Current.Properties.ContainsKey("VINCarMake"))
            {
                VINCarMake = (string)Application.Current.Properties["VINCarMake"];
                VehicleChoice.Value = "VIN";
                //VehicleChoiceVisibility = true;
            }
            else
            {
                VehicleChoice.Value = null;
            }

            InitializePickerItemsSource();
            AddValidations();
        }



        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        //binding value to passed data
        public string MMYCarMake
        {
            get
            {
                return _mmycarMake;
            }
            set
            {
                _mmycarMake = value;
                RaisePropertyChanged(() => MMYCarMake);
            }
        }

        public string VINCarMake
        {
            get
            {
                return _vincarMake;
            }
            set
            {
                _vincarMake = value;
                RaisePropertyChanged(() => VINCarMake);
            }
        }

        public ValidatableObject<string> VehicleChoice
        {
            get
            {
                return _vehicleChoice;
            }
            set

            {
                _vehicleChoice = value;
                RaisePropertyChanged(() => VehicleChoice);

            }
        }

        //method for loader
        public void ShowIndicator()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    //DependencyService.Get<IProgressIndicator>().ShowIndicator();
                    break;
                default:
                    DialogService.ShowLoader();
                    break;
            }

        }

        /** Use this method to  hide the activity indicator */
        public void HideIndicator()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    //DependencyService.Get<IProgressIndicator>().HideIndicator();
                    break;
                default:
                    DialogService.HideLoader();
                    break;
            }

        }




            private ObservableCollection<string> _vehicleChoices;
            public ObservableCollection<string> VehicleChoices
            {
                get 
            { 
                return _vehicleChoices;
            }
                set
                {
                        _vehicleChoices = value;
                        RaisePropertyChanged(() => VehicleChoices);
                }
            }

        private void InitializePickerItemsSource()
        {
            VehicleChoices = new ObservableCollection<string>()
            {
                "Year Make Model", "VIN"
            };
        }

        public ICommand SaveCommand => new Command(async () => await SaveAsync());

        private async Task SaveAsync()
        {
            

                
                if (VehicleChoice.Value == "Year Make Model")
                {
                    await currentPage.Navigation.PushAsync(new CarFormMMYView());
                }
                else if (VehicleChoice.Value == "VIN")
                {
                    await currentPage.Navigation.PushAsync(new CarFormVinView());
                }
                else
                {
                    bool isValid = Validate();
                }
            //}
        }

        public ICommand SaveAndContinueLaterCommand => new Command(async () => await SaveAndContinueLaterAsync());

        //save and continue later
        private async Task SaveAndContinueLaterAsync()
        {
            bool isValid = Validate();
            Application.Current.Properties["IsContinueLater"] = true;
            Application.Current.Properties["ContinueLaterQuoteId"] = quoteID;
            //await DialogService.ShowAlertAsync("Navigate to next or previous page to save data", "Can't save data here", "OK");
            await currentPage.DisplayAlert("Can't save data here", "Navigate to next or previous page to save data", "OK");
        }

        public ICommand PreviousCommand => new Command(async () => await PreviousCommandAction());

        private async Task PreviousCommandAction()
        {
            await currentPage.Navigation.PopAsync();
        }

        public ICommand AddVehicleCommand => new Command(() => AddVehicleAction());

        private async void AddVehicleAction()
        {
            if(MMYCarMake==null&& VINCarMake==null)
            {
                if (VehicleChoice.Value == "Year Make Model")
                {
                    await currentPage.Navigation.PushAsync(new CarFormMMYView());
                }
                else if (VehicleChoice.Value == "VIN")
                {
                    await currentPage.Navigation.PushAsync(new CarFormVinView());
                }
            }

        }

        private bool Validate()
        {
            return _vehicleChoice.Validate();
        }

        private void AddValidations()
        {
            _vehicleChoice.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "You have to enter atleast one car." });
        }
    }
}
