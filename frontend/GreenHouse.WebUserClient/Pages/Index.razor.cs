using GreenHouse.HttpModels.Responses;
using GreenHouse.WebUserClient.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GreenHouse.WebUserClient.Pages
{
    public partial class Index
    {
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        private IReadOnlyList<CityResponse>? Cities { get; set; }

        public void NavigateToCityPage(Guid id)
        {
            Navigation.NavigateTo($"/city/{id}", false);
        }

        private void OpenBookDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small, FullWidth=true };
            DialogService.Show<СontactDialog>("Бронирование", options);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Cities = await GreenHouseClient.GetAllCitiesAsync(_cts.Token);
        }
    }
}
