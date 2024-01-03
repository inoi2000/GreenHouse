using GreenHouse.HttpModels.Requests;
using GreenHouse.HttpModels.Responses;
using Microsoft.AspNetCore.Components.Forms;

namespace GreenHouse.HttpApiClient
{
    public interface IGreenHouseClient
    {
        Task<IReadOnlyList<CityResponse>> GetAllCitiesAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<AppartmentResponse>> GetAllAppartmentsAsync(CancellationToken cancellationToken);
        Task<CityResponse> GetCityByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task AddCity(CityRequest cityRequest, CancellationToken cancellationToken);
        Task DeleteCity(Guid Id, CancellationToken cancellationToken);
        Task AddAppartment(AppartmentRequest appartmentRequest, CancellationToken cancellationToken);
}
