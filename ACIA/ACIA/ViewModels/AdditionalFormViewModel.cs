using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using ACIA.Converters;
using ACIA.Helper;
using ACIA.Model;
using ACIA.Services.SaveAndContinueService;
using ACIA.Validations;
using ACIA.ViewModels.Base;
using Xamarin.Forms;
using ACIA.Views;
using ACIA.Views.UILayer;
using ACIA.Helper.Prompt;
using ACIA.Services;

namespace ACIA.ViewModels
{
    public class AdditionalFormViewModel : ViewModelBase,IConfirmDelegate
    {
        Page currentPage = ACIANavigation.GetCurrentPage();

        private bool _isValid;
        private string quoteID;
        private bool isGuestUser = false;
        private string currentUserId = string.Empty;
        private string continueLaterQuoteID = string.Empty;

        //validatable fields
        private ValidatableObject<string> _carUsedFor;
        private ValidatableObject<string> _dayTimeCarPark;
        private ValidatableObject<string> _nightTimeCarPark;
        private ValidatableObject<string> _isAnotherVehicle;
        private ValidatableObject<string> _noOfPaid;
        private ValidatableObject<string> _sumOfPaid;
        private ValidatableObject<string> _noOfNamedPassengers;
        private ValidatableObject<string> _sumOfNamedPassengers;
        private ValidatableObject<string> _noOfUnNamedPassengers;
        private ValidatableObject<string> _sumOfUnNamedPassengers;
        private ValidatableObject<string> _voluntaryExcess;
        private ValidatableObject<string> _totalAnnualMileage;

        //non-mandatory fields
        private string _paymentMethod;
        private string _coverStartDate;
        private string _quoteType;
        private string _coverType;
        private bool _isExistingPolicyWithUs;
        private string _policyNumber;
        private string _policyStartDate;
        private string _policyEndDate;
        private bool _isNCB;
        private string _nCBDuration;
        private bool _isNCBProtected;
        private bool _isWillNCBProtect;
        private bool _isAddBreakdownCover;
        private string _existingCoverType;
        private bool _isAddElectricalCover;
        private bool _isAddNonElectricalCover;
        private bool _isCNGFitmentCover;
        private bool _isPersonalAccidentCover;
        private string _electricalAccessoriesValue;
        private string _nonelectricalAccessoriesValue;
        private string _CNGFitmentValue;
        private bool _isPACoverPaidDrivers;
        private bool _isPACoverNamedPassengers;
        private bool _isPACoverUnnamedPassengers;

        //control visibility
        private bool _quoteTypePickerVisibility;
        private bool _isNCBVisibility;
        private bool _isWillNCBVisibility;
        private bool _isAddElectricalCoverVisibility;
        private bool _isAddNonElectricalCoverVisibility;
        private bool _isCNGFitmentCoverVisibility;
        private bool _isPACoverPaidVisibility;
        private bool _isPANamedVisibility;
        private bool _isPAUnNamedVisibility;


        private ISaveAndContinueService _saveAndContinueServive;

        public AdditionalFormViewModel(ISaveAndContinueService saveAndContinueServive)
        {
            _saveAndContinueServive = saveAndContinueServive;

            QuoteTypePickerVisibility = false;
            IsNCBVisibility = false;
            IsWillNCBVisibility = true;
            IsAddElectricalCoverVisibility = true;
            IsAddNonElectricalCoverVisibility = true;
            IsCNGFitmentCoverVisibility = true;
            IsPACoverPaidVisibility = false;
            IsPANamedVisibility = false;
            IsPAUnNamedVisibility = false;

            InitializePickerItemsSource();

            //Initialize Validatable Fields

            _carUsedFor = new ValidatableObject<string>();
            _dayTimeCarPark = new ValidatableObject<string>();
            _nightTimeCarPark = new ValidatableObject<string>();
            _isAnotherVehicle = new ValidatableObject<string>();
            _voluntaryExcess = new ValidatableObject<string>();
            _noOfPaid = new ValidatableObject<string>();
            _sumOfPaid = new ValidatableObject<string>();
            _noOfNamedPassengers = new ValidatableObject<string>();
            _sumOfNamedPassengers = new ValidatableObject<string>();
            _noOfUnNamedPassengers = new ValidatableObject<string>();
            _sumOfUnNamedPassengers = new ValidatableObject<string>();
            _totalAnnualMileage=new ValidatableObject<string>();

            IsNCB = false;
            IsNCBProtected = true;
            IsWillNCBProtect = false;
            IsAddBreakdownCover = false;
            IsAddElectricalCover = true;
            IsAddNonElectricalCover = true;
            IsCNGFitmentCover = true;
            IsPersonalAccidentCover = false;
            IsPACoverPaidDrivers = false;
            IsPACoverNamedPassengers = false;
            IsPACoverUnnamedPassengers = false;
            IsExistingPolicyWithUs = true;
            //Add validations
            AddValidations();

            if (Application.Current.Properties.ContainsKey("quoteid"))
            {
                quoteID = (string)Application.Current.Properties["quoteid"];
            }

            string currentUser = string.Empty;
            if (Application.Current.Properties.ContainsKey("UserName"))
            {
                currentUser = (string)Application.Current.Properties["UserName"];
            }

            var currentUserAccount = App.CredentialsService.GetValueForKey(currentUser, App.CurrentUser);
            if (currentUserAccount.Properties.ContainsKey(currentUser))
            {
                currentUserId = currentUserAccount.Properties[currentUser];
            }

            if (Application.Current.Properties.ContainsKey("IsGuestUser"))
            {
                isGuestUser = (bool)Application.Current.Properties["IsGuestUser"];
            }

            if (!isGuestUser)
            {
                GetListOfQuoteId();
            }    
        }

        private async void GetListOfQuoteId()
        {
            var uri = GlobalSetting.Instance.GetQuoteIdListEndPoint + "{" + currentUserId + "}";
            var response = await _saveAndContinueServive.GetDataAsync<ListOfQuoteId>(uri);
            foreach (var id in response.QuoteIdList)
            {
                if (id.QuoteId == quoteID)
                {
                    //method to populate form   
                    GetFormDataAsync(quoteID);
                }
            }

        }


        private async void GetFormDataAsync(string quote_id)
        {
            //DialogService.ShowLoader();
            try
            {
                var uri = GlobalSetting.Instance.GetCarAdditionalInformation + "{" + quote_id + "}";
                var response = await _saveAndContinueServive.GetDataAsync<CarAdditionalInfoModel>(uri);
                //DialogService.HideLoader();
                if (response != null)
                {
                    PopulateFormWithData(response);
                }
                else
                {
                    //await DialogService.ShowAlertAsync("Could not fetch form data", "Please try Again", "Ok");
                }
            }
            catch (HttpRequestException ex)
            {
                //DialogService.HideLoader();
                //await this.DialogService.ShowAlertAsync(ex.Message, "Alert", "Ok");
                await currentPage.DisplayAlert("Alert", ex.Message, "Ok");
            }
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

        //control visibility
        public bool QuoteTypePickerVisibility
        {
            get
            {
                return _quoteTypePickerVisibility;
            }
            set
            {
                _quoteTypePickerVisibility = value;
                RaisePropertyChanged(() => QuoteTypePickerVisibility);
            }
        }

        public bool IsNCBVisibility
        {
            get
            {
                return _isNCBVisibility;
            }
            set
            {
                _isNCBVisibility = value;
                RaisePropertyChanged(() => IsNCBVisibility);
            }
        }

        public bool IsWillNCBVisibility
        {
            get
            {
                return _isWillNCBVisibility;
            }
            set
            {
                _isWillNCBVisibility = value;
                RaisePropertyChanged(() => IsWillNCBVisibility);
            }
        }

        public bool IsAddElectricalCoverVisibility
        {
            get
            {
                return _isAddElectricalCoverVisibility;
            }
            set
            {
                _isAddElectricalCoverVisibility = value;
                RaisePropertyChanged(() => IsAddElectricalCoverVisibility);
            }
        }

        public bool IsAddNonElectricalCoverVisibility
        {
            get
            {
                return _isAddNonElectricalCoverVisibility;
            }
            set
            {
                _isAddNonElectricalCoverVisibility = value;
                RaisePropertyChanged(() => IsAddNonElectricalCoverVisibility);
            }
        }

        public bool IsCNGFitmentCoverVisibility
        {
            get
            {
                return _isCNGFitmentCoverVisibility;
            }
            set
            {
                _isCNGFitmentCoverVisibility = value;
                RaisePropertyChanged(() => IsCNGFitmentCoverVisibility);
            }
        }

        public bool IsPACoverPaidVisibility
        {
            get
            {
                return _isPACoverPaidVisibility;
            }
            set
            {
                _isPACoverPaidVisibility = value;
                RaisePropertyChanged(() => IsPACoverPaidVisibility);
            }
        }

        public bool IsPANamedVisibility
        {
            get
            {
                return _isPANamedVisibility;
            }
            set
            {
                _isPANamedVisibility = value;
                RaisePropertyChanged(() => IsPANamedVisibility);
            }
        }

        public bool IsPAUnNamedVisibility
        {
            get
            {
                return _isPAUnNamedVisibility;
            }
            set
            {
                _isPAUnNamedVisibility = value;
                RaisePropertyChanged(() => IsPAUnNamedVisibility);
            }
        }

        //Validate bindable Attributes
        public ValidatableObject<string> CarUsedFor
        {
            get
            {
                return _carUsedFor;
            }
            set
            {
                _carUsedFor = value;
                RaisePropertyChanged(() => CarUsedFor);
            }
        }

        public ValidatableObject<string> TotalAnnualMileage
        {
            get
            {
                return _totalAnnualMileage;
            }
            set
            {
                _totalAnnualMileage = value;
                RaisePropertyChanged(() => TotalAnnualMileage);
            }
        }

        public ValidatableObject<string> DayTimeCarPark
        {
            get
            {
                return _dayTimeCarPark;
            }
            set
            {
                _dayTimeCarPark = value;
                RaisePropertyChanged(() => DayTimeCarPark);
            }
        }
        public ValidatableObject<string> NightTimeCarPark
        {
            get
            {
                return _nightTimeCarPark;
            }
            set
            {
                _nightTimeCarPark = value;
                RaisePropertyChanged(() => NightTimeCarPark);
            }
        }

        public ValidatableObject<string> IsAnotherVehicle
        {
            get
            {
                return _isAnotherVehicle;
            }
            set
            {
                _isAnotherVehicle = value;
                RaisePropertyChanged(() => IsAnotherVehicle);
            }
        }


        public ValidatableObject<string> NoOfPaid
        {
            get
            {
                return _noOfPaid;
            }
            set
            {
                _noOfPaid = value;
                RaisePropertyChanged(() => NoOfPaid);
            }
        }

        public ValidatableObject<string> SumOfPaid
        {
            get
            {
                return _sumOfPaid;
            }
            set
            {
                _sumOfPaid = value;
                RaisePropertyChanged(() => SumOfPaid);
            }
        }

        public ValidatableObject<string> NoOfNamedPassengers
        {
            get
            {
                return _noOfNamedPassengers;
            }
            set
            {
                _noOfNamedPassengers = value;
                RaisePropertyChanged(() => NoOfNamedPassengers);
            }
        }

        public ValidatableObject<string> SumOfNamedPassengers
        {
            get
            {
                return _sumOfNamedPassengers;
            }
            set
            {
                _sumOfNamedPassengers = value;
                RaisePropertyChanged(() => SumOfNamedPassengers);
            }
        }

        public ValidatableObject<string> NoOfUnNamedPassengers
        {
            get
            {
                return _noOfUnNamedPassengers;
            }
            set
            {
                _noOfUnNamedPassengers = value;
                RaisePropertyChanged(() => NoOfUnNamedPassengers);
            }
        }

        public ValidatableObject<string> SumOfUnNamedPassengers
        {
            get
            {
                return _sumOfUnNamedPassengers;
            }
            set
            {
                _sumOfUnNamedPassengers = value;
                RaisePropertyChanged(() => SumOfUnNamedPassengers);
            }
        }

        public ValidatableObject<string> VoluntaryExcess
        {
            get
            {
                return _voluntaryExcess;
            }
            set
            {
                _voluntaryExcess = value;
                RaisePropertyChanged(() => VoluntaryExcess);
            }
        }

        //Non-mandatory bindable attributes
        public string PaymentMethod
        {
            get
            {
                return _paymentMethod;
            }
            set
            {
                _paymentMethod = value;
                RaisePropertyChanged(() => PaymentMethod);
            }
        }
        public string CoverStartDate
        {
            get
            {
                return _coverStartDate;
            }
            set
            {
                _coverStartDate = value;
                RaisePropertyChanged(() => CoverStartDate);
            }
        }
        public string QuoteType
        {
            get
            {
                return _quoteType;
            }
            set
            {
                _quoteType = value;
                RaisePropertyChanged(() => QuoteType);
            }
        }


        public string CoverType
        {
            get
            {
                return _coverType;
            }
            set
            {
                _coverType = value;
                RaisePropertyChanged(() => CoverType);
            }
        }
        public bool IsExistingPolicyWithUs
        {
            get
            {
                return _isExistingPolicyWithUs;
            }
            set
            {
                _isExistingPolicyWithUs = value;
                RaisePropertyChanged(() => IsExistingPolicyWithUs);
            }
        }

        public bool IsNCB
        {
            get
            {
                return _isNCB;
            }
            set
            {
                _isNCB = value;
                RaisePropertyChanged(() => IsNCB);
            }
        }

        public string NCBDuration
        {
            get
            {
                return _nCBDuration;
            }
            set
            {
                _nCBDuration = value;
                RaisePropertyChanged(() => NCBDuration);
            }
        }

        public bool IsNCBProtected
        {
            get
            {
                return _isNCBProtected;
            }
            set
            {
                _isNCBProtected = value;
                RaisePropertyChanged(() => IsNCBProtected);
            }
        }

        public bool IsWillNCBProtect
        {
            get
            {
                return _isWillNCBProtect;
            }
            set
            {
                _isWillNCBProtect = value;
                RaisePropertyChanged(() => IsWillNCBProtect);
            }
        }

        public bool IsAddBreakdownCover
        {
            get
            {
                return _isAddBreakdownCover;
            }
            set
            {
                _isAddBreakdownCover = value;
                RaisePropertyChanged(() => IsAddBreakdownCover);
            }
        }

        public bool IsAddElectricalCover
        {
            get
            {
                return _isAddElectricalCover;
            }
            set
            {
                _isAddElectricalCover = value;
                RaisePropertyChanged(() => IsAddElectricalCover);
            }
        }

        public bool IsAddNonElectricalCover
        {
            get
            {
                return _isAddNonElectricalCover;
            }
            set
            {
                _isAddNonElectricalCover = value;
                RaisePropertyChanged(() => IsAddNonElectricalCover);
            }
        }

        public bool IsCNGFitmentCover
        {
            get
            {
                return _isCNGFitmentCover;
            }
            set
            {
                _isCNGFitmentCover = value;
                RaisePropertyChanged(() => IsCNGFitmentCover);
            }
        }

        public bool IsPersonalAccidentCover
        {
            get
            {
                return _isPersonalAccidentCover;
            }
            set
            {
                _isPersonalAccidentCover = value;
                RaisePropertyChanged(() => IsPersonalAccidentCover);
            }
        }

        public string PolicyNumber
        {
            get
            {
                return _policyNumber;
            }
            set
            {
                _policyNumber = value;
                RaisePropertyChanged(() => PolicyNumber);
            }
        }

        public string PolicyStartDate
        {
            get
            {
                return _policyStartDate;
            }
            set
            {
                _policyStartDate = value;
                RaisePropertyChanged(() => PolicyStartDate);
            }
        }

        public string PolicyEndDate
        {
            get
            {
                return _policyEndDate;
            }
            set
            {
                _policyEndDate = value;
                RaisePropertyChanged(() => PolicyEndDate);
            }
        }

        public string ExistingCoverType
        {
            get
            {
                return _existingCoverType;
            }
            set
            {
                _existingCoverType = value;
                RaisePropertyChanged(() => ExistingCoverType);
            }
        }



        public string ElectricalAccessoriesValue
        {
            get
            {
                return _electricalAccessoriesValue;
            }
            set
            {
                _electricalAccessoriesValue = value;
                RaisePropertyChanged(() => ElectricalAccessoriesValue);
            }
        }
        public string NonElectricalAccessoriesValue
        {
            get
            {
                return _nonelectricalAccessoriesValue;
            }
            set
            {
                _nonelectricalAccessoriesValue = value;
                RaisePropertyChanged(() => NonElectricalAccessoriesValue);
            }
        }
        public string CNGFitmentValue
        {
            get
            {
                return _CNGFitmentValue;
            }
            set
            {
                _CNGFitmentValue = value;
                RaisePropertyChanged(() => CNGFitmentValue);
            }
        }
        public bool IsPACoverPaidDrivers
        {
            get
            {
                return _isPACoverPaidDrivers;
            }
            set
            {
                _isPACoverPaidDrivers = value;
                RaisePropertyChanged(() => IsPACoverPaidDrivers);
            }
        }

        public bool IsPACoverNamedPassengers
        {
            get
            {
                return _isPACoverNamedPassengers;
            }
            set
            {
                _isPACoverNamedPassengers = value;
                RaisePropertyChanged(() => IsPACoverNamedPassengers);
            }
        }
        public bool IsPACoverUnnamedPassengers
        {
            get
            {
                return _isPACoverUnnamedPassengers;
            }
            set
            {
                _isPACoverUnnamedPassengers = value;
                RaisePropertyChanged(() => IsPACoverUnnamedPassengers);
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


        //pickerData source Attributes
        private ObservableCollection<bool> _multiselect;
        public ObservableCollection<bool> Multiselect
        {
            get 
            { 
                return _multiselect; 
            }
            set
            {
                _multiselect = value;
                RaisePropertyChanged(() => Multiselect);
            }
        }

        private ObservableCollection<string> _paymentMethods;
        public ObservableCollection<string> PaymentMethods
        {
            get { return _paymentMethods; }
            set
            {
                _paymentMethods = value;
                RaisePropertyChanged(() => PaymentMethods);
            }
        }

        private ObservableCollection<string> _quoteTypes;
        public ObservableCollection<string> QuoteTypes
        {
            get { return _quoteTypes; }
            set
            {
                _quoteTypes = value;
                RaisePropertyChanged(() => QuoteTypes);
            }
        }

        private ObservableCollection<string> _coverTypes;
        public ObservableCollection<string> CoverTypes
        {
            get { return _coverTypes; }
            set
            {
                _coverTypes = value;
                RaisePropertyChanged(() => CoverTypes);
            }
        }

        private ObservableCollection<string> _isExistingPoliciesWithUs;
        public ObservableCollection<string> IsExistingPoliciesWithUs
        {
            get { return _isExistingPoliciesWithUs; }
            set
            {
                _isExistingPoliciesWithUs = value;
                RaisePropertyChanged(() => IsExistingPoliciesWithUs);
            }
        }

        private ObservableCollection<string> _existingCoverTypes;
        public ObservableCollection<string> ExistingCoverTypes
        {
            get { return _existingCoverTypes; }
            set
            {
                _existingCoverTypes = value;
                RaisePropertyChanged(() => ExistingCoverTypes);
            }
        }

        private ObservableCollection<string> _carUsages;
        public ObservableCollection<string> CarUsages
        {
            get { return _carUsages; }
            set
            {
                _carUsages = value;
                RaisePropertyChanged(() => CarUsages);
            }
        }

        private ObservableCollection<string> _dayTimeCarParks;
        public ObservableCollection<string> DayTimeCarParks
        {
            get { return _dayTimeCarParks; }
            set
            {
                _dayTimeCarParks = value;
                RaisePropertyChanged(() => DayTimeCarParks);
            }
        }

        private ObservableCollection<string> _nightTimeCarParks;
        public ObservableCollection<string> NightTimeCarParks
        {
            get { return _nightTimeCarParks; }
            set
            {
                _nightTimeCarParks = value;
                RaisePropertyChanged(() => NightTimeCarParks);
            }
        }

        private ObservableCollection<string> _nCBDurations;
        public ObservableCollection<string> NCBDurations
        {
            get { return _nCBDurations; }
            set
            {
                _nCBDurations = value;
                RaisePropertyChanged(() => NCBDurations);
            }
        }

        private ObservableCollection<string> _isAnotherVehicles;
        public ObservableCollection<string> IsAnotherVehicles
        {
            get { return _isAnotherVehicles; }
            set
            {
                _isAnotherVehicles = value;
                RaisePropertyChanged(() => IsAnotherVehicles);
            }
        }

        private ObservableCollection<string> _noOfPaidPicker;
        public ObservableCollection<string> NoOfPaidPicker
        {
            get { return _noOfPaidPicker; }
            set
            {
                _noOfPaidPicker = value;
                RaisePropertyChanged(() => NoOfPaidPicker);
            }
        }

        private ObservableCollection<string> _voluntaryExcessPays;
        public ObservableCollection<string> VoluntaryExcessPays
        {
            get { return _voluntaryExcessPays; }
            set
            {
                _voluntaryExcessPays = value;
                RaisePropertyChanged(() => VoluntaryExcessPays);
            }
        }

        private ObservableCollection<string> _yesOrNo;
        public ObservableCollection<string> YesOrNo
        {
            get { return _yesOrNo; }
            set
            {
                _yesOrNo = value;
                RaisePropertyChanged(() => YesOrNo);
            }
        }

        private void InitializePickerItemsSource()
        {
            Multiselect = new ObservableCollection<bool>()
            {
                true,false
            };

            PaymentMethods = new ObservableCollection<string>()
            {
                "Annually","Monthly"
            };

            QuoteTypes = new ObservableCollection<string>()
            {
                "Renewal of Existing Policy","New Insurance Policy"
            };

            CoverTypes = new ObservableCollection<string>()
            {
                "Third Party only","Own damage","Fire and theft","Comprehensive"
            };

            IsExistingPoliciesWithUs = new ObservableCollection<string>()
            {
                "Yes","No,other provider"
            };

            ExistingCoverTypes = new ObservableCollection<string>()
            {
                "Third Party only","Own damage","Fire and Theft","Comprehensive"
            };

            CarUsages = new ObservableCollection<string>()
            {
                "Social only","Social and Commuting","Business Use by policy holder","Business Use by policy holder and spouse",
                "Business Use for all drivers","Business Use by spouse","Commercial travelling"
            };

            DayTimeCarParks = new ObservableCollection<string>()
            {
                "At home","Office factory car park","Secure public car park","Open public car park","Street away from home"
            };

            NightTimeCarParks = new ObservableCollection<string>()
            {
                "At home","Office factory car park","Secure public car park","Open public car park","Street away from home"
            };

            NCBDurations = new ObservableCollection<string>()
            {
                "Less than a year","1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","20+ year"
            };

            IsAnotherVehicles = new ObservableCollection<string>()
            {
                "No","Yes-Named driver on another car","Yes-Own or use another car","Yes-Own or use a van",
                "Yes-Own or use a motorcycle","Yes-Company car excluding personal use","Yes-Company car including personal use"
            };
            VoluntaryExcessPays = new ObservableCollection<string>()
            {
                "$0","$50","$100","$150","$200","$250","$300","$350","$400","$450","$500","$750","$1000"
            };

            YesOrNo = new ObservableCollection<string>()
            {
                "Yes","No"
            };

            NoOfPaidPicker = new ObservableCollection<string>(){
                "1","2","3","4","5"
            };
        }

        //save command
        public ICommand SaveCommand => new Command(async () => await SaveAsync());

        private async Task SaveAsync()
        {
            bool isValid = Validate();

            if (isValid)
            {
                var postData = GetCarAdditionalInformationPostData();
                //DialogService.ShowLoader();
                ShowIndicator();
                var response = await _saveAndContinueServive.SaveAndContinueAsync(postData, ServicesHelper.GetEndpointToSaveForm("CarAdditionalInformation"));
                //DialogService.HideLoader();
                HideIndicator();
                if (response.Status.ToUpper() == "DATA SAVED SUCCESSFULLY")
                {
                    //navigate to next page
                    await currentPage.Navigation.PushAsync(new PoliciesListView());
                }
                else
                {
                    //await DialogService.ShowAlertAsync(response.Status, "Alert", "Ok");
                    await currentPage.DisplayAlert("Alert", response.Status, "Ok");
                }
            }
            else
            {
                //await DialogService.ShowAlertAsync("Please fill all mandatory fields", "Alert", "Ok");
                await currentPage.DisplayAlert("Alert", "Please fill all mandatory fields", "Ok");
            }
        }

        public ICommand SaveAndContinueLaterCommand => new Command(async () => await SaveAndContinueLaterAsync());

        //save and continue later
        private async Task SaveAndContinueLaterAsync()
        {
            if (isGuestUser)
            {
                await currentPage.DisplayAlert("Alert", "kindly login to enable this feature", "Ok");
            }
            else{
                bool isValid = Validate();
                if (isValid)
                {
                    var postData = GetCarAdditionalInformationPostData();
                    //DialogService.ShowLoader();
                    ShowIndicator();
                    var response = await _saveAndContinueServive.SaveAndContinueAsync(postData, ServicesHelper.GetEndpointToSaveForm("CarAdditionalInformation"));
                    //DialogService.HideLoader();
                    HideIndicator();
                    if (response.Status.ToUpper() == "DATA SAVED SUCCESSFULLY")
                    {
                        TriggerEmailModel model = new TriggerEmailModel();

                        if (Application.Current.Properties.ContainsKey("EmailId"))
                        {
                            model.MailTo = (string)Application.Current.Properties["EmailId"];
                        }
                        model.QuoteId = quoteID;
                        var triggerEmailPostData = ServicesHelper.GetTriggerEmailPostData(model);
                        //DialogService.ShowLoader();
                        ShowIndicator();
                        var triggerMailResponse = await _saveAndContinueServive.PostDataAsync<EmailModel>(GlobalSetting.Instance.SendMailEndPoint, triggerEmailPostData);
                        //DialogService.HideLoader();
                        HideIndicator();

                        if (triggerMailResponse.Status == "Mail sent successfully")
                        {
                            var result= await currentPage.DisplayAlert("Alert", "Resume link successfully sent to your Email ID,return to Dashboard?", "Ok", "Cancel");
                            if(result){
                                OnConfirmDialogActionAsync();
                            }
                        }
                        else
                        {
                            //await DialogService.ShowAlertAsync(triggerMailResponse.Status, "Alert", "Ok");
                            await currentPage.DisplayAlert("Alert", triggerMailResponse.Status, "Ok");
                        }
                        //await DialogService.ShowConfirmAsync(this, "Form Saved Successfully, return to Dashboard?", "Ok", "Cancel");
                        //await currentPage.DisplayAlert("Alert", "Form Saved Successfully, return to Dashboard?", "Ok", "Cancel");
                    }
                    else
                    {
                        //await DialogService.ShowAlertAsync(response.Status, "Alert", "Ok");
                        await currentPage.DisplayAlert("Alert", response.Status, "Ok");
                    }
                }
            }

        }

        public async void OnConfirmDialogActionAsync()
        {
            await currentPage.Navigation.PopToRootAsync();
        }

        public ICommand PreviousCommand => new Command(async () => await PreviousCommandAction());

        private async Task PreviousCommandAction()
        {
            await currentPage.Navigation.PopAsync();
        }

        public ICommand QuoteTypePickerCommand => new Command(() => QuoteTypePickerAction());

        private void QuoteTypePickerAction()
        {
            if (QuoteType == "Renewal of Existing Policy")
            {
                QuoteTypePickerVisibility = true;
            }
            else
            {
                QuoteTypePickerVisibility = false;
            }
        }

        public ICommand IsNCBCommand => new Command(() => IsNCBAction());

        private void IsNCBAction()
        {
            if (IsNCB == true)
            {
                IsNCBVisibility = true;
            }
            else
            {
                IsNCBVisibility = false;
            }
        }

        public ICommand IsWillNCBProtectCommand => new Command(() => IsWillNCBProtectAction());

        private void IsWillNCBProtectAction()
        {
            if (IsNCBProtected == false)
            {
                IsWillNCBVisibility = true;
            }
            else
            {
                IsWillNCBVisibility = false;
            }
        }

        public ICommand IsAddElectricalCoverCommand => new Command(() => IsAddElectricalCoverAction());

        private void IsAddElectricalCoverAction()
        {
            if (IsAddElectricalCover == true)
            {
                IsAddElectricalCoverVisibility = true;
            }
            else
            {
                IsAddElectricalCoverVisibility = false;
            }
        }

        public ICommand IsAddNonElectricalCoverCommand => new Command(() => IsAddNonElectricalCoverAction());

        private void IsAddNonElectricalCoverAction()
        {
            if (IsAddNonElectricalCover == true)
            {
                IsAddNonElectricalCoverVisibility = true;
            }
            else
            {
                IsAddNonElectricalCoverVisibility = false;
            }
        }

        public ICommand IsCNGFitmentCommand => new Command(() => IsCNGFitmentAction());

        private void IsCNGFitmentAction()
        {
            if (IsCNGFitmentCover == true)
            {
                IsCNGFitmentCoverVisibility = true;
            }
            else
            {
                IsCNGFitmentCoverVisibility = false;
            }
        }

        public ICommand IsPACoverPaidCommand => new Command(() => IsPACoverPaidAction());

        private void IsPACoverPaidAction()
        {
            if (IsPACoverPaidDrivers == true)
            {
                IsPACoverPaidVisibility = true;
            }
            else
            {
                IsPACoverPaidVisibility = false;
            }
        }

        public ICommand IsPACoverNamedCommand => new Command(() => IsPACoverNamedAction());

        private void IsPACoverNamedAction()
        {
            if (IsPACoverNamedPassengers == true)
            {
                IsPANamedVisibility = true;
            }
            else
            {
                IsPANamedVisibility = false;
            }
        }

        public ICommand IsPACoverUnNamedCommand => new Command(() => IsPACoverUnNamedAction());

        private void IsPACoverUnNamedAction()
        {
            if (IsPACoverUnnamedPassengers == true)
            {
                IsPAUnNamedVisibility = true;
            }
            else
            {
                IsPAUnNamedVisibility = false;
            }
        }

        //GetCarAdditionalInformation Post Data
        private string GetCarAdditionalInformationPostData()
        {
            
            CarAdditionalInfoModel carAdditionalInfoModel = new CarAdditionalInfoModel();
            if (isGuestUser)
            {
                carAdditionalInfoModel.UserId = "00000000-0000-0000-0000-000000000000";
            }
            else
            {
                carAdditionalInfoModel.UserId = currentUserId;
            }

            carAdditionalInfoModel.QuoteId = quoteID;
            carAdditionalInfoModel.AddBreakdownCoverage = IsAddBreakdownCover;
            carAdditionalInfoModel.AddCNGFitment = IsCNGFitmentCover;
            carAdditionalInfoModel.AddElectricalAccessories = IsAddElectricalCover;
            carAdditionalInfoModel.AddNonElectricalAccessories = IsAddNonElectricalCover;
            carAdditionalInfoModel.AddNamedPassenger = IsPACoverNamedPassengers;
            carAdditionalInfoModel.AddPaidDriverCover = IsPACoverPaidDrivers;
            carAdditionalInfoModel.AddPersonalAccidentCover = IsPersonalAccidentCover;
            carAdditionalInfoModel.AddUnNamedPassenger = IsPACoverUnnamedPassengers;
            carAdditionalInfoModel.AnotherVechile = IsAnotherVehicle.Value;
            carAdditionalInfoModel.CarUsage = CarUsedFor.Value;
            carAdditionalInfoModel.CoverStartDate = CoverStartDate;
            carAdditionalInfoModel.CoverType = CoverType;
            carAdditionalInfoModel.DayTimeParking = DayTimeCarPark.Value;
            carAdditionalInfoModel.ExcessPay = VoluntaryExcess.Value;
            carAdditionalInfoModel.HasNCB = IsNCB;
            carAdditionalInfoModel.HasProtectedNCB = IsNCBProtected;
            carAdditionalInfoModel.NeedToProtectNCB = IsWillNCBProtect;
            carAdditionalInfoModel.ExistingCoverType = ExistingCoverType;
            carAdditionalInfoModel.Mileage = TotalAnnualMileage.Value;
            carAdditionalInfoModel.NCBDuration = NCBDuration;
            carAdditionalInfoModel.NightTimeParking = NightTimeCarPark.Value;
            carAdditionalInfoModel.NumberOfDrivers = NoOfPaid.Value;
            carAdditionalInfoModel.NumberOfNamedPassenger = NoOfNamedPassengers.Value;
            carAdditionalInfoModel.NumberofUnNamedPassenger = NoOfUnNamedPassengers.Value;
            carAdditionalInfoModel.PaymentMethod = PaymentMethod;
            if(QuoteType=="Renewal of Existing Policy")
            {
                carAdditionalInfoModel.PolicyEndDate = PolicyEndDate;
                carAdditionalInfoModel.PolicyStartDate = PolicyStartDate;
            }
            else
            {
                carAdditionalInfoModel.PolicyEndDate = null;
                carAdditionalInfoModel.PolicyStartDate = null;
            }
            carAdditionalInfoModel.PolicyNumber = PolicyNumber;
            carAdditionalInfoModel.QuoteType = QuoteType;
            carAdditionalInfoModel.SumNamedPassenger = SumOfNamedPassengers.Value;
            carAdditionalInfoModel.SumUnNamedPassenger = SumOfUnNamedPassengers.Value;
            carAdditionalInfoModel.SumPaidDriver = SumOfPaid.Value;
            carAdditionalInfoModel.ValueCNGFitment = CNGFitmentValue;
            carAdditionalInfoModel.ValueElectricalAccessories = ElectricalAccessoriesValue;
            carAdditionalInfoModel.ValueNonElectricalAccessories = NonElectricalAccessoriesValue;
            carAdditionalInfoModel.HasExistingPolicyUs = IsExistingPolicyWithUs;

            return ServicesHelper.GetAdditionalInfoPostData(carAdditionalInfoModel);
            //return ServicesHelper.GetAdditionalInfoPostData(ServicesHelper.GetServiceKeysForID("CarAdditionalInformation"), capturedValues);
        }

        //populate personal form with data
        private void PopulateFormWithData(CarAdditionalInfoModel carAdditionalInfoModel)
        {
            if(!(String.IsNullOrEmpty(carAdditionalInfoModel.CarUsage))){
                quoteID = carAdditionalInfoModel.QuoteId;
                IsAddBreakdownCover = carAdditionalInfoModel.AddBreakdownCoverage;
                IsCNGFitmentCover = carAdditionalInfoModel.AddCNGFitment;
                IsAddElectricalCover = carAdditionalInfoModel.AddElectricalAccessories;
                IsPACoverNamedPassengers = carAdditionalInfoModel.AddNamedPassenger;
                IsAddNonElectricalCover = carAdditionalInfoModel.AddNonElectricalAccessories;
                IsPACoverPaidDrivers = carAdditionalInfoModel.AddPaidDriverCover;
                IsPersonalAccidentCover = carAdditionalInfoModel.AddPersonalAccidentCover;
                IsPACoverUnnamedPassengers = carAdditionalInfoModel.AddUnNamedPassenger;
                //quoteID = carAdditionalInfoModel.AdditionalInformationId;
                IsAnotherVehicle.Value = carAdditionalInfoModel.AnotherVechile;
                CarUsedFor.Value = carAdditionalInfoModel.CarUsage;
                //CoverStartDate = carAdditionalInfoModel.CoverStartDate;
                CoverType = carAdditionalInfoModel.CoverType;
                DayTimeCarPark.Value = carAdditionalInfoModel.DayTimeParking;
                VoluntaryExcess.Value = carAdditionalInfoModel.ExcessPay;
                IsExistingPolicyWithUs = (bool)carAdditionalInfoModel.HasExistingPolicyUs;
                IsNCB = carAdditionalInfoModel.HasNCB;
                if (carAdditionalInfoModel.HasProtectedNCB != null)
                {
                    IsNCBProtected = (bool)carAdditionalInfoModel.HasProtectedNCB;
                }
                if (carAdditionalInfoModel.NeedToProtectNCB != null)
                {
                    IsWillNCBProtect = (bool)carAdditionalInfoModel.NeedToProtectNCB;
                }
                if (carAdditionalInfoModel.ExistingCoverType != null)
                {
                    ExistingCoverType = (string)carAdditionalInfoModel.ExistingCoverType;
                }
                TotalAnnualMileage.Value = carAdditionalInfoModel.Mileage;
                NCBDuration = carAdditionalInfoModel.NCBDuration;
                NightTimeCarPark.Value = carAdditionalInfoModel.NightTimeParking;
                if (carAdditionalInfoModel.NumberOfDrivers != null)
                {
                    NoOfPaid.Value = (string)carAdditionalInfoModel.NumberOfDrivers;
                }
                if (carAdditionalInfoModel.NumberOfNamedPassenger != null)
                {
                    NoOfNamedPassengers.Value = (string)carAdditionalInfoModel.NumberOfNamedPassenger;
                }

                PaymentMethod = carAdditionalInfoModel.PaymentMethod;
                if (carAdditionalInfoModel.PolicyEndDate != null)
                {
                    //PolicyEndDate = (string)carAdditionalInfoModel.PolicyEndDate;
                }
                if (carAdditionalInfoModel.PolicyNumber != null)
                {
                    PolicyNumber = (string)carAdditionalInfoModel.PolicyNumber;
                }
                if (carAdditionalInfoModel.PolicyStartDate != null)
                {
                    //PolicyStartDate = (string)carAdditionalInfoModel.PolicyStartDate;
                }

                QuoteType = carAdditionalInfoModel.QuoteType;
                if (carAdditionalInfoModel.SumNamedPassenger != null)
                {
                    SumOfNamedPassengers.Value = (string)carAdditionalInfoModel.SumNamedPassenger;
                }
                if (carAdditionalInfoModel.SumUnNamedPassenger != null)
                {
                    SumOfUnNamedPassengers.Value = (string)carAdditionalInfoModel.SumUnNamedPassenger;
                }
                if (carAdditionalInfoModel.ValueCNGFitment != null)
                {
                    CNGFitmentValue = (string)carAdditionalInfoModel.ValueCNGFitment;
                }
                if (carAdditionalInfoModel.ValueElectricalAccessories != null)
                {
                    ElectricalAccessoriesValue = (string)carAdditionalInfoModel.ValueElectricalAccessories;
                }
                if (carAdditionalInfoModel.ValueNonElectricalAccessories != null)
                {
                    NonElectricalAccessoriesValue = (string)carAdditionalInfoModel.ValueNonElectricalAccessories;
                }
            }

        }

        //validate
        private bool Validate()
        {
            return _carUsedFor.Validate() && _dayTimeCarPark.Validate() && _nightTimeCarPark.Validate()&& _totalAnnualMileage.Validate()
                              && _isAnotherVehicle.Validate() && ValidateForPaidDrivers() && ValidateForNamedPassengers() && ValidateForUnnamedPassengers();
        }

        private bool ValidateForPaidDrivers()
        {
            if(IsPACoverPaidDrivers==true)
            {
                return _noOfPaid.Validate() && _sumOfPaid.Validate();

            }
            else
            {
                return true;
            }
        }

        private bool ValidateForNamedPassengers()
        {
            if(IsPACoverNamedPassengers==true)
            {
                return _sumOfNamedPassengers.Validate() && _noOfNamedPassengers.Validate();
            }
            else
            {
                return true;
            }
        }

        private bool ValidateForUnnamedPassengers()
        {
            if(IsPACoverUnnamedPassengers==true)
            {
                return _noOfUnNamedPassengers.Validate() && _sumOfUnNamedPassengers.Validate();
            }
            else
            {
                return true;
            }
        }

        //add validationsPaP
        private void AddValidations()
        {
            _carUsedFor.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select the Car Usage" });
            _dayTimeCarPark.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select the DayTime Car Park" });
            _nightTimeCarPark.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select the OverNight Car Park" });
            _isAnotherVehicle.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please select the policy holder's ownership or usage of another vehicle" });
            _voluntaryExcess.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter the number of un-named passenger(s) you want to add" });
            _noOfPaid.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please select the number of paid drivers you want to add" });
            _sumOfPaid.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter the sum for paid driver(s) you want to add" });
            _noOfNamedPassengers.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please select the named passengers you want to add" });
            _sumOfNamedPassengers.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter the sum for named passengers you want to add" });
            _noOfUnNamedPassengers.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter the sum for named passengers you want to add" });
            _sumOfUnNamedPassengers.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter the sum for named passengers you want to add" });
            _totalAnnualMileage.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter the total annual mileage" });

        }
    }
}