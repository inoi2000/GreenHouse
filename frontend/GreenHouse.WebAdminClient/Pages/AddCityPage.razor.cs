using GreenHouse.HttpModels.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace GreenHouse.WebAdminClient.Pages
{
    public partial class AddCityPage
    {
        [Inject] private ISnackbar Snackbar { get; set; }

        List<string> imgUrls = new List<string>();
        List<FileData> fileData = new List<FileData>();


        private string Name { get; set; }
        private string Description { get; set; }
        private string ImagUri { get; set; }
        private decimal Price { get; set; }

        MudForm form { get; set; }
        

        private CancellationTokenSource _cts = new CancellationTokenSource();

        private async Task SaveToServer()
        {
            try
            {
                if (fileData.Count > 0)
                {
                    var payload = new SaveFile { Files = fileData };
                    var names = await GreenHouseClient.UploadAppartmentImages(payload, _cts.Token);
                    //TODO переделать функционал с возможностью возвращения одного объекта
                }
            } catch (Exception ex)
            {
                Snackbar.Add("Ошибка", Severity.Error);
            }
        }

        private async Task UploadFiles(IBrowserFile imgFile)
        {
            var buffers = new byte[imgFile.Size];
            await imgFile.OpenReadStream().ReadAsync(buffers);
            string imageType = imgFile.ContentType;
            string imgUrl = $"data:{imageType};base64,{Convert.ToBase64String(buffers)}";
            imgUrls.Add(imgUrl);
            fileData.Add(new FileData
            {
                Data = buffers,
                FileType = imageType,
                Size = imgFile.Size
            });
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }        
    }
}
