 using GreenHouse.HttpModels.Responses;
using GreenHouse.WebUserClient.Services;
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

        private PaginationService<AppartmentResponse> _paginationService = new PaginationService<AppartmentResponse>();
        const int APPARTMENT_COUNT_ON_PAGE = 5;

        private int _currentPage = 1;
        private int _countOfPages = 1;

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
            if (City != null) { _countOfPages = City.Appartments!.Count / APPARTMENT_COUNT_ON_PAGE + 1; }
        }

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

        private string GetNumberOfGuests(AppartmentResponse appartmentDto)
        {
            switch (appartmentDto.NumberOfGuests)
            {
                case 1:
                    return $"{appartmentDto.NumberOfGuests} гость";
                case 2:
                case 3:
                case 4:
                    return $"{appartmentDto.NumberOfGuests} гостя";                  
                default: 
                    return $"{appartmentDto.NumberOfGuests} гостей";
            }
        }

        private string GetNumberOfRooms(AppartmentResponse appartmentDto)
        {
            switch (appartmentDto.NumberOfRooms)
            {
                case 1:
                    return $"{appartmentDto.NumberOfRooms} комната";
                case 2:
                case 3:
                case 4:
                    return $"{appartmentDto.NumberOfRooms} комнаты";
                default:
                    return $"{appartmentDto.NumberOfRooms} комнат";
            }
        }

        private string GetNumberOfSlippingPlaces(AppartmentResponse appartmentDto)
        {
            switch (appartmentDto.NumberOfSlippingPlaces)
            {
                case 1:
                    return $"{appartmentDto.NumberOfSlippingPlaces} спальное место";
                case 2:
                case 3:
                case 4:
                    return $"{appartmentDto.NumberOfSlippingPlaces} спальных места";
                default:
                    return $"{appartmentDto.NumberOfSlippingPlaces} спальных мест";
            }
        }
    }
}
