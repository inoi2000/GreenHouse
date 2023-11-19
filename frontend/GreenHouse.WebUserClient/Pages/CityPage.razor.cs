using GreenHouse.HttpModels.Responses;
using Microsoft.AspNetCore.Components;

namespace GreenHouse.WebUserClient.Pages
{
    public partial class CityPage
    {
        [Parameter] public Guid Id { get; set; }

        private CityResponse City { get; set; }
        private CancellationTokenSource _cts = new CancellationTokenSource();
    }
}
