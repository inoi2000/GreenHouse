using System.Net.Http;
using System.Text.RegularExpressions;
using GreenHouse.HttpModels.DataTransferObjects;
using GreenHouse.HttpModels.Requests;
using GreenHouse.HttpModels.Responses;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace GreenHouse.WebAdminClient.Pages
{
    public partial class AppartmentTabletPage
    {
        [Inject] private ISnackbar Snackbar { get; set; }

        private bool _loading = true;
        private AppartmentResponse selectedItem = null;
        private string searchString1 { get; set; } = String.Empty;

        private IReadOnlyList<CityResponse>? Cities { get; set; }

        private string selecterCity;
        public string SelectedCity
        {
            get { return selecterCity; }
            set { selecterCity = value; }
        }

        private IReadOnlyList<AppartmentResponse>? Appartments { get; set; }
        private HashSet<AppartmentResponse> selectedItems = new HashSet<AppartmentResponse>();

        private IEnumerable<AppartmentResponse> Elements = new List<AppartmentResponse>();

        private async Task EditApartment(AppartmentResponse appartment)
        {

        }

        private async Task RemoveApartment(Guid id)
        {
            await GreenHouseClient.DeleteAppartment(id, _cts.Token);
            await OnInitializedAsync();
        }

        private void ShowImgInfo(AppartmentResponse appartment)
        {
            if(selectedItem != appartment) 
            {
                selectedItem = appartment;
            } else
            {
                selectedItem = null!;
            }
        }

        private bool FilterFunc(AppartmentResponse element)
        {
            if (SelectedCity is null) return true;
            if (element.CityId == Guid.Parse(SelectedCity)) return true;
            else return false;
        }

        protected override async Task OnInitializedAsync()
        {
            Cities = await GreenHouseClient.GetAllCitiesAsync(_cts.Token);
            Appartments = await GreenHouseClient.GetAllAppartmentsAsync(_cts.Token);
            _loading = false;
        }
    }
}
