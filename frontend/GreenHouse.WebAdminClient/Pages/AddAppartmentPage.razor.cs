using System.Text.RegularExpressions;
using GreenHouse.HttpModels.DataTransferObjects;
using GreenHouse.HttpModels.Requests;
using GreenHouse.HttpModels.Responses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace GreenHouse.WebAdminClient.Pages
{
    public partial class AddAppartmentPage
    {
        private IReadOnlyList<CityResponse>? Cities { get; set; }

        private string SelectedCity { get; set; }
        private string Location { get; set; }
        private int NumberOfGuests { get; set; } = 1;
        private int NumberOfRooms { get; set; } = 1;
        private int NumberOfSlippingPlaces { get; set; } = 1;
        private double Square { get; set; } = 1;
        private decimal Bail { get; set; } = 1;
        private decimal Price { get; set; } = 1;

        private bool IsChildrenAllowed { get; set; }
        private bool IsPetsAllowed { get; set; }
        private bool IsSmokingAllowed { get; set; }
        private bool IsPartyAllowed { get; set; }


        private List<string> Photos { get; set; } = new List<string>();
        private string PhotoUri { get; set; } = string.Empty;



        [Inject] private ISnackbar Snackbar { get; set; }
        private CityRequest model = new();

        bool success;
        string[] errors = { };
        MudTextField<string> pwField1;
        MudForm form;

        private void AddPhoto(string photo)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(photo, UriKind.Absolute, out uriResult);
            if (result) 
            { 
                Photos.Add(photo);
                PhotoUri = string.Empty;
            } 
            else { Snackbar.Add("Неверный URI адрес", Severity.Error); }
        }

        private void RemovePhoto()
        {
            if(Photos.Count != 0) { Photos.RemoveAt(Photos.Count - 1); }
            else { Snackbar.Add("Вы не добавили ни одного URI", Severity.Error); }
        }

        private async Task Reset()
        {
            Photos.Clear();
            PhotoUri = string.Empty;
            await form.ResetAsync();            
        }

        private async Task Save()
        {
            await form.Validate();
            if (success) { 
                var appartmentRequest = new AppartmentRequest()
                {
                    CityId = Guid.Parse(SelectedCity),
                    Location = Location,
                    NumberOfGuests = NumberOfGuests,
                    NumberOfRooms = NumberOfRooms,
                    NumberOfSlippingPlaces = NumberOfSlippingPlaces,
                    Square = Square,
                    Bail = Bail,
                    Price = Price,
                    Photos = Photos,
                    Rules = new RulesListDto()
                    {
                        IsChildrenAllowed = IsChildrenAllowed,
                        IsPartyAllowed = IsPartyAllowed,
                        IsPetsAllowed = IsPetsAllowed,
                        IsSmokingAllowed = IsSmokingAllowed
                    }
                };
                await GreenHouseClient.AddAppartment(appartmentRequest, _cts.Token);
                Snackbar.Add("Квартира успешно добавлена", Severity.Success);
            } else
            {
                Snackbar.Add("Введите корректные данные", Severity.Error);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Cities = await GreenHouseClient.GetAllCitiesAsync(_cts.Token);
        }
    }
}
