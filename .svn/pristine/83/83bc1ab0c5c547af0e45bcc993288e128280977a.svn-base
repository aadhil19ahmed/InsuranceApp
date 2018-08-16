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
    public class CarFormMMYViewModel : ViewModelBase,IConfirmDelegate
    {
        Page currentPage = ACIANavigation.GetCurrentPage();

        private bool _isValid;
        private string quoteID;
        private bool isGuestUser = false;
        private string currentUserId = string.Empty;
        private string continueLaterQuoteID = string.Empty;

        //Validatable Fields
        private ValidatableObject<string> _registrationNumber;
        private ValidatableObject<string> _chassisNumber;
        private ValidatableObject<string> _registrationDate;
        private ValidatableObject<string> _engineNumber;
        private ValidatableObject<string> _make;
        private ValidatableObject<string> _model;
        private ValidatableObject<string> _year;
        private ValidatableObject<string> _body;
        private ValidatableObject<string> _driver;
        private ValidatableObject<string> _fuel;
        private ValidatableObject<string> _ownershipType;
        private ValidatableObject<string> _legalOwner;
        private ValidatableObject<string> _legalOwnerName;
        private ValidatableObject<string> _registeredKeeper;
        private ValidatableObject<string> _registeredKeeperName;

        //Non - Mandatory Fields
        private string _safety;
        private string _security;
        private bool _isModified;
        private string _accessories;
        private string _bodyModifications;
        private string _brakes;
        private string _engine;
        private string _paintwork;
        private string _spoilers;
        private string _wheels;
        private bool _policyInName;

        //control visibility
        private bool _isModifiedVisibility;
        private bool _isRegisteredKeeperVisibility;
        private bool _isLegalOwnerVisibility;
        private bool _isPolicyNameVisibility;

        private ISaveAndContinueService _saveAndContinueServive;

        public CarFormMMYViewModel(ISaveAndContinueService saveAndContinueServive)
        {
            _saveAndContinueServive = saveAndContinueServive;

            IsModifiedVisibility = false;
            IsRegisteredKeeperVisibility = false;
            IsLegalOwnerVisibility = false;
            IsPolicyNameVisibility = false;

            InitializePickerItemsSource();
            //Initialize Validatable Fields
            _registrationNumber = new ValidatableObject<string>();
            _chassisNumber = new ValidatableObject<string>();
            _registrationDate = new ValidatableObject<string>();
            _engineNumber = new ValidatableObject<string>();
            _make = new ValidatableObject<string>();
            _model = new ValidatableObject<string>();
            _year = new ValidatableObject<string>();
            _body = new ValidatableObject<string>();
            _driver = new ValidatableObject<string>();
            _fuel = new ValidatableObject<string>();
            _ownershipType = new ValidatableObject<string>();
            _legalOwner = new ValidatableObject<string>();
            _legalOwnerName = new ValidatableObject<string>();
            _registeredKeeper = new ValidatableObject<string>();
            _registeredKeeperName = new ValidatableObject<string>();

            IsModified = false;
            PolicyInName = false;

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
                var uri = GlobalSetting.Instance.GetCarMakeModelDetailsEndPoint + "{" + quote_id + "}";
                var response = await _saveAndContinueServive.GetDataAsync<List<CarMakeModel>>(uri);
                //DialogService.HideLoader();
                if (response != null && response.Count > 0)
                {
                    PopulateFormWithData(response[0]);
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
        public bool IsModifiedVisibility
        {
            get
            {
                return _isModifiedVisibility;
            }
            set
            {
                _isModifiedVisibility = value;
                RaisePropertyChanged(() => IsModifiedVisibility);
            }
        }

        public bool IsRegisteredKeeperVisibility
        {
            get
            {
                return _isRegisteredKeeperVisibility;
            }
            set
            {
                _isRegisteredKeeperVisibility = value;
                RaisePropertyChanged(() => IsRegisteredKeeperVisibility);
            }
        }

        public bool IsLegalOwnerVisibility
        {
            get
            {
                return _isLegalOwnerVisibility;
            }
            set
            {
                _isLegalOwnerVisibility = value;
                RaisePropertyChanged(() => IsLegalOwnerVisibility);
            }
        }

        public bool IsPolicyNameVisibility
        {
            get
            {
                return _isPolicyNameVisibility;
            }
            set
            {
                _isPolicyNameVisibility = value;
                RaisePropertyChanged(() => IsPolicyNameVisibility);
            }
        }

        //Validatable Bindable Attributes

        public ValidatableObject<string> RegistrationNumber
        {
            get
            {

                return _registrationNumber;
            }
            set
            {

                _registrationNumber = value;
                RaisePropertyChanged(() => RegistrationNumber);
            }
        }

        public ValidatableObject<string> ChassisNumber
        {
            get
            {

                return _chassisNumber;
            }
            set
            {

                _chassisNumber = value;
                RaisePropertyChanged(() => ChassisNumber);
            }
        }

        public ValidatableObject<string> RegistrationDate
        {
            get
            {

                return _registrationDate;
            }
            set
            {

                _registrationDate = value;
                RaisePropertyChanged(() => RegistrationDate);

            }
        }

        public ValidatableObject<string> EngineNumber
        {
            get
            {

                return _engineNumber;
            }
            set
            {

                _engineNumber = value;
                RaisePropertyChanged(() => EngineNumber);

            }
        }

        public ValidatableObject<string> Make
        {
            get
            {

                return _make;
            }
            set
            {

                _make = value;
                RaisePropertyChanged(() => Make);

            }
        }

        public ValidatableObject<string> Model
        {
            get
            {

                return _model;
            }
            set
            {

                _model = value;
                RaisePropertyChanged(() => Model);

            }
        }

        public ValidatableObject<string> Year
        {
            get
            {

                return _year;
            }
            set
            {

                _year = value;
                RaisePropertyChanged(() => Year);

            }
        }

        public ValidatableObject<string> Body
        {
            get
            {

                return _body;
            }
            set
            {

                _body = value;
                RaisePropertyChanged(() => Body);

            }
        }

        public ValidatableObject<string> Driver
        {
            get
            {

                return _driver;
            }
            set
            {

                _driver = value;
                RaisePropertyChanged(() => Driver);

            }
        }
        public ValidatableObject<string> Fuel
        {
            get
            {

                return _fuel;
            }
            set
            {

                _fuel = value;
                RaisePropertyChanged(() => Fuel);

            }
        }



        public ValidatableObject<string> OwnershipType
        {
            get
            {

                return _ownershipType;
            }
            set
            {

                _ownershipType = value;
                RaisePropertyChanged(() => OwnershipType);

            }
        }

        public ValidatableObject<string> LegalOwner
        {
            get
            {

                return _legalOwner;
            }
            set
            {

                _legalOwner = value;
                RaisePropertyChanged(() => LegalOwner);

            }
        }

        public ValidatableObject<string> LegalOwnerName
        {
            get
            {

                return _legalOwnerName;
            }
            set
            {

                _legalOwnerName = value;
                RaisePropertyChanged(() => LegalOwnerName);

            }
        }

        public ValidatableObject<string> RegisteredKeeper
        {
            get
            {

                return _registeredKeeper;
            }
            set
            {

                _registeredKeeper = value;
                RaisePropertyChanged(() => RegisteredKeeper);

            }
        }

        public ValidatableObject<string> RegisteredKeeperName
        {
            get
            {

                return _registeredKeeperName;
            }
            set
            {

                _registeredKeeperName = value;
                RaisePropertyChanged(() => RegisteredKeeperName);

            }
        }

        //Non-Validatable Bindable Attributes

        public string Safety
        {
            get
            {

                return _safety;
            }
            set
            {

                _safety = value;
                RaisePropertyChanged(() => Safety);
            }
        }

        public string Security
        {
            get
            {

                return _security;
            }
            set
            {

                _security = value;
                RaisePropertyChanged(() => Security);
            }
        }

        public bool IsModified
        {
            get
            {

                return _isModified;
            }
            set
            {

                _isModified = value;
                RaisePropertyChanged(() => IsModified);

            }
        }
        public string Accessories
        {
            get
            {

                return _accessories;
            }
            set
            {

                _accessories = value;
                RaisePropertyChanged(() => Accessories);
            }
        }

        public string BodyModifications
        {
            get
            {

                return _bodyModifications;
            }
            set
            {

                _bodyModifications = value;
                RaisePropertyChanged(() => BodyModifications);
            }
        }

        public string Brakes
        {
            get
            {

                return _brakes;
            }
            set
            {

                _brakes = value;
                RaisePropertyChanged(() => Brakes);
            }
        }

        public string Engine
        {
            get
            {

                return _engine;
            }
            set
            {

                _engine = value;
                RaisePropertyChanged(() => Engine);
            }
        }


        public string Paintwork
        {
            get
            {

                return _paintwork;
            }
            set
            {

                _paintwork = value;
                RaisePropertyChanged(() => Paintwork);
            }
        }

        public string Spoilers
        {
            get
            {

                return _spoilers;
            }
            set
            {

                _spoilers = value;
                RaisePropertyChanged(() => Spoilers);
            }
        }

        public string Wheels
        {
            get
            {

                return _wheels;
            }
            set
            {

                _wheels = value;
                RaisePropertyChanged(() => Wheels);
            }
        }

        public bool PolicyInName
        {
            get
            {
                return _policyInName;
            }
            set
            {

                _policyInName = value;
                RaisePropertyChanged(() => PolicyInName);

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


        //pickerdatasource attributes

        private ObservableCollection<string> _makes;
        public ObservableCollection<string> Makes
        {
            get { return _makes; }
            set
            {
                _makes = value;
                RaisePropertyChanged(() => Makes);
            }
        }

        private ObservableCollection<string> _models;
        public ObservableCollection<string> Models
        {
            get { return _models; }
            set
            {
                _models = value;
                RaisePropertyChanged(() => Models);
            }
        }
        private ObservableCollection<string> _years;
        public ObservableCollection<string> Years
        {
            get { return _years; }
            set
            {
                _years = value;
                RaisePropertyChanged(() => Years);
            }
        }

        private ObservableCollection<string> _bodys;
        public ObservableCollection<string> Bodys
        {
            get { return _bodys; }
            set
            {
                _bodys = value;
                RaisePropertyChanged(() => Bodys);
            }
        }

        private ObservableCollection<string> _drivers;
        public ObservableCollection<string> Drivers
        {
            get { return _drivers; }
            set
            {
                _drivers = value;
                RaisePropertyChanged(() => Drivers);
            }
        }

        private ObservableCollection<string> _fuels;
        public ObservableCollection<string> Fuels
        {
            get { return _fuels; }
            set
            {
                _fuels = value;
                RaisePropertyChanged(() => Fuels);
            }
        }

        private ObservableCollection<string> _securitys;
        public ObservableCollection<string> Securitys
        {
            get { return _securitys; }
            set
            {
                _securitys = value;
                RaisePropertyChanged(() => Securitys);
            }
        }

        private ObservableCollection<string> _safetys;
        public ObservableCollection<string> Safetys
        {
            get { return _safetys; }
            set
            {
                _safetys = value;
                RaisePropertyChanged(() => Safetys);
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

        private ObservableCollection<string> _ownershipTypes;
        public ObservableCollection<string> OwnershipTypes
        {
            get { return _ownershipTypes; }
            set
            {
                _ownershipTypes = value;
                RaisePropertyChanged(() => OwnershipTypes);
            }
        }

        private ObservableCollection<string> _legalOwnerTypes;
        public ObservableCollection<string> LegalOwnerTypes
        {
            get { return _legalOwnerTypes; }
            set
            {
                _legalOwnerTypes = value;
                RaisePropertyChanged(() => LegalOwnerTypes);
            }
        }

        private ObservableCollection<string> _registeredKeepertypes;
        public ObservableCollection<string> RegisteredKeepertypes
        {
            get { return _registeredKeepertypes; }
            set
            {
                _registeredKeepertypes = value;
                RaisePropertyChanged(() => RegisteredKeepertypes);
            }
        }

        private ObservableCollection<string> _accessoriesTypes;
        public ObservableCollection<string> AccessoriesTypes
        {
            get { return _accessoriesTypes; }
            set
            {
                _accessoriesTypes = value;
                RaisePropertyChanged(() => AccessoriesTypes);
            }
        }

        private ObservableCollection<string> _bodyModificationTypes;
        public ObservableCollection<string> BodyModificationTypes
        {
            get { return _bodyModificationTypes; }
            set
            {
                _bodyModificationTypes = value;
                RaisePropertyChanged(() => BodyModificationTypes);
            }
        }

        private ObservableCollection<string> _brakeTypes;
        public ObservableCollection<string> BrakeTypes
        {
            get { return _brakeTypes; }
            set
            {
                _brakeTypes = value;
                RaisePropertyChanged(() => BrakeTypes);
            }
        }

        private ObservableCollection<string> _engineTypes;
        public ObservableCollection<string> EngineTypes
        {
            get { return _engineTypes; }
            set
            {
                _engineTypes = value;
                RaisePropertyChanged(() => EngineTypes);
            }
        }

        private ObservableCollection<string> _paintworks;
        public ObservableCollection<string> Paintworks
        {
            get { return _paintworks; }
            set
            {
                _paintworks = value;
                RaisePropertyChanged(() => Paintworks);
            }
        }

        private ObservableCollection<string> _spoilerTypes;
        public ObservableCollection<string> SpoilerTypes
        {
            get { return _spoilerTypes; }
            set
            {
                _spoilerTypes = value;
                RaisePropertyChanged(() => SpoilerTypes);
            }
        }

        private ObservableCollection<string> _wheelTypes;
        public ObservableCollection<string> WheelTypes
        {
            get { return _wheelTypes; }
            set
            {
                _wheelTypes = value;
                RaisePropertyChanged(() => WheelTypes);
            }
        }


        private void InitializePickerItemsSource()
        {
            Makes = new ObservableCollection<string>()
            {
                "Aston Martin", "Cadillac", "Fiat", "Ford", "General Motors", "Jaguar", "Land Rover", "Mercedes Benz", "Porsche", "Renault", "Rolls - Royce", "Volkswagen"
            };

            Models = new ObservableCollection<string>()
            {
                "Ghost", "Ghost EWB", "Phantom Sedan", "Phantom EWB", "Phantom Couple Convertible", "wraith", "Discovery3", "Discovery3 Diesel", "Porsche", "Renault", "Rolls - Royce", "Volkswagen"
            };

            Years = new ObservableCollection<string>()
            {
                "2016","2015","2014","2013","2012","2011","2010","2009","2008"
            };

            Bodys = new ObservableCollection<string>()
            {
                "Hatchback", "Sedan", "MUV/SUV", "Coupe", "Convertible", "Wagon", "Van"
            };

            Drivers = new ObservableCollection<string>()
            {
                "Left Hand", "Right Hand"
            };

            Fuels = new ObservableCollection<string>()
            {
                "Petrol", "Diesel", "Electric", "Natural Gas", "Hybrid", "Hydrogen Fuel Cell", "BioDesel"
            };

            Safetys = new ObservableCollection<string>()
            {
                "Auto Emergency Braking", "Anti Lock Braking System", "Blind Spot Monitoring", "Curtain Airbags", "Hill Start Assistance"
            };

            Securitys = new ObservableCollection<string>()
            {
                "Factory fitted Immobilizer", "Factory fitted Alarm", "Other Approved Immobilizer", "Other non Approved Immobilizer", "Vehicle Tracker", "Hill Start Assistance"
            };

            YesOrNo = new ObservableCollection<string>()
            {
                "Yes","No"
            };

            OwnershipTypes = new ObservableCollection<string>()
            {
                "LegalOwner And Registered Keeper","Legal Owner","Registered Keeper"
            };

            LegalOwnerTypes = new ObservableCollection<string>()
            {
                "Spouse","Named Driver","Parent","Commonlawpartner","CivilPartner","Private leased","Company","Company(leased)"
            };

            RegisteredKeepertypes = new ObservableCollection<string>()
            {
                "Spouse","Named Driver","Child","Commonlawpartner","CivilPartner"
            };

            AccessoriesTypes = new ObservableCollection<string>()
            {
                "Interior Changes","Rally/Spotlights","Electronic Central Locking"
            };

            BodyModificationTypes = new ObservableCollection<string>()
            {
                "Bullbars","Exterior Decorative Changes","Modifications due to disability"
            };

            BrakeTypes = new ObservableCollection<string>(){
                "Brakes(uprated)","Suspension lowered(<5cm)","Suspension lowered(5-12.5cm)"
            };

            EngineTypes = new ObservableCollection<string>()
            {
                "Engine Bored-out","Engine(Standard replacement)","Engine(Non-standard replacement)"
            };

            Paintworks = new ObservableCollection<string>(){
                "Body coloured bumpers","Respray(different colour)"
            };

            SpoilerTypes = new ObservableCollection<string>(){
                "Bonnet bulge/grill","Side skirts/sills","Spoiler(Rear)"
            };

            WheelTypes = new ObservableCollection<string>(){
                "Alloy wheels(Standard,Optional)","Alloy wheels(Non-standard)"
            };
        }

        public ICommand SaveCommand => new Command(async () => await SaveAsync());

        private async Task SaveAsync()
        {
            bool isValid = Validate();

            if (isValid)
            {
                var postData = GetCarMakeModelDetailsPostData();
                //DialogService.ShowLoader();
                ShowIndicator();
                var response = await _saveAndContinueServive.SaveAndContinueAsync(postData, ServicesHelper.GetEndpointToSaveForm("CarMakeModel"));
                //DialogService.HideLoader();
                HideIndicator();
                if (response.Status.ToUpper() == "DATA SAVED SUCCESSFULLY")
                {
                    //navigate to next page
                    Application.Current.Properties["MMYCarMake"] = Make.Value;
                    if (Application.Current.Properties.ContainsKey("FirstName"))
                    {
                        var firstname = (string)Application.Current.Properties["FirstName"];
                        if (firstname==string.Empty)
                        {
                            await currentPage.Navigation.PushAsync(new DriverAddFormView());
                        }
                        else
                        {
                            await currentPage.Navigation.PushAsync(new DriverDetailsFormView());
                        }
                    }
                }
                else
                {
                    //await DialogService.ShowAlertAsync(response.Status, "Alert", "Ok");
                    await currentPage.DisplayAlert("Alert",response.Status,"Ok");
                }
            }
            else
            {
                //await DialogService.ShowAlertAsync("Please fill all mandatory fields", "Alert", "Ok");
                await currentPage.DisplayAlert("Alert","Please fill all mandatory fields", "Ok");
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
                string userName = string.Empty;
                if (isValid)
                {
                    var postData = GetCarMakeModelDetailsPostData();
                    //DialogService.ShowLoader();
                    ShowIndicator();
                    var response = await _saveAndContinueServive.SaveAndContinueAsync(postData, ServicesHelper.GetEndpointToSaveForm("CarMakeModel"));
                    //DialogService.HideLoader();
                    HideIndicator();
                    if (response.Status.ToUpper() == "DATA SAVED SUCCESSFULLY")
                    {
                        TriggerEmailModel model = new TriggerEmailModel();
                        if (Application.Current.Properties.ContainsKey("UserName"))
                        {
                            userName = (string)Application.Current.Properties["UserName"];
                        }
                        if(Application.Current.Properties.ContainsKey("EmailId")){
                            model.MailTo = (string)Application.Current.Properties["EmailId"];
                        }
                        model.QuoteId = quoteID;
                        var triggerEmailPostData = ServicesHelper.GetTriggerEmailPostData(model);
                        ShowIndicator();
                        var triggerMailResponse = await _saveAndContinueServive.PostDataAsync<EmailModel>(GlobalSetting.Instance.SendMailEndPoint, triggerEmailPostData);
                        HideIndicator();

                        if (triggerMailResponse.Status == "Mail sent successfully")
                        {
                            //await DialogService.ShowConfirmAsync(this, "Resume link successfully sent to your Email ID, return to Dashboard?", "Ok", "Cancel");
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
                        // currentPage.DisplayAlert("Alert", "Form Saved Successfully, return to Dashboard?", "OK", "Cancel");
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


        public ICommand IsModifiedCarCommand => new Command(() => IsModifiedCarAction());

        private void IsModifiedCarAction()
        {
            if (IsModified == true)
            {
                IsModifiedVisibility = true;
            }
            else
            {
                IsModifiedVisibility = false;
            }
        }

        public ICommand OwnershipPickerCommand => new Command(() => OwnershipPickerAction());

        private void OwnershipPickerAction()
        {
            if (OwnershipType.Value=="Legal Owner")
            {
                IsPolicyNameVisibility = true;
                IsLegalOwnerVisibility = true;
            }
            else
            {
                IsLegalOwnerVisibility = false;
                IsPolicyNameVisibility = false;
            }
            if(OwnershipType.Value=="Registered Keeper"){
                IsPolicyNameVisibility = true;
                IsRegisteredKeeperVisibility = true;
            }else{
                IsRegisteredKeeperVisibility = false;
                IsPolicyNameVisibility = false;
            }
        }

        //SaveCarMakeModelDetails
        private string GetCarMakeModelDetailsPostData()
        {
            List<CarMakeModel> capturedValues = new List<CarMakeModel>();
            CarMakeModel carMakeModel = new CarMakeModel();

            //add all collected values from form
            if (isGuestUser)
            {
                carMakeModel.UserId = "00000000-0000-0000-0000-000000000000";
            }
            else
            {
                carMakeModel.UserId = currentUserId;
            }
            carMakeModel.QuoteId = quoteID;
            carMakeModel.RegistrationNumber = RegistrationNumber.Value;
            carMakeModel.RegistrationDate = RegistrationDate.Value;
            carMakeModel.ChassisNumber = ChassisNumber.Value;
            carMakeModel.EngineNumber = EngineNumber.Value;
            carMakeModel.ManufactureName = Make.Value;
            carMakeModel.ModelName = Model.Value;
            carMakeModel.YearofManufacture = Year.Value;
            carMakeModel.BodyType = Body.Value;
            carMakeModel.DriverType = Driver.Value;
            carMakeModel.FuelType = Fuel.Value;
            carMakeModel.HasModification = IsModified.ToString();
            carMakeModel.OwnershipCar = OwnershipType.Value;
            carMakeModel.SafetyFeatures = Safety;
            carMakeModel.SecurityFeatures = Security;
            carMakeModel.Accessories = Accessories;
            carMakeModel.BodyModifications = BodyModifications;
            carMakeModel.Brakes = Brakes;
            carMakeModel.EngineOrTransmission = Engine;
            carMakeModel.Paintwork = Paintwork;
            carMakeModel.Spoilers = Spoilers;
            carMakeModel.Wheels = Wheels;
            carMakeModel.NeedPolicyInName = PolicyInName;

            capturedValues.Add(carMakeModel);
            return ServicesHelper.GetCarMakeModelDetailsPostData(capturedValues);
        }

        private void PopulateFormWithData(CarMakeModel carMakeModel)
        {
            if(!(String.IsNullOrEmpty(carMakeModel.RegistrationNumber)))
            {
                //quoteID = saveCarPersonalModel.QuoteId;
            RegistrationNumber.Value = carMakeModel.RegistrationNumber;
            RegistrationDate.Value = carMakeModel.RegistrationDate;
            ChassisNumber.Value = carMakeModel.ChassisNumber;
            EngineNumber.Value = carMakeModel.EngineNumber;
            Make.Value = carMakeModel.ManufactureName;
            Model.Value = carMakeModel.ModelName;
            Year.Value = carMakeModel.YearofManufacture;
            Body.Value = carMakeModel.BodyType;
            Driver.Value = carMakeModel.DriverType;
            Fuel.Value = carMakeModel.FuelType;
            OwnershipType.Value = carMakeModel.OwnershipCar;
            if(carMakeModel.SafetyFeatures!=null){
                Safety = (string)carMakeModel.SafetyFeatures;
            }
            if (carMakeModel.SecurityFeatures != null)
            {
                Security = (string)carMakeModel.SecurityFeatures;
            }
            if (carMakeModel.Accessories != null)
            {
                Accessories = (string)carMakeModel.Accessories;
            }
            if (carMakeModel.BodyModifications != null)
            {
                BodyModifications = (string)carMakeModel.BodyModifications;
            }
            if (carMakeModel.Brakes != null)
            {
                Brakes = (string)carMakeModel.Brakes;

            }
            if (carMakeModel.EngineOrTransmission != null)
            {
                Engine = (string)carMakeModel.EngineOrTransmission;

            }
            if (carMakeModel.Paintwork != null)
            {
                Paintwork =(string) carMakeModel.Paintwork;

            }
            if (carMakeModel.Spoilers != null)
            {
                Spoilers = (string)carMakeModel.Spoilers;

            }
            if (carMakeModel.Wheels != null)
            {
                Wheels = (string)carMakeModel.Wheels;

            }
            if (carMakeModel.NeedPolicyInName != null)
            {
                PolicyInName = (bool)carMakeModel.NeedPolicyInName;

            }
            //convert string to bool
            StringToBoolConverter converter = new StringToBoolConverter();
            IsModified=(bool)converter.Convert(carMakeModel.HasModification,null,IsModified,null);
     
            }
        }

        //validate
        private bool Validate()
        {
            return ValidateLegalOwnerFields() && ValidateRegKeeperFields() && ValidateNonDependentFields();
        }

        private bool ValidateLegalOwnerFields(){
            if(OwnershipType.Value=="Legal Owner"){
                return _legalOwner.Validate() && _legalOwnerName.Validate();
            }else{
                return true;
            }
        }

        private bool ValidateRegKeeperFields()
        {
            if (OwnershipType.Value == "Registered Keeper")
            {
                return _registeredKeeper.Validate() && _registeredKeeperName.Validate();
            }
            else
            {
                return true;
            }
        }

        private bool ValidateNonDependentFields()
        {
            return _registrationNumber.Validate() && _chassisNumber.Validate() && _registrationDate.Validate() && _engineNumber.Validate()
                                    && _make.Validate() && _model.Validate() && _year.Validate() && _body.Validate() && _driver.Validate()
                                      && _fuel.Validate();
        }

        //add validations
        private void AddValidations()
        {
            _registrationNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Registration No." });
            _chassisNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Chassis No." });
            _registrationDate.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Registration Date." });
            _engineNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Engine No." });
            _make.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Make" });
            _model.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Model" });
            _year.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Year" });
            _body.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Body" });
            _driver.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Driver Hand type" });
            _fuel.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Fuel type" });
            _ownershipType.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Ownership type" });
            _legalOwner.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Legal Owner" });
            _legalOwnerName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Legal Owner Name" });
            _registeredKeeper.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Registered Keeper" });
            _registeredKeeperName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Registered Keeper Name" });

        }

    }
}
