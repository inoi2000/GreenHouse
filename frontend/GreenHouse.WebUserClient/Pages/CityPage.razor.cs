using GreenHouse.HttpModels.Responses;
using GreenHouse.WebUserClient.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GreenHouse.WebUserClient.Pages
{
    public partial class CityPage
    {
        [Parameter] public Guid Id { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        private CityResponse City { get; set; }

        private string GetCityName()
        {
            string cityName = City.Name;
            if (!string.IsNullOrEmpty(cityName))
            {
                if (cityName.EndsWith("а"))
                {
                    cityName = cityName.Remove(cityName.Length - 1, 1);
                }
                
                return $"{cityName}е";
            }
            else
            {
                return string.Empty;
            }
        }
        private void BookedAppartment(AppartmentResponse appartment)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small, FullWidth = true };
            DialogService.Show<BookedDialog>("Бронирование", options);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            //TODO запрос на вытягивание квартир!
            //Appartments = await GreenHouseClient.;
            City = await GreenHouseClient.GetCityByIdAsync(Id, _cts.Token);
        }
    }
}
