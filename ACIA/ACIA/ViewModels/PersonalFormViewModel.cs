﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using ACIA.Helper;
using ACIA.Model;
using ACIA.Services.SaveAndContinueService;
using ACIA.Validations;
using ACIA.ViewModels.Base;
using ACIA.Views;
using ACIA.Views.UILayer;
using ACIA.Converters;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using ACIA.Helper.Prompt;
using ACIA.Services;

namespace ACIA.ViewModels
{
    public class PersonalFormViewModel : ViewModelBase, IConfirmDelegate
    {
        Page currentPage = ACIANavigation.GetCurrentPage();

        private bool _isValid;
        private string quoteID;
        private bool isGuestUser = false;
        private string currentUserId = string.Empty;
        private string continueLaterQuoteID = string.Empty;


        //Validatable Fields
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
        private ValidatableObject<string> _occupationType;
        private ValidatableObject<string> _industry;
        private ValidatableObject<string> _jobFunctions;
        private ValidatableObject<string> _licenseType;
        private ValidatableObject<string> _licenseDuration;
        private ValidatableObject<string> _drivingLicenseNumber;
        private ValidatableObject<string> _claimDate;
        private ValidatableObject<string> _claimAmount;
        private ValidatableObject<string> _claimType;
        private ValidatableObject<string> _convictionDate;
        private ValidatableObject<string> _convictionType;
        private ValidatableObject<string> _breathalyserReading;
        private ValidatableObject<string> _nomineeTitle;
        private ValidatableObject<string> _nomineeGender;
        private ValidatableObject<string> _nomineeFirstName;
        private ValidatableObject<string> _nomineeLastName;
        private ValidatableObject<string> _nomineeDOB;
        private ValidatableObject<string> _appointeeName;
        private ValidatableObject<string> _nomineeRelationship;

        //Non - Mandatory Fields
        private string _suffix;
        private string _town;
        private string _state;
        private bool _citizenByBirth;
        private bool _isNamedDriver;
        private bool _isHaveAccidentsOrClaims;
        private bool _isHaveNominee;
        private bool _isHaveConvictions;
        private string _pointsIncurred;
        private string _fineIncurred;
        private bool _isBreathalysed;
        private bool _isAccident;
        private string _banLength;
        private string _isHaveCars;
        private string _socialSecurityNumber;
        private bool _isHomeOwner;
        private bool _isHaveChild;


        //control visibility        
        private bool _isHaveAccidentsOrClaimsVisibility;
        private bool _isNamedDriverVisibility;
        private bool _isHaveConvictionsVisibility;
        private bool _isHaveNomineeVisibility;
        private bool _occupationPickerVisibility;
        private bool _isCitizenByBirthVisibility;
        private bool _isLoggedInUserVisibility;

        private ISaveAndContinueService _saveAndContinueServive;

        public PersonalFormViewModel(ISaveAndContinueService saveAndContinueServive)
        {

            _saveAndContinueServive = saveAndContinueServive;
            IsHaveAccidentsOrClaimsVisibility = false;
            IsNamedDriverVisibility = false;
            IsHaveConvictionsVisibility = false;
            IsHaveNomineeVisibility = false;
            OccupationPickerVisibility = false;
            IsCitizenByBirthVisibility = false;

            //Initialize Picker Data Source     
            InitializePickerItemsSource();

            //Initialize Validatable Fields
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
            _occupationType = new ValidatableObject<string>();
            _industry = new ValidatableObject<string>();
            _jobFunctions = new ValidatableObject<string>();
            _licenseType = new ValidatableObject<string>();
            _licenseDuration = new ValidatableObject<string>();
            _drivingLicenseNumber = new ValidatableObject<string>();
            _claimDate = new ValidatableObject<string>();
            _claimAmount = new ValidatableObject<string>();
            _claimType = new ValidatableObject<string>();
            _convictionDate = new ValidatableObject<string>();
            _convictionType = new ValidatableObject<string>();
            _breathalyserReading = new ValidatableObject<string>();
            _nomineeTitle = new ValidatableObject<string>();
            _nomineeGender = new ValidatableObject<string>();
            _nomineeFirstName = new ValidatableObject<string>();
            _nomineeLastName = new ValidatableObject<string>();
            _nomineeDOB = new ValidatableObject<string>();
            _appointeeName = new ValidatableObject<string>();
            _nomineeRelationship = new ValidatableObject<string>();

            IsHaveAccidentsOrClaims = false;
            IsNamedDriver = false;
            IsHaveConviction = false;
            IsHaveNominee = false;
            IsHaveChild = false;
            IsHomeOwner = false;
            IsBreathalysed = false;
            IsAccident = false;
            CitizenByBirth = true;



            //add validations
            AddValidations();

            if(Application.Current.Properties.ContainsKey("quoteid"))
            {
                quoteID = (string)Application.Current.Properties["quoteid"];
            }

            string currentUser = string.Empty;
            if (Application.Current.Properties.ContainsKey("UserName"))
            {
                currentUser = (string)Application.Current.Properties["UserName"];
            }

            var currentUserAccount = App.CredentialsService.GetValueForKey(currentUser, App.CurrentUser);
            if(currentUserAccount.Properties.ContainsKey(currentUser))
            {
                currentUserId = currentUserAccount.Properties[currentUser];
            }

          
            if (Application.Current.Properties.ContainsKey("IsGuestUser"))
            {
                isGuestUser = (bool)Application.Current.Properties["IsGuestUser"];
            }

            if(!isGuestUser)
            {
                GetListOfQuoteId();
            }
        }

        private async void GetListOfQuoteId()
        {
            var uri = GlobalSetting.Instance.GetQuoteIdListEndPoint + "{" + currentUserId + "}";
            var response = await _saveAndContinueServive.GetDataAsync<ListOfQuoteId>(uri);
            foreach(var id in response.QuoteIdList)
            {
                if(id.QuoteId == quoteID)
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
                var uri = GlobalSetting.Instance.GetCarDetailsEndPoint + "{" + quote_id + "}";
                var response = await _saveAndContinueServive.GetDataAsync<SaveCarPersonalModel>(uri);
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
                await currentPage.DisplayAlert("Alert",ex.Message,"Ok");
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

        public bool IsLoggedInUserVisibility
        {
            get
            {
                return _isLoggedInUserVisibility;
            }
            set
            {
                _isLoggedInUserVisibility = value;
                RaisePropertyChanged(() => IsLoggedInUserVisibility);
            }
        }

        public bool IsNamedDriverVisibility
        {
            get
            {
                return _isNamedDriverVisibility;
            }
            set
            {
                _isNamedDriverVisibility = value;
                RaisePropertyChanged(() => IsNamedDriverVisibility);
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

        public bool IsHaveNomineeVisibility
        {
            get
            {
                return _isHaveNomineeVisibility;
            }
            set
            {
                _isHaveNomineeVisibility = value;
                RaisePropertyChanged(() => IsHaveNomineeVisibility);
            }
        }

        public bool OccupationPickerVisibility
        {
            get
            {
                return _occupationPickerVisibility;
            }
            set
            {
                _occupationPickerVisibility = value;
                RaisePropertyChanged(() => OccupationPickerVisibility);
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

        public ValidatableObject<string> OccupationType
        {
            get
            {
                return _occupationType;
            }
            set
            {
                _occupationType = value;
                RaisePropertyChanged(() => OccupationType);
            }
        }

        public ValidatableObject<string> Industry
        {
            get
            {
                return _industry;
            }
            set
            {
                _industry = value;
                RaisePropertyChanged(() => Industry);
            }
        }

        public ValidatableObject<string> JobFunctions
        {
            get
            {
                return _jobFunctions;
            }
            set
            {
                _jobFunctions = value;
                RaisePropertyChanged(() => JobFunctions);
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


        public ValidatableObject<string> NomineeTitle
        {
            get
            {
                return _nomineeTitle;
            }
            set
            {
                _nomineeTitle = value;
                RaisePropertyChanged(() => NomineeTitle);
            }
        }

        public ValidatableObject<string> NomineeGender
        {
            get
            {
                return _nomineeGender;
            }
            set
            {
                _nomineeGender = value;
                RaisePropertyChanged(() => NomineeGender);
            }
        }

        public ValidatableObject<string> NomineeFirstName
        {
            get
            {
                return _nomineeFirstName;
            }
            set
            {
                _nomineeFirstName = value;
                RaisePropertyChanged(() => NomineeFirstName);
            }
        }

        public ValidatableObject<string> NomineeLastName
        {
            get
            {
                return _nomineeLastName;
            }
            set
            {
                _nomineeLastName = value;
                RaisePropertyChanged(() => NomineeLastName);
            }
        }

        public ValidatableObject<string> NomineeDOB
        {
            get
            {
                return _nomineeDOB;
            }
            set
            {
                _nomineeDOB = value;
                RaisePropertyChanged(() => NomineeDOB);
            }
        }

        public ValidatableObject<string> AppointeeName
        {
            get
            {
                return _appointeeName;
            }
            set
            {
                _appointeeName = value;
                RaisePropertyChanged(() => AppointeeName);
            }
        }

        public ValidatableObject<string> NomineeRelationship
        {
            get
            {
                return _nomineeRelationship;
            }
            set
            {
                _nomineeRelationship = value;
                RaisePropertyChanged(() => NomineeRelationship);
            }
        }



        //Non - Mandatory Bindable Attribites
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

        public bool IsNamedDriver
        {
            get
            {
                return _isNamedDriver;
            }
            set
            {
                _isNamedDriver = value;
                RaisePropertyChanged(() => IsNamedDriver);
            }
        }

        public bool IsHaveNominee
        {
            get
            {
                return _isHaveNominee;
            }
            set
            {
                _isHaveNominee = value;
                RaisePropertyChanged(() => IsHaveNominee);
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

        public string IsHaveCars
        {
            get
            {
                return _isHaveCars;
            }
            set
            {
                _isHaveCars = value;
                RaisePropertyChanged(() => IsHaveCars);
            }
        }

        public string SocialSecurityNumber
        {
            get
            {
                return _socialSecurityNumber;
            }
            set
            {
                _socialSecurityNumber = value;
                RaisePropertyChanged(() => SocialSecurityNumber);
            }
        }

        public bool IsHomeOwner
        {
            get
            {
                return _isHomeOwner;
            }
            set
            {
                _isHomeOwner = value;
                RaisePropertyChanged(() => IsHomeOwner);
            }
        }

        public bool IsHaveChild
        {
            get
            {
                return _isHaveChild;
            }
            set
            {
                _isHaveChild = value;
                RaisePropertyChanged(() => IsHaveChild);
            }
        }

        //method for loader
        public void ShowIndicator()
        {
            switch(Device.RuntimePlatform){
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

        private ObservableCollection<string> _occupationTypes;
        public ObservableCollection<string> OccupationTypes
        {
            get { return _occupationTypes; }
            set
            {
                _occupationTypes = value;
                RaisePropertyChanged(() => OccupationTypes);
            }
        }

        private ObservableCollection<string> _industries;
        public ObservableCollection<string> Industries
        {
            get { return _industries; }
            set
            {
                _industries = value;
                RaisePropertyChanged(() => Industries);
            }
        }

        private ObservableCollection<string> _jobFunction;
        public ObservableCollection<string> JobFunction
        {
            get { return _jobFunction; }
            set
            {
                _jobFunction = value;
                RaisePropertyChanged(() => JobFunction);
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

        private ObservableCollection<string> _nomineeTitles;
        public ObservableCollection<string> NomineeTitles
        {
            get { return _nomineeTitles; }
            set
            {
                _nomineeTitles = value;
                RaisePropertyChanged(() => NomineeTitles);
            }
        }

        private ObservableCollection<string> _relationShips;
        public ObservableCollection<string> RelationShips
        {
            get { return _relationShips; }
            set
            {
                _relationShips = value;
                RaisePropertyChanged(() => RelationShips);
            }
        }

        private void InitializePickerItemsSource()
        {
            Titles = new ObservableCollection<string>()
            {
                "Mr.","Mrs","Miss"
            };

            Suffices = new ObservableCollection<String>()
            {
                "Jr","Sr","I","II","III","IV"
            };

            Genders = new ObservableCollection<string>()
            {
                "Male", "Female"
            };

            MStatus = new ObservableCollection<string>()
            {
                "Divorced", "Married","Widowed", "Seperated", "Living withpartner", "Civil relationship", "Single"
            };

            YesOrNo = new ObservableCollection<string>()
            {
                "Yes", "No"
            };

            OccupationTypes = new ObservableCollection<string>()
            {
                "Employed", "Full Time Education", "Self-Employed", "Retired","Unemployed", "Home Maker", "Employed Due To Disability","Indenpendent Means", "Voluntary"
            };

            Industries = new ObservableCollection<string>()
            {
                "Finance", "banking", "Retail", "Insurance", "AutoMobile" ,"Technology","Other"
            };

            JobFunction = new ObservableCollection<string>()
            {
                "Software engineer", "System Analyst", "Business Analyst" , "Technical support", "Network Engineer", "Consultant", "Technical Sales", "Project Manager", "other"
            };

            LicenseTypes = new ObservableCollection<string>()
            {
                "Class A", "Class B","Class C","Class D","Class E","Class MJ","Class DJ", "CDL"
            };

            LicenseDurations = new ObservableCollection<string>()
            {
                "Less than a year", "1", "2", "3" ,"4", "5", "6", "7", "8" ,"9" , "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "20+ year"
            };

            ClaimTypes = new ObservableCollection<string>()
            {
                "Accident", "Theft", "Windscreen Glass", "Malicious Damage", "Storm or Flood", "Fire"
            };

            ConvictionTypes = new ObservableCollection<string>()
            {
                "Pending conviction", "Exceeding statutory speed limit on a public road", "Exceeding statutory speed limit on a motorway", "use of a hand held device while driving", "using a vehicle uninsured against third party risks"
            };

            NomineeTitles = new ObservableCollection<string>()
            {
                "Mr.","Mrs","Miss"
            };

            RelationShips = new ObservableCollection<string>()
            {
                "Spouse","Parent(Father)","Parent(Mother)", "child", "Legal Heir"
            };

            Multiselect = new ObservableCollection<bool>()
            {
                true,false
            };

        }

        //save command
        public ICommand SaveCommand => new Command(async () => await SaveAsync());

        private async Task SaveAsync()
        {
            //Page currentPage = ACIANavigation.GetCurrentPage();
            //await currentPage.Navigation.PushAsync(new CarFormChoiceView());
            IsValid = Validate();
            if (IsValid)
            {
                var postData = GetPersonalFormPostData();
                ShowIndicator();
                var response = await _saveAndContinueServive.SaveAndContinueAsync(postData, ServicesHelper.GetEndpointToSaveForm("Personal"));
                HideIndicator();
                if (response.Status.ToUpper() == "DATA SAVED SUCCESSFULLY")
                {
                    Application.Current.Properties["EmailId"] = EmailAddress.Value;
                    if(IsNamedDriver==true){
                        Application.Current.Properties["FirstName"] = FirstName.Value;
                        Application.Current.Properties["LastName"] = LastName.Value;

                    }
                    await currentPage.Navigation.PushAsync(new CarFormChoiceView());
                    HideIndicator();
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
                await currentPage.DisplayAlert("Alert", "Please fill all mandatory fields", "ok");
            }
        }


        public ICommand SaveAndContinueLaterCommand => new Command(async () => await SaveAndContinueLaterAsync());

        //save and continue later
        private async Task SaveAndContinueLaterAsync()
        {
            if (isGuestUser){
                await currentPage.DisplayAlert("Alert", "kindly login to enable this feature", "Ok");
            }
            else
            {
                IsValid = Validate();
                string userName = string.Empty;
                if (IsValid)
                {
                    var postData = GetPersonalFormPostData();
                    //DialogService.ShowLoader();
                    ShowIndicator();
                    var response = await _saveAndContinueServive.SaveAndContinueAsync(postData, ServicesHelper.GetEndpointToSaveForm("Personal"));
                    //DialogService.HideLoader();
                    HideIndicator();
                    if (response.Status.ToUpper() == "DATA SAVED SUCCESSFULLY")
                    {
                        TriggerEmailModel model = new TriggerEmailModel();
                        model.MailTo = EmailAddress.Value;
                        model.QuoteId = quoteID;
                        var triggerEmailPostData = ServicesHelper.GetTriggerEmailPostData(model);
                        //DialogService.ShowLoader();
                        ShowIndicator();
                        var triggerMailResponse = await _saveAndContinueServive.PostDataAsync<EmailModel>(GlobalSetting.Instance.SendMailEndPoint,triggerEmailPostData);
                        //DialogService.HideLoader();
                        HideIndicator();
                        if (Application.Current.Properties.ContainsKey("UserName"))
                        {
                            userName = (string)Application.Current.Properties["UserName"];
                        }
                        Application.Current.Properties["EmailId"] = EmailAddress.Value;
                        if (IsNamedDriver == true)
                        {
                            Application.Current.Properties["FirstName"] = FirstName.Value;
                            Application.Current.Properties["LastName"] = LastName.Value;

                        }
                        await Application.Current.SavePropertiesAsync();
                        if(triggerMailResponse.Status == "Mail sent successfully"){
                            //await DialogService.ShowConfirmAsync(this, "Resume link successfully sent to your Email ID, return to Dashboard?", "Ok", "Cancel");
                            var result = await currentPage.DisplayAlert("Alert", "Resume link successfully sent to your Email ID,return to Dashboard?", "Ok", "Cancel");
                            if (result)
                            {
                                OnConfirmDialogActionAsync();
                            }
                        }
                        else{
                            //await DialogService.ShowAlertAsync(triggerMailResponse.Status, "Alert", "Ok");
                            await currentPage.DisplayAlert("Alert", response.Status, "Ok");
                        }
                        //await currentPage.DisplayAlert("Alert", "Form Saved Successfully, return to Dashboard?", "Ok", "Cancel");
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

        public ICommand PreviousCommand => new Command(async() => await PreviousCommandAction());

        private async Task PreviousCommandAction()
        {
            await currentPage.Navigation.PopAsync();
        }

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

        public ICommand IsNamedDriverCommand => new Command(() => IsNamedDriverAction());

        private void IsNamedDriverAction()
        {
            if (IsNamedDriver == true)
            {
                IsNamedDriverVisibility = true;
            }
            else
            {
                IsNamedDriverVisibility = false;
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

        //command for picker dependent fields
        public ICommand OccupationPickerCommand => new Command(() => OccupationPickerAction());

        private void OccupationPickerAction()
        {
            if ((OccupationType.Value == "Employed") || (OccupationType.Value == "Self - Employed") ||
                (OccupationType.Value == "Employed Due To Disability") || (OccupationType.Value == "Voluntary"))
            {
                OccupationPickerVisibility = true;
            }
            else
            {
                OccupationPickerVisibility = false;
            }
        }

        public ICommand IsHaveNomineeCommand => new Command(() => IsHaveNomineeAction());

        private void IsHaveNomineeAction()
        {
            if (IsHaveNominee == true)
            {
                IsHaveNomineeVisibility = true;
            }
            else
            {
                IsHaveNomineeVisibility = false;
            }
        }

        public ICommand IsCitizenByBirthCommand => new Command(() => IsCitizenByBirthAction());

        private void IsCitizenByBirthAction()
        {
            if (CitizenByBirth == true)
            {
                IsCitizenByBirthVisibility = false;
            }
            else
            {
                IsCitizenByBirthVisibility = true;
            }
        }

        //Get post data
        private string GetPersonalFormPostData()
        {
            
            //collect captured values
            SaveCarPersonalModel saveCarPersonalModel = new SaveCarPersonalModel();
            if(isGuestUser){
                saveCarPersonalModel.UserId = "00000000-0000-0000-0000-000000000000";
            }
            else{
                saveCarPersonalModel.UserId = currentUserId;
            }
            saveCarPersonalModel.QuoteId = quoteID;
            saveCarPersonalModel.Title = Title.Value;
            saveCarPersonalModel.FirstName = FirstName.Value;
            saveCarPersonalModel.LastName = LastName.Value;
            saveCarPersonalModel.MiddleInitial = MiddleInitial.Value;
            saveCarPersonalModel.Suffix = Suffix;
            saveCarPersonalModel.Gender = Gender.Value;
            saveCarPersonalModel.DateOfBirth = DateOfBirth.Value;
            saveCarPersonalModel.MaritalStatus = MartialStatus.Value;
            saveCarPersonalModel.MobilePhone = MobileNumber.Value;
            saveCarPersonalModel.EmailId = EmailAddress.Value;
            saveCarPersonalModel.ZipCode = Zipcode.Value;
            saveCarPersonalModel.City = Town;
            saveCarPersonalModel.State = State;
            saveCarPersonalModel.Address = Address.Value;
            saveCarPersonalModel.Citizenship = CitizenShip.Value;
            saveCarPersonalModel.CitizenByBirth = CitizenByBirth.ToString();
            if(CitizenByBirth==false){
                saveCarPersonalModel.CitizenshipStartDate = CitizenshipStartDate.Value;
            }else{
                saveCarPersonalModel.CitizenshipStartDate = null;
            }

            saveCarPersonalModel.OccupationType = OccupationType.Value;
            saveCarPersonalModel.Industry = Industry.Value;
            saveCarPersonalModel.JobFunctions = JobFunctions.Value;
            saveCarPersonalModel.IsNamedDriver = IsNamedDriver;
            saveCarPersonalModel.LicenseType = LicenseType.Value;
            saveCarPersonalModel.LicenseDuration = LicenseDuration.Value;
            saveCarPersonalModel.LicenseNo = DrivingLicenseNumber.Value;
            saveCarPersonalModel.IsHaveAccidentsOrClaims = IsHaveAccidentsOrClaims;
            saveCarPersonalModel.IsHaveConviction = IsHaveConviction;
            saveCarPersonalModel.IsHaveNominee = IsHaveNominee;
            saveCarPersonalModel.NomineeTitle = NomineeTitle.Value;
            saveCarPersonalModel.NomineeFirstName = NomineeFirstName.Value;
            saveCarPersonalModel.NomineeLastName = NomineeLastName.Value;
            if(IsHaveNominee){
                saveCarPersonalModel.NomineeDOB = NomineeDOB.Value;
            }else{
                saveCarPersonalModel.NomineeDOB = null;
            }

            saveCarPersonalModel.NomineeGender = NomineeGender.Value;
            saveCarPersonalModel.NomineeRelationship = NomineeRelationship.Value;
            saveCarPersonalModel.AppointeeName = AppointeeName.Value;
            saveCarPersonalModel.IsHomeOwner = IsHomeOwner;
            saveCarPersonalModel.IsHaveChild = IsHaveChild;
            saveCarPersonalModel.NoOfCars = IsHaveCars;
            saveCarPersonalModel.PersonalClaimsList = new List<PersonalClaimsList>();
            if (IsHaveAccidentsOrClaims)
            {
                PersonalClaimsList personalClaimsList = new PersonalClaimsList();
                personalClaimsList.QuoteId = quoteID;
                personalClaimsList.ClaimDate = ClaimDate.Value;
                personalClaimsList.ClaimAmount = ClaimAmount.Value;
                personalClaimsList.ClaimType = ClaimType.Value;
                personalClaimsList.EmailId = EmailAddress.Value;
                saveCarPersonalModel.PersonalClaimsList.Add(personalClaimsList);
            }
            saveCarPersonalModel.PersonalConvictionsList = new List<PersonalConvictionsList>();
            if (IsHaveConviction)
            {
                PersonalConvictionsList personalConvictionList = new PersonalConvictionsList();
                personalConvictionList.QuoteId = quoteID;
                personalConvictionList.ConvictionDate = ConvictionDate.Value;
                personalConvictionList.ConvictionType = ConvictionType.Value;
                personalConvictionList.PointsIncurred = PointsIncurred;
                personalConvictionList.FineIncurred = FineIncurred;
                personalConvictionList.BanLength = BanLength;
                personalConvictionList.BreathalysedReading = BreathalyserReading.Value;
                personalConvictionList.IsBreathalysed = IsBreathalysed;
                personalConvictionList.IsAccident = IsAccident;
                personalConvictionList.EmailId = EmailAddress.Value;
                saveCarPersonalModel.PersonalConvictionsList.Add(personalConvictionList);
            }
            return ServicesHelper.GetCarPersonalFormPostData(saveCarPersonalModel);
        }

        //populate personal form with data
        private void PopulateFormWithData(SaveCarPersonalModel saveCarPersonalModel)
        {
            if(!(String.IsNullOrEmpty(saveCarPersonalModel.FirstName)))
            {
                Title.Value = saveCarPersonalModel.Title;
                FirstName.Value = (string)saveCarPersonalModel.FirstName;
                LastName.Value = saveCarPersonalModel.LastName;
                MiddleInitial.Value = saveCarPersonalModel.MiddleInitial;
                if (saveCarPersonalModel.Suffix != null)
                {
                    Suffix = (string)saveCarPersonalModel.Suffix;
                }
                Gender.Value = saveCarPersonalModel.Gender;
                //DateTime dateOfBirth = DateTime.Parse(saveCarPersonalModel.DateOfBirth).Date;
                //DateOfBirth.Value = dateOfBirth.ToString("MM/dd/yyyy");
                MartialStatus.Value = saveCarPersonalModel.MaritalStatus;
                if (saveCarPersonalModel.MobilePhone != null)
                {
                    MobileNumber.Value = Regex.Replace((string)saveCarPersonalModel.MobilePhone, @"[^0-9a-zA-Z]+", "");
                }
                EmailAddress.Value = saveCarPersonalModel.EmailId;
                Zipcode.Value = saveCarPersonalModel.ZipCode;
                if(!(String.IsNullOrEmpty(saveCarPersonalModel.City))){
                    Town = saveCarPersonalModel.City;
                }
                if(!(String.IsNullOrEmpty(saveCarPersonalModel.State))){
                    State = saveCarPersonalModel.State;
                }
                Address.Value = saveCarPersonalModel.Address;
                CitizenShip.Value = saveCarPersonalModel.Citizenship;

                StringToBoolConverter converter = new StringToBoolConverter();
                CitizenByBirth = (bool)converter.Convert(saveCarPersonalModel.CitizenByBirth, null, CitizenByBirth, null);

                if (saveCarPersonalModel.CitizenshipStartDate != null)
                {
                    //CitizenshipStartDate.Value = (string)saveCarPersonalModel.CitizenshipStartDate;
                }
                OccupationType.Value = saveCarPersonalModel.OccupationType;
                if (saveCarPersonalModel.Industry != null)
                {
                    Industry.Value = (string)saveCarPersonalModel.Industry;
                }
                if (saveCarPersonalModel.JobFunctions != null)
                {
                    JobFunctions.Value = (string)saveCarPersonalModel.JobFunctions;
                }
                IsNamedDriver = saveCarPersonalModel.IsNamedDriver;
                if (saveCarPersonalModel.LicenseType != null)
                {
                    LicenseType.Value = (string)saveCarPersonalModel.LicenseType;
                }
                if (saveCarPersonalModel.LicenseDuration != null)
                {
                    LicenseDuration.Value = (string)saveCarPersonalModel.LicenseDuration;
                }
                if (saveCarPersonalModel.LicenseNo != null)
                {
                    DrivingLicenseNumber.Value = (string)saveCarPersonalModel.LicenseNo;
                }

                IsHaveAccidentsOrClaims = saveCarPersonalModel.IsHaveAccidentsOrClaims;
                IsHaveConviction = saveCarPersonalModel.IsHaveConviction;
                IsHaveNominee = saveCarPersonalModel.IsHaveNominee;
                if (saveCarPersonalModel.NomineeTitle != null)
                {
                    NomineeTitle.Value = (string)saveCarPersonalModel.NomineeTitle;
                }
                if (saveCarPersonalModel.NomineeFirstName != null)
                {
                    NomineeFirstName.Value = (string)saveCarPersonalModel.NomineeFirstName;
                }
                if (saveCarPersonalModel.NomineeLastName != null)
                {
                    NomineeLastName.Value = (string)saveCarPersonalModel.NomineeLastName;
                }
                if (saveCarPersonalModel.NomineeDOB != null)
                {
                    //NomineeDOB.Value = (string)saveCarPersonalModel.NomineeDOB;
                }
                if (saveCarPersonalModel.NomineeGender != null)
                {
                    NomineeGender.Value = (string)saveCarPersonalModel.NomineeGender;
                }
                if (saveCarPersonalModel.NomineeRelationship != null)
                {
                    NomineeRelationship.Value = (string)saveCarPersonalModel.NomineeRelationship;
                }
                if (saveCarPersonalModel.AppointeeName != null)
                {
                    AppointeeName.Value = (string)saveCarPersonalModel.AppointeeName;
                }
                IsHomeOwner = saveCarPersonalModel.IsHomeOwner;
                IsHaveChild = saveCarPersonalModel.IsHaveChild;
                if (saveCarPersonalModel.NoOfCars != null)
                {
                    IsHaveCars = (string)saveCarPersonalModel.NoOfCars;
                }
                if (saveCarPersonalModel.PersonalClaimsList != null && saveCarPersonalModel.PersonalClaimsList.Count > 0)
                {
                    PersonalClaimsList personalClaimsList = saveCarPersonalModel.PersonalClaimsList[0];
                    //= quoteID = personalClaimsList.QuoteId;
                    if (personalClaimsList.ClaimDate != null)
                    {
                        //ClaimDate.Value = (string)personalClaimsList.ClaimDate;
                    }
                    if (personalClaimsList.ClaimAmount != null)
                    {
                        ClaimAmount.Value = (string)personalClaimsList.ClaimAmount;
                    }
                    if (personalClaimsList.ClaimType != null)
                    {
                        ClaimType.Value = (string)personalClaimsList.ClaimType;
                    }
                    EmailAddress.Value = personalClaimsList.EmailId;
                }

                if (saveCarPersonalModel.PersonalConvictionsList != null && saveCarPersonalModel.PersonalConvictionsList.Count > 0)
                {
                    PersonalConvictionsList personalConvictionList = saveCarPersonalModel.PersonalConvictionsList[0];
                    //personalConvictionList.QuoteId = quoteID;
                    if (personalConvictionList.ConvictionDate != null)
                    {
                        //ConvictionDate.Value = (string)personalConvictionList.ConvictionDate;
                    }
                    if (personalConvictionList.ConvictionType != null)
                    {
                        ConvictionType.Value = (string)personalConvictionList.ConvictionType;
                    }
                    if (personalConvictionList.PointsIncurred != null)
                    {
                        PointsIncurred = (string)personalConvictionList.PointsIncurred;
                    }
                    if (personalConvictionList.FineIncurred != null)
                    {
                        FineIncurred = (string)personalConvictionList.FineIncurred;
                    }
                    if (personalConvictionList.BanLength != null)
                    {
                        BanLength = (string)personalConvictionList.BanLength;
                    }
                    if (personalConvictionList.BreathalysedReading != null)
                    {
                        BreathalyserReading.Value = (string)personalConvictionList.BreathalysedReading;
                    }
                    if (personalConvictionList.IsBreathalysed != null)
                    {
                        IsBreathalysed = (bool)personalConvictionList.IsBreathalysed;
                    }
                    if (personalConvictionList.IsAccident != null)
                    {
                        IsAccident = (bool)personalConvictionList.IsAccident;
                    }

                    EmailAddress.Value = personalConvictionList.EmailId;
                }
            }
            //quoteID = saveCarPersonalModel.QuoteId;

        }

        //validate
        private bool Validate()
        {
            return ValidateNonDependentFields() && ValidateAccidentOrClaims() && ValidateOccupationType() && ValidateNamedDriver()&&ValidateCitizenByBirth()
                && ValidateConvictions() && ValidateNomineeFields();
        }

        private bool ValidateNomineeFields()
        {
            if (IsHaveNominee == true)
            {
                return _nomineeTitle.Validate() && _nomineeRelationship.Validate() && _nomineeGender.Validate() && _nomineeFirstName.Validate() && _nomineeDOB.Validate()
                                    && _nomineeLastName.Validate() && _appointeeName.Validate();
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

        private bool ValidateNamedDriver(){
            if(IsNamedDriver==true){
                return _licenseType.Validate() && _licenseDuration.Validate() && _drivingLicenseNumber.Validate();
            }else{
                return true;
            }
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

        private bool ValidateNonDependentFields()
        {
            return _title.Validate() && _firstName.Validate() && _dateOfBirth.Validate() && _lastName.Validate() && _gender.Validate() 
                         && _martialStatus.Validate() && _mobileNumber.Validate() && _emailAddress.Validate()
                         && _zipcode.Validate() && _address.Validate() && _occupationType.Validate();
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

        private bool ValidateOccupationType()
        {
            if (OccupationType.Value == "Employed" || OccupationType.Value == "Self-Employed" || OccupationType.Value == "Employed Due To Disability" || OccupationType.Value == "Voluntary")
            {
                return _industry.Validate() && _jobFunctions.Validate();
            }
            else
            {
                return true;
            }

        }

        //add validations
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
            _occupationType.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Occupation" });
            _industry.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Industry" });
            _jobFunctions.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Occupation" });
            _licenseType.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select LicenseType" });
            _licenseDuration.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select License Duration" });
            _drivingLicenseNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter License Number" });
            _claimDate.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Claim Date" });
            _claimAmount.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Claim Amount" });
            _claimType.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Claim Type" });
            _convictionDate.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Conviction Date" });
            _convictionType.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Conviction Type" });
            _breathalyserReading.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Breatlyser Reading" });
            _nomineeTitle.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Nominee Title" });
            _nomineeGender.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Nominee Gender" });
            _nomineeFirstName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Nominee First Name" });
            _nomineeLastName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Nominee Last Name" });
            _nomineeDOB.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Nominee Date of Birth" });
            _appointeeName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Enter Appointee Name" });
            _nomineeRelationship.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please Select Nominee Relationship" });
        }

    }
}
