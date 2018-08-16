using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using ACIA.Converters;
using ACIA.Helper;
using ACIA.Model;
using ACIA.Services.Dialog;
using ACIA.Services.SaveAndContinueService;
using ACIA.Validations;
using ACIA.ViewModels.Base;
using ACIA.Views.UILayer;
using ACIA.Views;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using ACIA.Helper.Prompt;
using ACIA.Services;

namespace ACIA.ViewModels
{
    public class DriverAddFormViewModel : ViewModelBase,IConfirmDelegate
    {
        Page currentPage = ACIANavigation.GetCurrentPage();

        private bool _isValid;
        private string quoteID;
        private bool isGuestUser = false;
        private string currentUserId = string.Empty;
        private string continueLaterQuoteID = string.Empty;

        //Validatable fields
        private ValidatableObject<string> _title;
        private ValidatableObject<string> _firstName;
        private ValidatableObject<string> _middleInitial;
        private ValidatableObject<string> _lastName;
        private ValidatableObject<string> _gender;
        private ValidatableObject<string> _dateOfBirth;
        private ValidatableObject<string> _martialStatus;
        private ValidatableObject<string> _mobileNumber;
        private ValidatableObject<string> _emailAddress;
        private ValidatableObject<string> _zipcode;
        private ValidatableObject<string> _address;
        private ValidatableObject<string> _citizenship;
        private ValidatableObject<string> _citizenshipStartDate;
        private ValidatableObject<string> _licenseType;
        private ValidatableObject<string> _licenseDuration;
        private ValidatableObject<string> _drivingLicenseNumber;
        private ValidatableObject<string> _claimDate;
        private ValidatableObject<string> _claimAmount;
        private ValidatableObject<string> _claimType;
        private ValidatableObject<string> _convictionDate;
        private ValidatableObject<string> _convictionType;
        private ValidatableObject<string> _breathalyserReading;

        //non-mandatory fields
        private string _suffix;
        private string _town;
        private string _state;
        private bool _citizenByBirth;
        private bool _isHaveAccidentsOrClaims;
        private bool _isHaveConvictions;
        private bool _isBreathalysed;
        private string _pointsIncurred;
        private string _fineIncurred;
        private string _banLength;
        private bool _isAccident;

        //control visibility
        private bool _isHaveAccidentsOrClaimsVisibility;
        private bool _isHaveConvictionsVisibility;
        private bool _isCitizenByBirthVisibility;


        //save and continue service
        private ISaveAndContinueService _saveAndContinueServive;

        public DriverAddFormViewModel(ISaveAndContinueService saveAndContinueServive)
        {
            
            _saveAndContinueServive = saveAndContinueServive;

            IsHaveAccidentsOrClaimsVisibility = false;
            IsHaveConvictionsVisibility = false;
            IsCitizenByBirthVisibility = false;
            //Initialize Picker Data Source     
            InitializePickerItemsSource();

            _title = new ValidatableObject<string>();
            _firstName = new ValidatableObject<string>();
            _middleInitial = new ValidatableObject<string>();
            _lastName = new ValidatableObject<string>();
            _gender = new ValidatableObject<string>();
            _dateOfBirth = new ValidatableObject<string>();
            _martialStatus = new ValidatableObject<string>();
            _mobileNumber = new ValidatableObject<string>();
            _emailAddress = new ValidatableObject<string>();
            _zipcode = new ValidatableObject<string>();
            _address = new ValidatableObject<string>();
            _citizenship = new ValidatableObject<string>();
            _citizenshipStartDate = new ValidatableObject<string>();
            _licenseType = new ValidatableObject<string>();
            _licenseDuration = new ValidatableObject<string>();
            _drivingLicenseNumber = new ValidatableObject<string>();
            _claimDate = new ValidatableObject<string>();
            _claimAmount = new ValidatableObject<string>();
            _claimType = new ValidatableObject<string>();
            _convictionDate = new ValidatableObject<string>();
            _convictionType = new ValidatableObject<string>();
            _breathalyserReading = new ValidatableObject<string>();

            IsHaveAccidentsOrClaims = false;
            IsHaveConviction = false;
            CitizenByBirth = true;
            IsBreathalysed = true;
            IsAccident = false;

            //add validations
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
                var uri = GlobalSetting.Instance.GetCarDriverDetailsEndPoint + "{" + quote_id + "}";
                var response = await _saveAndContinueServive.GetDataAsync<List<CarDriverModel>>(uri);
                             
                //DialogService.HideLoader();
                if (response != null && response.Count > 0)
                {
                    PopulateFormWithData(response[0]);
                    if(response[0].IsHaveAccidentsOrClaims)
                    {
                        var claimsuri = GlobalSetting.Instance.GetCarClaimDetailsEndPoint + "{" + quote_id + "}";
                        var claimsResponse = await _saveAndContinueServive.GetDataAsync<List<DriverClaimsList>>(uri);
                        if (claimsResponse != null && claimsResponse.Count > 0)
                        {
                            PopulateFormWithClaimsData(claimsResponse[0]);
                        }
                    }

                    if(response[0].IsHaveConviction)
                    {
                        var convictionsuri = GlobalSetting.Instance.GetCarConvictionDetailsEndPoint + "{" + quote_id + "}";
                        var convictionsResponse = await _saveAndContinueServive.GetDataAsync<List<DriverConvictionsList>>(uri);
                        if (convictionsResponse != null && convictionsResponse.Count > 0)
                        {
                            PopulateFormWithConvictionsData(convictionsResponse[0]);
                        }
                    }
                }
               
            }
            catch (HttpRequestException ex)
            {
                //DialogService.HideLoader();
                //await this.DialogService.ShowAlertAsync(ex.Message, "Alert", "Ok");
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
        public bool IsHaveAccidentsOrClaimsVisibility
        {
            get
            {
                return _isHaveAccidentsOrClaimsVisibility;
            }
            set
            {
                _isHaveAccidentsOrClaimsVisibility = value;
                RaisePropertyChanged(() => IsHaveAccidentsOrClaimsVisibility);
            }
        }

        public bool IsHaveConvictionsVisibility
        {
            get
            {
                return _isHaveConvictionsVisibility;
            }
            set
            {
                _isHaveConvictionsVisibility = value;
                RaisePropertyChanged(() => IsHaveConvictionsVisibility);
            }
        }

        public bool IsCitizenByBirthVisibility
        {
            get
            {
                return _isCitizenByBirthVisibility;
            }
            set
            {
                _isCitizenByBirthVisibility = value;
                RaisePropertyChanged(() => IsCitizenByBirthVisibility);
            }
        }

        //Validatable Bindable Attributes
        public ValidatableObject<string> Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public ValidatableObject<string> FirstName
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

        public ValidatableObject<string> MiddleInitial
        {
            get
            {
                return _middleInitial;
            }
            set
            {
                _middleInitial = value;
                RaisePropertyChanged(() => MiddleInitial);
            }
        }

        public ValidatableObject<string> LastName
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

        public ValidatableObject<string> Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                RaisePropertyChanged(() => Gender);
            }
        }

        public ValidatableObject<string> DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                _dateOfBirth = value;
                RaisePropertyChanged(() => DateOfBirth);
            }
        }

        public ValidatableObject<string> MartialStatus
        {
            get
            {
                return _martialStatus;
            }
            set
            {
                _martialStatus = value;
                RaisePropertyChanged(() => MartialStatus);
            }
        }

        public ValidatableObject<string> MobileNumber
        {
            get
            {
                return _mobileNumber;
            }
            set
            {
                _mobileNumber = value;
                RaisePropertyChanged(() => MobileNumber);
            }
        }

        public ValidatableObject<string> EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                _emailAddress = value;
                RaisePropertyChanged(() => EmailAddress);
            }
        }

        public ValidatableObject<string> Zipcode
        {
            get
            {
                return _zipcode;
            }
            set
            {
                _zipcode = value;
                RaisePropertyChanged(() => Zipcode);
            }
        }

        public ValidatableObject<string> Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                RaisePropertyChanged(() => Address);
            }
        }

        public ValidatableObject<string> CitizenShip
        {
            get
            {
                return _citizenship;
            }
            set
            {
                _citizenship = value;
                RaisePropertyChanged(() => CitizenShip);
            }
        }

        public ValidatableObject<string> CitizenshipStartDate
        {
            get
            {
                return _citizenshipStartDate;
            }
            set
            {
                _citizenshipStartDate = value;
                RaisePropertyChanged(() => CitizenshipStartDate);
            }
        }

        public ValidatableObject<string> LicenseType
        {
            get
            {
                return _licenseType;
            }
            set
            {
                _licenseType = value;
                RaisePropertyChanged(() => LicenseType);
            }
        }

        public ValidatableObject<string> LicenseDuration
        {
            get
            {
                return _licenseDuration;
            }
            set
            {
                _licenseDuration = value;
                RaisePropertyChanged(() => LicenseDuration);
            }
        }

        public ValidatableObject<string> DrivingLicenseNumber
        {
            get
            {
                return _drivingLicenseNumber;
            }
            set
            {
                _drivingLicenseNumber = value;
                RaisePropertyChanged(() => DrivingLicenseNumber);
            }
        }       

        public ValidatableObject<string> ClaimDate
        {
            get
            {
                return _claimDate;
            }
            set
            {
                _claimDate = value;
                RaisePropertyChanged(() => ClaimDate);
            }
        }

        public ValidatableObject<string> ClaimAmount
        {
            get
            {
                return _claimAmount;
            }
            set
            {
                _claimAmount = value;
                RaisePropertyChanged(() => ClaimAmount);
            }
        }

        public ValidatableObject<string> ClaimType
        {
            get
            {
                return _claimType;
            }
            set
            {
                _claimType = value;
                RaisePropertyChanged(() => ClaimType);
            }
        }

        public ValidatableObject<string> ConvictionDate
        {
            get
            {
                return _convictionDate;
            }
            set
            {
                _convictionDate = value;
                RaisePropertyChanged(() => ConvictionDate);
            }
        }

        public ValidatableObject<string> ConvictionType
        {
            get
            {
                return _convictionType;
            }
            set
            {
                _convictionType = value;
                RaisePropertyChanged(() => ConvictionType);
            }
        }

        public ValidatableObject<string> BreathalyserReading
        {
            get
            {
                return _breathalyserReading;
            }
            set
            {
                _breathalyserReading = value;
                RaisePropertyChanged(() => BreathalyserReading);
            }
        }

        //Non-mandatory bindable attributes
        public string Suffix
        {
            get
            {
                return _suffix;
            }
            set
            {
                _suffix = value;
                RaisePropertyChanged(() => Suffix);
            }
        }

        public string Town
        {
            get
            {
                return _town;
            }
            set
            {
                _town = value;
                RaisePropertyChanged(() => Town);
            }
        }

        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                RaisePropertyChanged(() => State);
            }
        }

        public bool CitizenByBirth
        {
            get
            {
                return _citizenByBirth;
            }
            set
            {
                _citizenByBirth = value;
                RaisePropertyChanged(() => CitizenByBirth);
            }
        }

        public bool IsHaveAccidentsOrClaims
        {
            get
            {
                return _isHaveAccidentsOrClaims;
            }
            set
            {
                _isHaveAccidentsOrClaims = value;
                RaisePropertyChanged(() => IsHaveAccidentsOrClaims);
            }
        }

        public bool IsHaveConviction
        {
            get
            {
                return _isHaveConvictions;
            }
            set
            {
                _isHaveConvictions = value;
                RaisePropertyChanged(() => IsHaveConviction);
            }
        }

        public bool IsBreathalysed
        {
            get
            {
                return _isBreathalysed;
            }
            set
            {
                _isBreathalysed = value;
                RaisePropertyChanged(() => IsBreathalysed);
            }
        }

        public string PointsIncurred
        {
            get
            {
                return _pointsIncurred;
            }
            set
            {
                _pointsIncurred = value;
                RaisePropertyChanged(() => PointsIncurred);
            }
        }

        public string FineIncurred
        {
            get
            {
                return _fineIncurred;
            }
            set
            {
                _fineIncurred = value;
                RaisePropertyChanged(() => FineIncurred);
            }
        }

        public string BanLength
        {
            get
            {
                return _banLength;
            }
            set
            {
                _banLength = value;
                RaisePropertyChanged(() => BanLength);
            }
        }

        public bool IsAccident
        {
            get
            {
                return _isAccident;
            }
            set
            {
                _isAccident = value;
                RaisePropertyChanged(() => IsAccident);
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

        //pickerdatasource Attributes
        private ObservableCollection<bool> _multiselect;
        public ObservableCollection<bool> Multiselect
        {
            get { return _multiselect; }
            set
            {
                _multiselect = value;
                RaisePropertyChanged(() => Multiselect);
            }
        }

        private ObservableCollection<string> _titles;
        public ObservableCollection<string> Titles
        {
            get { return _titles; }
            set
            {
                _titles = value;
                RaisePropertyChanged(() => Titles);
            }
        }

        private ObservableCollection<string> _suffices;
        public ObservableCollection<string> Suffices
        {
            get { return _suffices; }
            set
            {
                _suffices = value;
                RaisePropertyChanged(() => Suffices);
            }
        }

        private ObservableCollection<string> _genders;
        public ObservableCollection<string> Genders
        {
            get { return _genders; }
            set
            {
                _genders = value;
                RaisePropertyChanged(() => Genders);
            }
        }

        private ObservableCollection<string> _mStatus;
        public ObservableCollection<string> MStatus
        {
            get { return _mStatus; }
            set
            {
                _mStatus = value;
                RaisePropertyChanged(() => MStatus);
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

        private ObservableCollection<string> _licenseTypes;
        public ObservableCollection<string> LicenseTypes
        {
            get { return _licenseTypes; }
            set
            {
                _licenseTypes = value;
                RaisePropertyChanged(() => LicenseTypes);
            }
        }

        private ObservableCollection<string> _licenseDurations;
        public ObservableCollection<string> LicenseDurations
        {
            get { return _licenseDurations; }
            set
            {
                _licenseDurations = value;
                RaisePropertyChanged(() => LicenseDurations);
            }
        }

        private ObservableCollection<string> _claimTypes;
        public ObservableCollection<string> ClaimTypes
        {
            get { return _claimTypes; }
            set
            {
                _claimTypes = value;
                RaisePropertyChanged(() => ClaimTypes);
            }
        }

        private ObservableCollection<string> _convictionTypes;
        public ObservableCollection<string> ConvictionTypes
        {
            get { return _convictionTypes; }
            set
            {
                _convictionTypes = value;
                RaisePropertyChanged(() => ConvictionTypes);
            }
        }

        private void InitializePickerItemsSource()
        {
            Titles = new ObservableCollection<string>()
            {
                "Mr.","Mrs.","Miss."
            };

            Suffices = new ObservableCollection<string>()
            {
                "Jr.","Sr.","I","II","III","IV"
            };

            Genders = new ObservableCollection<string>()
            {
                "Male","Female"
            };

            MStatus = new ObservableCollection<string>()
            {
                "Married","Widowed","Seperated","Divorced","Living with partner","Civil Partnership","Single"
            };

            YesOrNo = new ObservableCollection<string>()
            {
                "Yes","No"
            };

            LicenseTypes = new ObservableCollection<string>()
            {
                "Class A","Class B","Class C","CDL","Class D","Class E","Class MJ","Class DJ"
            };

            LicenseDurations = new ObservableCollection<string>()
            {
                "Less than a year","1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","20+ year"
            };

            ClaimTypes = new ObservableCollection<string>()
            {
                "Accident","Theft","Windscreen glass","Malicious damage","Storm or flood","Fire"
            };

            ConvictionTypes = new ObservableCollection<string>()
            {
                "Pending Conviction","Exceeding statutory speed limit on a public road","Exceeding statutory speed limit on a motorway",
                "Use of a hand-held vehicle while driving","Using a vehicle uninsured against third party risks"
            };

            Multiselect = new ObservableCollection<bool>()
            {
                true,false
            };
        }

        //save command
        public ICommand SaveCommand => new Command(async () => await SaveAsync());

        public ICommand IsHaveAccidentsOrClaimsCommand => new Command(() => IsHaveAccidentsOrClaimsAction());

        private void IsHaveAccidentsOrClaimsAction()
        {
            if (IsHaveAccidentsOrClaims == true)
            {
                IsHaveAccidentsOrClaimsVisibility = true;
            }
            else
            {
                IsHaveAccidentsOrClaimsVisibility = false;
            }
        }

        public ICommand IsHaveConvictionsCommand => new Command(() => IsHaveConvictionsAction());

        private void IsHaveConvictionsAction()
        {
            if (IsHaveConviction == true)
            {
                IsHaveConvictionsVisibility = true;
            }
            else
            {
                IsHaveConvictionsVisibility = false;
            }
        }

        public ICommand IsCitizenByBirthCommand => new Command(() => IsCitizenByBirthAction());

        private void IsCitizenByBirthAction()
        {
            if (CitizenByBirth == false)
            {
                IsCitizenByBirthVisibility = true;
            }
            else
            {
                IsCitizenByBirthVisibility = false;
            }
        }

        private async Task SaveAsync()
        {
            bool isValid = Validate();

            if (isValid)
            {
                var postData = GetDriverAddFormPostData();
                //DialogService.ShowLoader();
                ShowIndicator();
                var response = await _saveAndContinueServive.SaveAndContinueAsync(postData, ServicesHelper.GetEndpointToSaveForm("CarDriverDetails"));
                //DialogService.HideLoader();
                HideIndicator();
                if (response.Status.ToUpper() == "DATA SAVED SUCCESSFULLY")
                {
                   //service call for convictions
                    if(IsHaveConviction==true)
                    {
                        var driverConvictionpostData = GetDriverConvictionPostData();
                        //DialogService.ShowLoader();
                        ShowIndicator();

                        var convictionresponse = await _saveAndContinueServive.SaveAndContinueAsync(driverConvictionpostData, ServicesHelper.GetEndpointToSaveForm("DriverConvictionDetails"));
                        //DialogService.HideLoader();
                        HideIndicator();
                    }

                    //service call for claims
                    if (IsHaveAccidentsOrClaims == true)
                    {
                        var driverClaimPostData = GetDriverClaimPostData();
                        //DialogService.ShowLoader();
                        ShowIndicator();

                        var claimresponse = await _saveAndContinueServive.SaveAndContinueAsync(driverClaimPostData, ServicesHelper.GetEndpointToSaveForm("DriverClaimDetails"));
                        //DialogService.HideLoader();
                        HideIndicator();
                    }

                    //Navigate to Next Page
                    await currentPage.Navigation.PushAsync(new AdditionalFormView());

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
                    //service call for driver addition
                    var postData = GetDriverAddFormPostData();
                    //DialogService.ShowLoader();
                    ShowIndicator();
                    var response = await _saveAndContinueServive.SaveAndContinueAsync(postData, ServicesHelper.GetEndpointToSaveForm("CarDriverDetails"));
                    //DialogService.HideLoader();
                    HideIndicator();
                    if (response.Status.ToUpper() == "DATA SAVED SUCCESSFULLY")
                    {
                        TriggerEmailModel model = new TriggerEmailModel();
                        //service call for convictions
                        if (IsHaveConviction == true)
                        {
                            var driverConvictionpostData = GetDriverConvictionPostData();
                            //DialogService.ShowLoader();
                            ShowIndicator();

                            var convictionresponse = await _saveAndContinueServive.SaveAndContinueAsync(driverConvictionpostData, ServicesHelper.GetEndpointToSaveForm("DriverConvictionDetails"));
                            //DialogService.HideLoader();
                            HideIndicator();
                        }

                        //service call for claims
                        if (IsHaveAccidentsOrClaims == true)
                        {
                            var driverClaimPostData = GetDriverClaimPostData();
                            //DialogService.ShowLoader();
                            ShowIndicator();

                            var claimresponse = await _saveAndContinueServive.SaveAndContinueAsync(driverClaimPostData, ServicesHelper.GetEndpointToSaveForm("DriverClaimDetails"));
                            //DialogService.HideLoader();
                            HideIndicator();
                        }

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
                            //await DialogService.ShowConfirmAsync(this, "Resume link successfully sent to your Email ID,return to Dashboard?, return to Dashboard?", "Ok", "Cancel");
                            var result = await currentPage.DisplayAlert("Alert", "Resume link successfully sent to your Email ID,return to Dashboard?", "Ok", "Cancel");
                            if (result)
                            {
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
                    }else{
                        await currentPage.DisplayAlert("Alert", response.Status, "Cancel");
                    }

                }
                else
                {
                    //await DialogService.ShowAlertAsync("Please fill all mandatory fields", "Alert", "Ok");
                    await currentPage.DisplayAlert("Alert", "Please fill all mandatory fields", "Ok");
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

        //Get post data
        private string GetDriverAddFormPostData()
        {
            List<CarDriverModel> capturedValues = new List<CarDriverModel>();
            CarDriverModel carDriverModel = new CarDriverModel();
            if (isGuestUser)
            {
                carDriverModel.UserId = "00000000-0000-0000-0000-000000000000";
            }
            else
            {
                carDriverModel.UserId = currentUserId;
            }
            carDriverModel.Address = Address.Value;
            carDriverModel.Citizenship = CitizenShip.Value;
            carDriverModel.CitizenByBirth = CitizenByBirth.ToString();
            if(CitizenByBirth==false){
                carDriverModel.CitizenshipStartDate = CitizenshipStartDate.Value;
            }else{
                carDriverModel.CitizenshipStartDate = null;
            }

            carDriverModel.City = Town;
            carDriverModel.DateOfBirth = DateOfBirth.Value;
            carDriverModel.EmailId = EmailAddress.Value;
            carDriverModel.FirstName = FirstName.Value;
            carDriverModel.Gender = Gender.Value;
            carDriverModel.IsHaveAccidentsOrClaims = IsHaveAccidentsOrClaims;
            carDriverModel.IsHaveConviction = IsHaveConviction;
            carDriverModel.LastName = LastName.Value;
            carDriverModel.LicenseDuration = LicenseDuration.Value;
            carDriverModel.LicenseNo = DrivingLicenseNumber.Value;
            carDriverModel.MaritalStatus = MartialStatus.Value;
            carDriverModel.MiddleInitial = MiddleInitial.Value;
            carDriverModel.MobilePhone = MobileNumber.Value;
            carDriverModel.QuoteId = quoteID;
            carDriverModel.State = State;
            carDriverModel.Suffix = Suffix;
            carDriverModel.Title = Title.Value;
            carDriverModel.ZipCode = Zipcode.Value;

            capturedValues.Add(carDriverModel);
            return ServicesHelper.GetSaveCarDriverDetailsPostData(capturedValues);
        }

        private string GetDriverConvictionPostData()
        {
            List<DriverConvictionsList> capturedValues = new List<DriverConvictionsList>();
            if (IsHaveConviction)
            {
                DriverConvictionsList driverConvictionsList = new DriverConvictionsList();
                if (isGuestUser)
                {
                    driverConvictionsList.UserId = "00000000-0000-0000-0000-000000000000";
                }
                else
                {
                    driverConvictionsList.UserId = currentUserId;
                }
                driverConvictionsList.QuoteId = quoteID;
                driverConvictionsList.ConvictionDate = ConvictionDate.Value;
                driverConvictionsList.ConvictionType = ConvictionType.Value;
                driverConvictionsList.PointsIncurred = PointsIncurred;
                driverConvictionsList.FineIncurred = FineIncurred;
                driverConvictionsList.BanLength = BanLength;
                driverConvictionsList.IsBreathalysed = IsBreathalysed;
                driverConvictionsList.BreathalysedReading = BreathalyserReading.Value;
                driverConvictionsList.IsAccident = IsAccident;
                driverConvictionsList.EmailId = EmailAddress.Value;

                capturedValues.Add(driverConvictionsList);
            }

            return ServicesHelper.GetDriverConvictionsDetailsPostData(capturedValues);

        }

        private string GetDriverClaimPostData()
        {
            List<DriverClaimsList> capturedValues = new List<DriverClaimsList>();
            if(IsHaveAccidentsOrClaims)
            {
                DriverClaimsList driverClaimsList = new DriverClaimsList();
                if (isGuestUser)
                {
                    driverClaimsList.UserId = "00000000-0000-0000-0000-000000000000";
                }
                else
                {
                    driverClaimsList.UserId = currentUserId;
                }
                driverClaimsList.QuoteId = quoteID;
                driverClaimsList.ClaimAmount = ClaimAmount.Value;
                driverClaimsList.ClaimDate = ClaimDate.Value;
                driverClaimsList.ClaimType = ClaimType.Value;
                driverClaimsList.EmailId = EmailAddress.Value;

                capturedValues.Add(driverClaimsList);
            }
            return ServicesHelper.GetDriverClaimPostData(capturedValues);
        }

        private void PopulateFormWithData(CarDriverModel carDriverModel)
        {
            if(!(String.IsNullOrEmpty(carDriverModel.FirstName))){
                //quoteID = saveCarPersonalModel.QuoteId;
                Address.Value = carDriverModel.Address;
                CitizenShip.Value = carDriverModel.Citizenship;
                CitizenshipStartDate.Value = (string)carDriverModel.CitizenshipStartDate;
                DateOfBirth.Value = carDriverModel.DateOfBirth;
                EmailAddress.Value = carDriverModel.EmailId;
                FirstName.Value = carDriverModel.FirstName;
                Gender.Value = carDriverModel.Gender;

                LastName.Value = carDriverModel.LastName;
                LicenseDuration.Value = carDriverModel.LicenseDuration;
                DrivingLicenseNumber.Value = carDriverModel.LicenseNo;
                MartialStatus.Value = carDriverModel.MaritalStatus;
                if (carDriverModel.MiddleInitial != null)
                {
                    MiddleInitial.Value = (string)carDriverModel.MiddleInitial;
                }
                MobileNumber.Value = Regex.Replace(carDriverModel.MobilePhone, @"[^0-9a-zA-Z]+", "");
                State = carDriverModel.State;
                if (carDriverModel.Suffix != null)
                {
                    Suffix = (string)carDriverModel.Suffix;
                }
                Title.Value = carDriverModel.Title;
                Zipcode.Value = carDriverModel.ZipCode;

                StringToBoolConverter converter = new StringToBoolConverter();
                CitizenByBirth = (bool)converter.Convert(carDriverModel.CitizenByBirth, null, CitizenByBirth, null);
            }

        }

        private void PopulateFormWithClaimsData(DriverClaimsList driverClaimsList)
        {
            //quoteID = driverClaimsList.QuoteId;
            if(driverClaimsList.ClaimDate!=null)
            {
                ClaimDate.Value = (string)driverClaimsList.ClaimDate;
            }
            if (driverClaimsList.ClaimType != null)
            {
                ClaimType.Value = (string)driverClaimsList.ClaimType;
            }if (driverClaimsList.ClaimAmount != null)
            {
                ClaimAmount.Value = (string)driverClaimsList.ClaimAmount;
            }
        }

        private void PopulateFormWithConvictionsData(DriverConvictionsList driverConvictionsList)
        {
            //quoteID = driverConvictionsList.QuoteId;
            if (driverConvictionsList.ConvictionDate != null)
            {
                ConvictionDate.Value = (string)driverConvictionsList.ConvictionDate;
            }
            if (driverConvictionsList.ConvictionType != null)
            {
                ConvictionType.Value = (string)driverConvictionsList.ConvictionType;
            }
            if (driverConvictionsList.PointsIncurred != null)
            {
                PointsIncurred = (string)driverConvictionsList.PointsIncurred;
            }
            if (driverConvictionsList.FineIncurred != null)
            {
                FineIncurred = (string)driverConvictionsList.FineIncurred;
            }
            if (driverConvictionsList.IsBreathalysed != null)
            {
                IsBreathalysed = (bool)driverConvictionsList.IsBreathalysed;
            }
            if (driverConvictionsList.BanLength != null)
            {
                BanLength = (string)driverConvictionsList.BanLength;
            }
            if (driverConvictionsList.BreathalysedReading != null)
            {
                BreathalyserReading.Value = (string)driverConvictionsList.BreathalysedReading;
            }
            if (driverConvictionsList.IsAccident != null)
            {
                IsAccident = (bool)driverConvictionsList.IsAccident;
            }
        }

        //validate
        private bool Validate()
        {
            return ValidateNonDependentFields() && ValidateConvictions() && ValidateAccidentOrClaims()&&ValidateCitizenByBirth();
        }

        private bool ValidateNonDependentFields()
        {
            return _title.Validate() && _firstName.Validate() && _dateOfBirth.Validate() && _lastName.Validate() && _gender.Validate() && 
                         _martialStatus.Validate() && _mobileNumber.Validate() && _emailAddress.Validate()
                         && _zipcode.Validate() && _address.Validate() && _citizenship.Validate() 
                         && _licenseType.Validate() && _licenseDuration.Validate() && _drivingLicenseNumber.Validate();
        }

        private bool ValidateCitizenByBirth()
        {
            if (CitizenByBirth == true)
            {
                return _citizenshipStartDate.Validate();
            }
            else
            {
                return true;
            }
        }

        private bool ValidateConvictions()
        {
            if (IsHaveConviction == true)
            {
                return _convictionDate.Validate() && _breathalyserReading.Validate() && _convictionType.Validate();
            }
            else
            {
                return true;
            }
        }

        private bool ValidateAccidentOrClaims()
        {
            if (IsHaveAccidentsOrClaims == true)
            {
                return _claimDate.Validate() && _claimAmount.Validate() && _claimType.Validate();
            }
            else
            {
                return true;
            }
        }

        private void AddValidations()
        {
            _title.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Title." });
            _firstName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter the policy holder's first name" });
            _dateOfBirth.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Date of Birth" });
            _lastName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter the policy holder's surname" });
            _gender.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please select a gender" });
            _martialStatus.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select MaritalStatus" });
            _mobileNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Mobile Number" });
            _emailAddress.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Email Address" });
            _zipcode.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Zipcode" });
            _address.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Address" });
            _citizenship.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Your CitizenShip" });
            _citizenshipStartDate.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select CitizenShip Start Date" });
            _licenseType.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select LicenseType" });
            _licenseDuration.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select License Duration" });
            _drivingLicenseNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter License Number" });
            _claimDate.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Claim Date" });
            _claimAmount.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Claim Amount" });
            _claimType.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Claim Type" });
            _convictionDate.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Conviction Date" });
            _convictionType.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Conviction Type" });
            _breathalyserReading.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Breatlyser Reading" });
        }
    }
}
