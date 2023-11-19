using GreenHouse.HttpApiClient;
using Microsoft.AspNetCore.Components;

namespace GreenHouse.WebUserClient.Shared
{
    public class AppComponentBase : ComponentBase
    {
        [Inject] protected IGreenHouseClient GreenHouseClient { get; private set; }

        protected CancellationTokenSource _cts = new CancellationTokenSource();
    }
}
