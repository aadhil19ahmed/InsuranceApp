using System;
using System.Collections.Generic;
using ACIA.Helper;
using ACIA.Helper.Validations;
using ACIA.Services.Dialog;
using ACIA.Services.GetDynamicPage;
using ACIA.Services.Navigation;
using ACIA.Services.RequestProvider;
using ACIA.Services.SaveAndContinueService;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ACIA.Views.UILayer
{
    public class DUIButtonView : BaseSectionView
    {
        IDialogService dialogService = new DialogService();
        Model.View _view;
        public DUIButtonView(Model.View view)
        {
            _view = view;
        }

        private void CreateButtonView()
        {
            var button = new Button();
            button.Text = _view.name;
            var stack = new StackLayout();
            stack.Children.Add(button);
            button.Clicked += Button_ClickedAsync;
            Content = stack;
        }

        public override void CreateView()
        {
            base.CreateView();
            this.CreateButtonView();
        }

        async void Button_ClickedAsync(object sender, EventArgs e)
        {
            IRequestProvider requestProvider = new RequestProvider();
            IDialogService dialogService = new DialogService();
            //List<BaseSection> baseSections = new List<BaseSection>();
            DynamicUIPage dynamicUIPage = DUINavigationStack.GetCurrentPage();
            var postData = new Dictionary<string, object>();
            var serviceKeys = ServicesHelper.GetServiceKeysForID(dynamicUIPage.Title);
            int listIndex = 0;
            bool IsValid = true;
            foreach (BaseSection section in dynamicUIPage.baseSections)
            {
                if (IsValid)
                {
                    foreach (BaseSectionView subView in section.Views)
                    {
                        if (IsValid)
                        {
                            if (subView.GetType() == typeof(FormSubView))
                            {
                                var formSubView = subView as FormSubView;
                                //if (serviceKeys.Count != listIndex)
                                //{
                                    postData.Add(serviceKeys[listIndex], formSubView.inputView.CapturedValue);
                                    listIndex++;
                                //}

                                if (formSubView.reg_ex != string.Empty)
                                {
                                    if (!Validator.Validate(formSubView.inputView.CapturedValue, formSubView.reg_ex))
                                    {
                                        IsValid = false;
                                        await dialogService.ShowAlertAsync("Please enter valid data for field " + formSubView.name, "Alert", "Ok");
                                    }
                                    formSubView.questionLabel.label.TextColor = IsValid ? Color.Black : Color.Red;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            listIndex = 0;

            //make service call after all validations are full filled
            if (IsValid)
            {
                var content = JsonConvert.SerializeObject(postData);
                ISaveAndContinueService saveCarPersonalDetailsService = new SaveAndContinueService(requestProvider);
                dialogService.ShowLoader();
                var response = await saveCarPersonalDetailsService.SaveAndContinueAsync(content, ServicesHelper.GetEndpointToSaveForm(dynamicUIPage.Title));
                dialogService.HideLoader();
                if(response.Status.ToUpper() == "DATA SAVED SUCCESSFULLY")
                {
                    IGetDynamicPageService dynamicPageService = new GetDynamicPageService(requestProvider);
                    await Navigation.PushAsync(dynamicPageService.GetDynamicPage(_view.url));
                }
                else
                {
                    await dialogService.ShowAlertAsync(response.Status,"Alert", "Ok");
                }
            }
        }
    }
}
