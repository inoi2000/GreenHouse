using GreenHouse.HttpModels.Requests;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using GreenHouse.HttpApiClient;

namespace GreenHouse.WebAdminClient.Pages
{
    public partial class AuthorisationPage
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }

        private bool _authorisationInProgress;

        AuthorisationRequest model = new();
        bool success;

        private CancellationTokenSource _cts = new CancellationTokenSource();
        private async void OnValidSubmit(EditContext context)
        {
            success = true;
            StateHasChanged();
            await Authorisation();
        }

        public void MoveToRegistration()
        {
            NavigationManager.NavigateTo("/registration");
        }

        public async Task Authorisation()
        {
            if (success)
            {
                if (_authorisationInProgress)
                {
                    Snackbar.Add("Пожалуйста подождите!", Severity.Warning);
                    return;
                }

                try
                {
                    _authorisationInProgress = true;

                    var account = new AuthorisationRequest(model.Login, model.Password);
                    var authorisationResponse = await GreenHouseClient.AuthorisationAsync(account, _cts.Token);
                    await LocalStorage.SetItemAsync("token", authorisationResponse.Token);

                    Snackbar.Add("Авторизация успешно завершена!", Severity.Success);
                    State.LoggedIn = true;

                    NavigationManager.NavigateTo("/catalog");
                }
                catch (GreenHouseApiExeption e)
                {
                    Snackbar.Add(e.Details.Title, Severity.Error);
                }
                finally
                {
                    _authorisationInProgress = false;
                    StateHasChanged();
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}
