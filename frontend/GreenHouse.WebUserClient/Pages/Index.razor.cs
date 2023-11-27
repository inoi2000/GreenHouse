using GreenHouse.HttpModels.Responses;
using Microsoft.AspNetCore.Components;

namespace GreenHouse.WebUserClient.Pages
{
    public partial class Index
    {
        [Inject] private NavigationManager Navigation { get; set; }
        private IReadOnlyList<CityResponse>? Cities { get; set; }

        public void NavigateToCityPage(Guid id)
        {
            Navigation.NavigateTo($"/city/{id}", false);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Cities = await GreenHouseClient.GetAllCitiesAsync(_cts.Token);
        }
    }
}
