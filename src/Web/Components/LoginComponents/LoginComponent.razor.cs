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

        [Parameter, EditorRequired]
        public EventCallback LoginClickedCallback { get; set; }

        [Parameter, EditorRequired]
        public UserSession UserSession { get; set; }

        private bool ButtonDisabled 
        { 
            get
            {
                if(string.IsNullOrEmpty(UserSession.User.UserName) || string.IsNullOrEmpty(UserSession.User.Password))
                {
                    return true;
                }

                return false;
            } 
        }

        protected override void OnInitialized()
        {
            if(UserSession == null)
            {
                throw new ArgumentException($"{nameof(UserSession)} cannot be null");
            }
            
            UserSession.User.UserName = string.Empty;
            UserSession.User.Password = string.Empty;
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