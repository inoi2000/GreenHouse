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

        private List<FileData> fileData = new List<FileData>();
        private List<IBrowserFile> browserFiles = new List<IBrowserFile>();

        private const string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full z-10";
        private string _dragClass = DefaultDragClass;
        private readonly List<string> _fileNames = new();

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
            if (browserFiles.Count <= 0)
            {
                Snackbar.Add("Добавте 1 или более фотографий", Severity.Error);
                return;
            }

            try
            {
                foreach (var file in browserFiles)
                {
                    await UploadFiles(file);
                }

                var payload = new SaveFile { Files = fileData };
                var names = await GreenHouseClient.UploadAppartmentImages(payload, _cts.Token);
                
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
                    Photos = new List<string>(names),
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
            }
            catch(System.IO.IOException ex)
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
            _fileNames.Clear();
            browserFiles.Clear();
            ClearDragClass();
            await Task.Delay(100);
        }

        private void OnInputFileChanged(InputFileChangeEventArgs e)
        {
            ClearDragClass();
            var files = e.GetMultipleFiles();
            foreach (var file in files)
            {
                _fileNames.Add(file.Name);
               browserFiles.Add(file);
            }
        }

        private async Task UploadFiles(IBrowserFile imgFile)
        {
            var buffers = new byte[imgFile.Size];
            await imgFile.OpenReadStream().ReadAsync(buffers);
            string imageType = imgFile.ContentType;
            fileData.Add(new FileData
            {
                Data = buffers,
                FileType = imageType,
                Size = imgFile.Size
            });
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
