using GreenHouse.HttpModels.DataTransferObjects;
using GreenHouse.HttpModels.Requests;
using GreenHouse.HttpModels.Responses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace GreenHouse.WebAdminClient.Pages
{
    public partial class AddCityPage
    {
        private IReadOnlyList<CityResponse>? Cities { get; set; }
        private string Name { get; set; }

        private List<FileData> fileData = new List<FileData>();
        private IBrowserFile? _file;

        private const string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full z-10";
        private string _dragClass = DefaultDragClass;
        private string? _fileName;

        [Inject] private ISnackbar Snackbar { get; set; }

        bool success;
        MudTextField<string> pwField1;
        MudForm form;

        private async Task Reset()
        {
            await ClearDrag();
            await form.ResetAsync();
        }

        private async Task Save()
        {
            await form.Validate();
            if (!success)
            {
                Snackbar.Add("Введите корректные данные", Severity.Error);
                return;
            }
            if (_file == null)
            {
                Snackbar.Add("Добавте фото", Severity.Error);
                return;
            }

            try
            {
                var payload = await UploadFiles(_file);
                var imgUri = await GreenHouseClient.UploadCityImage(payload, _cts.Token);

                var cityRequest = new CityRequest()
                {
                    Name = Name,
                    ImagePath = imgUri
                };
                await GreenHouseClient.AddCity(cityRequest, _cts.Token);
                Snackbar.Add("Город успешно добавлен", Severity.Success);
            }
            catch (System.IO.IOException ex)
            {
                Snackbar.Add("Ошибка, максимальный размер файла не может быть более 512000 байт", Severity.Error);
            }
            catch (Exception ex)
            {
                Snackbar.Add("Ошибка", Severity.Error);
            }
        }

        private async Task ClearDrag()
        {
            _fileName = null;
            _file = null;
            ClearDragClass();
            await Task.Delay(100);
        }

        private void OnInputFileChanged(InputFileChangeEventArgs e)
        {
            ClearDragClass();
            var file = e.GetMultipleFiles();
            _fileName = file[0].Name;
            _file = file[0];
        }

        private async Task<FileData> UploadFiles(IBrowserFile imgFile)
        {
            var buffers = new byte[imgFile.Size];
            await imgFile.OpenReadStream().ReadAsync(buffers);
            string imageType = imgFile.ContentType;
            return new FileData
            {
                Data = buffers,
                FileType = imageType,
                Size = imgFile.Size
            };
        }

        private void SetDragClass()
        => _dragClass = $"{DefaultDragClass} mud-border-primary";

        private void ClearDragClass()
            => _dragClass = DefaultDragClass;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Cities = await GreenHouseClient.GetAllCitiesAsync(_cts.Token);
        }
    }
}
