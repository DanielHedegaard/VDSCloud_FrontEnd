using Microsoft.AspNetCore.Components;
using MudBlazor;
using Web.Models;

namespace Web.Components.LoginComponents
{
    public partial class CreateuserComponent
    {
        [Inject]
        public Session Session { get; set; }

        public string CreateText { get; set; } = "Create account";
        public bool CreateBtnDisabled { get; set; }
        private string PasswordReenter { get; set; } = string.Empty;

        protected override void OnInitialized()
        {
            Session.User.UserName = string.Empty;
            Session.User.Password = string.Empty;
        }

        private async Task CreateAcount()
        {
            CreateBtnDisabled = true;
            CreateText = "Creating and logging in";

            await Session.CreateUserAsync(PasswordReenter);

            CreateText = "Create Account";
            CreateBtnDisabled = false;
        }

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