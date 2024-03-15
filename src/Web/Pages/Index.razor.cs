using Microsoft.AspNetCore.Components;
using MudBlazor;
using Web.Models;

namespace Web.Pages
{
    public partial class Index
    {
        [Inject]
        public Session Session { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        public bool CreateUserbool { get; set; } = false;

        public bool IsUserLoggedIn { get; set; }

        protected override async Task OnInitializedAsync()
        {
            IsUserLoggedIn = await Session.IsUserLoggedIn();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender) 
            {
                Session.UserLoggedInEvent += Handle_UserStateChanged;
                Session.UserLoggedOutEvent += Handle_UserStateChanged;
                Session.ErrorEvent += Handle_ErrorEvent;
                Session.SuccessEvent += Handle_SuccessEvent;
                StateHasChanged();
            }

            IsUserLoggedIn = await Session.IsUserLoggedIn();
        }

        private async void Handle_UserStateChanged()
        {
            IsUserLoggedIn = await Session.IsUserLoggedIn();
            StateHasChanged();
        }

        private void Handle_ErrorEvent(string snackbarMessage)
        {
            Snackbar.Add(snackbarMessage, Severity.Error, config =>
            {
                config.Icon = Icons.Material.Filled.Error;
                config.IconColor = Color.Primary;
                config.IconSize = Size.Large;
            });

            StateHasChanged();
        }   

        private void Handle_SuccessEvent(string snackbarMessage)
        {
            Snackbar.Add(snackbarMessage, Severity.Success, config =>
            {
                config.Icon = Icons.Material.Filled.Check;
                config.IconColor = Color.Primary;
                config.IconSize = Size.Large;
            });

            StateHasChanged();
        }

        public void InvertCreateUserState() => CreateUserbool = !CreateUserbool;
    }
}
