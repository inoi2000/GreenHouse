using GreenHouse.HttpApiClient;
using Microsoft.AspNetCore.Components;

namespace GreenHouse.WebAdminClient.Shared
{
    public class AppComponentBase : ComponentBase
    {
        [Inject] protected IGreenHouseClient GreenHouseClient { get; private set; }
        [Inject] protected AppState State { get; private set; }

        protected CancellationTokenSource _cts = new CancellationTokenSource();
    }
}
