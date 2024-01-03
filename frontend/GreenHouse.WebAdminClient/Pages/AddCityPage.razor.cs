using GreenHouse.HttpModels.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace GreenHouse.WebAdminClient.Pages
{
    public partial class AddCityPage
    {
        [Inject] private ISnackbar Snackbar { get; set; }
        List<IBrowserFile> files = new List<IBrowserFile>();
        private string Name { get; set; }
        private string Description { get; set; }
        private string ImagUri { get; set; }
        private decimal Price { get; set; }

        MudForm form { get; set; }

        private CancellationTokenSource _cts = new CancellationTokenSource();

        private async Task SaveProduct()
        {
            var city = new CityRequest() 
            { 

            };
            //await OnlineShopClient.AddProductAsync(Product, _cts.Token);
        }

        private void UploadFiles(IBrowserFile file)
        {
            files.Add(file);
            GreenHouseClient.UploadAppatrmentPhotos(files, _cts.Token);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}
