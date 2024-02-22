using Microsoft.AspNetCore.Components;
using MudBlazor;
using Web.Models;

namespace Web.Components.LoginComponents
{
    public partial class CreateuserComponent
    {
        [Parameter, EditorRequired]
        public EventCallback CreateUserClickedCallback { get; set; }

        [Parameter, EditorRequired]
        public UserSession UserSession { get; set; }

        private bool ButtonDisabled
        {
            get
            {
                if (string.IsNullOrEmpty(UserSession.User.UserName) 
                    || string.IsNullOrEmpty(UserSession.User.Password)
                    || UserSession.User.Password != PasswordReenter)
                {
                    return true;
                }

                return false;
            }
        }

        private string PasswordReenter { get; set; } = string.Empty;

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