using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
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
    public class CarFormVinViewModel : ViewModelBase,IConfirmDelegate
    {
        Page currentPage = ACIANavigation.GetCurrentPage();

        private bool _isValid; 
        private string quoteID;
        private bool isGuestUser = false;
        private string currentUserId = string.Empty;
        private string continueLaterQuoteID = string.Empty;

        private ValidatableObject<string> _vin;
        private ValidatableObject<string> _primaryuse;
        private ValidatableObject<string> _primaryZipLocation;
        private ValidatableObject<string> _ownlease;
        private ValidatableObject<string> _securityalarm;

        private ISaveAndContinueService _saveAndContinueServive;

        public CarFormVinViewModel(ISaveAndContinueService saveAndContinueServive)
        {
            _saveAndContinueServive = saveAndContinueServive; 

            InitializePickerItemsSource();

            _vin = new ValidatableObject<string>();
            _primaryuse = new ValidatableObject<string>();
            _ownlease = new ValidatableObject<string>();
            _primaryZipLocation = new ValidatableObject<string>();
            _securityalarm = new ValidatableObject<string>();

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
                var uri = GlobalSetting.Instance.GetCarVINModelDetailsEndPoint + "{" + quote_id + "}";
                var response = await _saveAndContinueServive.GetDataAsync<List<CarVINModel>>(uri);
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
                await currentPage.DisplayAlert("Alert", ex.Message, "OK");
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

        public ValidatableObject<string> VIN
        {
            get
            {

                return _vin;
            }
            set
            {
                _vin = value;
                RaisePropertyChanged(() => VIN);

            }
        }

        public ValidatableObject<string> PrimaryUse
        {
            get

            {

                return _primaryuse;
            }
            set

            {
                _primaryuse = value;
                RaisePropertyChanged(() => PrimaryUse);

            }
        }

        public ValidatableObject<string> PrimaryZipLocation
        {
            get

            {
                return _primaryZipLocation;
            }
            set

            {
                _primaryZipLocation = value;
                RaisePropertyChanged(() => PrimaryZipLocation);

            }
        }

        public ValidatableObject<string> OwnLease
        {
            get

            {
                return _ownlease;
            }
            set

            {
                _ownlease = value;
                RaisePropertyChanged(() => OwnLease);

            }
        }

        public ValidatableObject<string> SecurityAlarm
        {
            get

            {
                return _securityalarm;
            }
            set

            {
                _securityalarm = value;
                RaisePropertyChanged(() => SecurityAlarm);

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

        private ObservableCollection<string> _primaryuses;
        public ObservableCollection<string> PrimaryUses
        {
            get { return _primaryuses; }
            set
            {
                _primaryuses = value;
                RaisePropertyChanged(() => PrimaryUses);
            }
        }

        private ObservableCollection<string> _ownleases;
        public ObservableCollection<string> OwnLeases
        {
            get { return _ownleases; }
            set
            {
                _ownleases = value;
                RaisePropertyChanged(() => OwnLeases);
            }
        }

        private ObservableCollection<string> _securityalarms;
        public ObservableCollection<string> SecurityAlarms
        {
            get { return _securityalarms; }
            set
            {
                _securityalarms = value;
                RaisePropertyChanged(() => SecurityAlarms);
            }
        }

        private void InitializePickerItemsSource()
        {
            PrimaryUses = new ObservableCollection<string>()
            {
                "Personal", "pleasure", "Business", "Farming"
            };
            OwnLeases = new ObservableCollection<string>()
            {
                "Own and Make Payments", "Own and do not Make Payments", "Lease"
            };
            SecurityAlarms = new ObservableCollection<string>()
            {
                "None", "Alarm", "Recovery Device", "VIN Etching", "Alarm/Recovery Device", "Alarm/VIN Etching", "Recovery Device/VIN etching", "Alarm/VIN Etching/Recovery Device"
            };
        }

        public ICommand SaveCommand => new Command(async () => await SaveAsync());

        private async Task SaveAsync()
        {
            bool isValid = Validate();

            if (isValid)
            {
                var postData = GetCarVINModelDetailsPostData();
                //DialogService.ShowLoader();
                ShowIndicator();
                var response = await _saveAndContinueServive.SaveAndContinueAsync(postData, ServicesHelper.GetEndpointToSaveForm("CarVINModel"));
                //DialogService.HideLoader();
                HideIndicator();
                if (response.Status.ToUpper() == "DATA SAVED SUCCESSFULLY")
                {
                    //Application.Current.Properties["VINCarMake"] = "Rolls-Royce";
                    if (Application.Current.Properties.ContainsKey("FirstName"))
                    {
                        var firstname=(string)Application.Current.Properties["FirstName"];
                        if(firstname==string.Empty){
                            await currentPage.Navigation.PushAsync(new DriverAddFormView());
                        }else{
                            await currentPage.Navigation.PushAsync(new DriverDetailsFormView());
                        }
                    }
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
                await currentPage.DisplayAlert("Alert", "Please fill all mandatory fields", "OK");
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
                    var postData = GetCarVINModelDetailsPostData();
                    //DialogService.ShowLoader();
                    ShowIndicator();
                    var response = await _saveAndContinueServive.SaveAndContinueAsync(postData, ServicesHelper.GetEndpointToSaveForm("CarVINModel"));
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
                            //await DialogService.ShowConfirmAsync(this, "Resume link successfully sent to your Email ID, return to Dashboard?", "Ok", "Cancel");
                            var result = await currentPage.DisplayAlert("Alert", "Resume link successfully sent to your Email ID,return to Dashboard?", "Ok", "Cancel");
                            if (result)
                            {
                                OnConfirmDialogActionAsync();
                            }
                        }
                        else
                        {
                           // await DialogService.ShowAlertAsync(triggerMailResponse.Status, "Alert", "Ok");
                            await currentPage.DisplayAlert("Alert", triggerMailResponse.Status, "Ok");
                        }
                        //await DialogService.ShowConfirmAsync(this, "Form Saved Successfully, return to Dashboard?", "Ok", "Cancel");
                        //await currentPage.DisplayAlert("Alert", "Form Saved Successfully, return to Dashboard?", "OK", "Cancel");
                    }
                    else
                    {
                        //await DialogService.ShowAlertAsync(response.Status, "Alert", "Ok");
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

        //public ICommand IsModifiedCarCommand => new Command(() => IsModifiedCarAction());

        //SaveCarVINModelDetails
        private string GetCarVINModelDetailsPostData()
        {
            List<CarVINModel> capturedValues = new List<CarVINModel>();
            CarVINModel carVINModel = new CarVINModel();

            //add all collected values from form
            if (isGuestUser)
            {
                carVINModel.UserId = "00000000-0000-0000-0000-000000000000";
            }
            else
            {
                carVINModel.UserId = currentUserId;
            }
            carVINModel.QuoteId = quoteID;
            carVINModel.VINNumber = VIN.Value;
            carVINModel.PrimaryUse = PrimaryUse.Value;
            carVINModel.ZipCode = PrimaryZipLocation.Value;
            carVINModel.OwnLease = OwnLease.Value;
            carVINModel.SecurityAlarm = SecurityAlarm.Value;

            capturedValues.Add(carVINModel);
            return ServicesHelper.GetCarVINModelDetailsPostData(capturedValues);
        }

        private void PopulateFormWithData(CarVINModel carVINModel)
        {
            if(!(String.IsNullOrEmpty(carVINModel.VINNumber)))
            {
                //quoteID = carVINModel.QuoteId;
                VIN.Value = carVINModel.VINNumber;
                PrimaryUse.Value = carVINModel.PrimaryUse;
                PrimaryZipLocation.Value = carVINModel.ZipCode;
                OwnLease.Value = carVINModel.OwnLease;
                SecurityAlarm.Value = carVINModel.SecurityAlarm;

            }
        }

        //validate
        private bool Validate()
        {
            return _vin.Validate() && _primaryuse.Validate() && _primaryZipLocation.Validate() && _ownlease.Validate() && _securityalarm.Validate();
        }

        //add validations
        private void AddValidations()
        {
            _vin.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter VIN number" });
            _primaryuse.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Primary Use of Car" });
            _primaryZipLocation.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Zip Location" });
            _ownlease.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select to make payments" });
            _securityalarm.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Security/Alarm" });

        }

    }
}

