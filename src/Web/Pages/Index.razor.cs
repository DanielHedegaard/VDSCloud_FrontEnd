using Web.Utilities;

namespace Web.Pages
{
    public partial class Index
    {
        public bool LoggedIn { get; set; }
        public bool CreateUserbool { get; set; }
        public string returnvaltste { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                CreateUserbool = false;
                LoggedIn = false;
                StateHasChanged();
            }
        }
        public void HandleCreateUserClicked()
        {
            CreateUserbool = true;
            StateHasChanged();
        }
        public void HandleSignInClicked()
        {
            CreateUserbool = false;
            StateHasChanged();
        }
        //login testing delete later
        public async Task HandleLoginCallback()
        {
            //change
            await GetValueAsync();
            if (returnvaltste != string.Empty)
            {
                if (returnvaltste != null)
                {
                    LoggedIn = true;
                }
            }
            StateHasChanged();
        }
        //
        public async Task HandleLogoutCallback()
        {
            await RemoveAsync();
            LoggedIn = false;
            StateHasChanged();
        }

        private async Task GetValueAsync()
        {
            string Key = "1";
            returnvaltste = await SessionStorageAccessor.GetValueAsync<string>(Key);
        }
        public async Task RemoveAsync()
        {
            await SessionStorageAccessor.RemoveAsync("1");
        }
    }
}
