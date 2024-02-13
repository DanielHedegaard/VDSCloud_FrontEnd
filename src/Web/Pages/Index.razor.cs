namespace Web.Pages
{
    public partial class Index
    {
        public bool LoggedIn { get; set; } = false;
        public bool CreateUserbool { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CreateUserbool = false;
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

    }
}
