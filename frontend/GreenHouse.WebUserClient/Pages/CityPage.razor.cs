using GreenHouse.HttpModels.Responses;
using Microsoft.AspNetCore.Components;

namespace GreenHouse.WebUserClient.Pages
{
    public partial class CityPage
    {
        [Parameter] public Guid Id { get; set; }
        //private IReadOnlyList<AppartmentResponse>? Appartments { get; set; }
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

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            //TODO запрос на вытягивание квартир!
            //Appartments = await GreenHouseClient.;
            City = await GreenHouseClient.GetCityByIdAsync(Id, _cts.Token);
        }
    }
}
