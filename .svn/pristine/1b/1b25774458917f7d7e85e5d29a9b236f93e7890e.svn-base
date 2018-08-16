using Acr.UserDialogs;
using System.Threading.Tasks;
using ACIA.Helper.Prompt;

namespace ACIA.Services.Dialog
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public async Task ShowConfirmAsync(IConfirmDelegate confirmDelegate, string message, string okText, string cancelText)
        {
            var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Message = message,
                OkText = okText,
                CancelText = cancelText
            });

            if (result)
            {
                confirmDelegate.OnConfirmDialogActionAsync();
            }
        }

        public void ShowPromptAsync(IPromptHelper customDelegate, string title="Title", string placeholder="PlaceHolder", InputType inputType=InputType.Default, int maxLength=20, string okText="Ok", string cancelText="Cancel")
        {
            UserDialogs.Instance.Prompt(new PromptConfig
            {
                Title = title,
                Placeholder = placeholder,
                InputType = inputType,
                MaxLength = maxLength,
                OkText = okText,
                CancelText = cancelText,
                OnTextChanged = args =>
                {
                    
                },
                OnAction = (result) =>
                {
                    if(result.Ok)
                    {
                        customDelegate.PromptOkActionAsync(result.Text);
                    }
                }
            });
        }

        void HandleAction(PromptResult obj)
        {

        }

        public void ShowLoader()
        {
            UserDialogs.Instance.ShowLoading();
        }

        public void HideLoader()
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}
