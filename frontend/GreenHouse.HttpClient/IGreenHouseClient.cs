using GreenHouse.HttpModels.DataTransferObjects;
using GreenHouse.HttpModels.Requests;
using GreenHouse.HttpModels.Responses;

namespace GreenHouse.HttpApiClient
{
    public interface IGreenHouseClient
    {
        Task<IReadOnlyList<CityResponse>> GetAllCitiesAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<AppartmentResponse>> GetAllAppartmentsAsync(CancellationToken cancellationToken);
        Task<CityResponse> GetCityByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task AddCity(CityRequest cityRequest, CancellationToken cancellationToken);
        Task DeleteCity(Guid Id, CancellationToken cancellationToken);
        Task DeleteAppartment(Guid Id, CancellationToken cancellationToken);
        Task AddAppartment(AppartmentRequest appartmentRequest, CancellationToken cancellationToken);
        Task<IReadOnlyList<string>> UploadAppartmentImages(SaveFile file, CancellationToken cancellationToken);
        Task<string> UploadCityImage(FileData file, CancellationToken cancellationToken);

        Task<AuthorisationResponse> AuthorisationAsync(AuthorisationRequest request, CancellationToken token);
        void SetAuthorizationToken(string token);
        void DeleteAuthorizationToken();
        Task<AdminResponse> GetCurrentAdmin(CancellationToken token);
    }
}
