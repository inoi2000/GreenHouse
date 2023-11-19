using GreenHouse.HttpModels.Responses;

namespace GreenHouse.HttpApiClient
{
    public interface IGreenHouseClient
    {
        Task <IReadOnlyList<CityResponse>> GetAllCities(CancellationToken cancellationToken);
    }
}
