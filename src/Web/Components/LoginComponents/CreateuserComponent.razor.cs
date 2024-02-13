using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Web.Components.LoginComponents
{
    public partial class CreateuserComponent
    {
        [Parameter, EditorRequired]
        public EventCallback SignInClickedCallback { get; set; }

        //password hide variables
        bool isShow;
        InputType PasswordInput = InputType.Password;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        //ReEnter Pass hide variables
        bool reEnterIsShow;
        InputType reEnterPassInp = InputType.Password;
        string reEnterPassInputIcon = Icons.Material.Filled.VisibilityOff;


        private void PassHideBtn()
        {
            if (isShow)
            {
                isShow = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                isShow = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }

        private void ReEnterPassHideBtn()
        {
            if (reEnterIsShow)
            {
                reEnterIsShow = false;
                reEnterPassInputIcon = Icons.Material.Filled.VisibilityOff;
                reEnterPassInp = InputType.Password;
            }
            else
            {
                reEnterIsShow = true;
                reEnterPassInputIcon = Icons.Material.Filled.Visibility;
                reEnterPassInp = InputType.Text;
            }
        }
    }
}
