using Microsoft.AspNetCore.Components;
using Models;
using MudBlazor;
using Web.Models;
using Web.Services;

namespace Web.Components.LoginComponents
{
    public partial class LoginComponent
    {
        //password hide variables
        bool isShow;
        InputType PasswordInput = InputType.Password;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        public string LoginText { get; set; } = "Login";

        public bool LoginBtnDisabled { get; set; }

        [Inject]
        public Session Session { get; set; }

        protected override void OnInitialized()
        {
            Session.User.UserName = string.Empty;
            Session.User.Password = string.Empty;
        }

        private async Task LogginIn()
        {
            LoginBtnDisabled = true;
            LoginText = "Logging in";

            await Session.LogUserInAsync();

            LoginText = "Login";
            LoginBtnDisabled = false;
        }

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
    }
}