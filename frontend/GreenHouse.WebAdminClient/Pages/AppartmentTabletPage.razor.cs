using System.Net.Http;
using System.Text.RegularExpressions;
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
        private string searchString1 { get; set; } = String.Empty;
        private AppartmentResponse selectedItem1 = null;
        private IReadOnlyList<AppartmentResponse>? Appartments { get; set; }
        private HashSet<AppartmentResponse> selectedItems = new HashSet<AppartmentResponse>();

        private IEnumerable<AppartmentResponse> Elements = new List<AppartmentResponse>();

        private async Task RemoveApartment(AppartmentResponse appartment)
        {

        }
        
        protected override async Task OnInitializedAsync()
        {
            Appartments = await GreenHouseClient.GetAllAppartmentsAsync(_cts.Token);
            _loading = false;
        }
    }
}
