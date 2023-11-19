using GreenHouse.HttpModels.Responses;

namespace GreenHouse.WebUserClient.Pages
{
    public partial class Index
    {
        private IReadOnlyList<CityResponse>? Cities { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Cities = await GreenHouseClient.GetAllCities(_cts.Token);
        }
    }
}
