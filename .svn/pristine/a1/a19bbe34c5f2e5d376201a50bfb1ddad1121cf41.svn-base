using System.Threading.Tasks;
using ACIA.Helper.Prompt;
using Acr.UserDialogs;

namespace ACIA.Services.Dialog
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
        Task ShowConfirmAsync(IConfirmDelegate confirmDelegate, string message, string okText, string cancelText);
        void ShowPromptAsync(IPromptHelper customDelegate, string title = "Title", string placeholder = "PlaceHolder", InputType inputType = InputType.Default, int maxLength = 20, string okText = "Ok", string cancelText = "Cancel");
        void ShowLoader();
        void HideLoader();
    }
}
