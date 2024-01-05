using Blazored.LocalStorage;
using GreenHouse.HttpApiClient;
using Microsoft.AspNetCore.Components;

namespace GreenHouse.WebAdminClient.Shared
{
    public class AppComponentBase : ComponentBase
    {
        [Inject] protected IGreenHouseClient GreenHouseClient { get; private set; }
        [Inject] protected AppState State { get; private set; }
        [Inject] protected ILocalStorageService LocalStorage { get; private set; }

        protected CancellationTokenSource _cts = new CancellationTokenSource();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (State.IsTokenChecked) return;
            State.IsTokenChecked = true;

            string? token = await LocalStorage.GetItemAsync<string>("token");
            if (!string.IsNullOrWhiteSpace(token))
            {
                GreenHouseClient.SetAuthorizationToken(token);
                State.Admin = await GreenHouseClient.GetCurrentAdmin(_cts.Token);
                State.LoggedIn = true;
            }
            else
            {
                State.LoggedIn = false;
            }
        }
    }
}
