using Microsoft.AspNetCore.Components;
using MudBlazor;
using Microsoft.AspNetCore.Components.Forms;
using GreenHouse.HttpModels.Requests;
using GreenHouse.HttpApiClient;
using GreenHouse.WebAdminClient.Shared;

namespace GreenHouse.WebAdminClient.Pages;

public partial class RegistrationPage
{
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }

    private bool _registrationInProgress;

    RegisterRequest model = new();
    bool success;

    private CancellationTokenSource _cts = new CancellationTokenSource();
    private async void OnValidSubmit(EditContext context)
    {
        success = true;
        StateHasChanged();
        await RegisterAccount();
    }

    public async Task RegisterAccount()
    {
        if (success)
        {
            if (_registrationInProgress)
            {
                Snackbar.Add("Пожалуйста подождите!", Severity.Warning);
                return;
            }

            try
            {
                _registrationInProgress = true;

                var account = new RegisterRequest(model.Login, model.Password, model.ConfirmedPassword);
                await GreenHouseClient.RegisterAccountAsync(account, _cts.Token);

                Snackbar.Add("Регистрация успешно завершена!", Severity.Success);

                NavigationManager.NavigateTo("/");
            }
            catch (GreenHouseApiExeption e)
            {
                Snackbar.Add(e.Details.Title, Severity.Error);
            }
            finally
            {
                _registrationInProgress = false;
                StateHasChanged();
            }
        }
    }
}
