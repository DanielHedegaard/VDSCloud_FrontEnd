using Microsoft.AspNetCore.Components;
using Models;
using MudBlazor;
using static MudBlazor.Colors;

namespace Web.Components.LoginComponents
{
    public partial class LoginComponent
    {
        public User UserModel { get; set; } = new User();

        //password hide variables
        bool isShow;
        InputType PasswordInput = InputType.Password;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            
        [Parameter, EditorRequired]
        public EventCallback CreateUserClickedCallback { get; set; }

        [Parameter, EditorRequired]
        public EventCallback LoginCallback { get; set; }
        
        void PassHideBtn()
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
        private async Task OnValidLoginSubmit()
        {
            //get user (for id) and pass as parameter
            await SetValueAsync("1");
            //login
        }

        private async Task SetValueAsync(string UserId)
        {
            await SessionStorageAccessor.SetValueAsync(UserId, UserModel.UserName);
        }
    }
}
