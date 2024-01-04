using GreenHouse.HttpModels.Responses;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GreenHouse.WebAdminClient.Pages
{
    public partial class Index
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }

        public AdminResponse Admin { get; set; }

        private CancellationTokenSource _cts = new CancellationTokenSource();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            try
            {
                Admin = State?.Admin;
                StateHasChanged();
            }
            catch (UnauthorizedAccessException e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }

        }

        public async Task Exit()
        {
            await LocalStorage.RemoveItemAsync("token");
            State.IsTokenChecked = false;
            GreenHouseClient.DeleteAuthorizationToken();
            State.LoggedIn = false;

            NavigationManager.NavigateTo("/authorisation");
        }
    }
}
