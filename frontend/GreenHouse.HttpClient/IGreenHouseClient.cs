using GreenHouse.HttpModels.Requests;
using GreenHouse.HttpModels.Responses;

namespace GreenHouse.HttpApiClient
{
    public interface IGreenHouseClient
    {
        Task<IReadOnlyList<CityResponse>> GetAllCitiesAsync(CancellationToken cancellationToken);
        Task<CityResponse> GetCityByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task AddCity(CityRequest cityRequest, CancellationToken cancellationToken);
        Task DeleteCity(Guid Id, CancellationToken cancellationToken);
    }
}
