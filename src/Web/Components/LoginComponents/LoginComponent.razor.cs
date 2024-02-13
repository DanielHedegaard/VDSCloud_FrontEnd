using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Web.Components.LoginComponents
{
    public partial class LoginComponent
    {
        //password hide variables
        bool isShow;
        InputType PasswordInput = InputType.Password;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            
        [Parameter, EditorRequired]
        public EventCallback CreateUserClickedCallback { get; set; }

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
    }
}
