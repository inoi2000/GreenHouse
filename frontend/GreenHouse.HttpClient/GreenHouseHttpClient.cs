using GreenHouse.HttpModels.Responses;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GreenHouse.HttpApiClient
{
    public class GreenHouseHttpClient : IGreenHouseClient
    {
        private readonly string _host;
        private readonly HttpClient _httpClient;

        public GreenHouseHttpClient(string host = "http://greenhouse.ru/", HttpClient? httpClient = null)
        {
            if (host is null) { throw new ArgumentNullException(nameof(host)); }

            if (!Uri.TryCreate(host, UriKind.Absolute, out var hostUri))
            {
                throw new ArgumentException("The host adress should be a valif url", nameof(host));
            }
            _host = host;
            _httpClient = httpClient ?? new HttpClient();
            if (_httpClient.BaseAddress is null)
            {
                _httpClient.BaseAddress = hostUri;
            }
        }

        public async Task<IReadOnlyList<CityResponse>> GetAllCities(CancellationToken cancellationToken)
        {
            var citiesTask = _httpClient.GetFromJsonAsync<IReadOnlyList<CityResponse>>("cities/get_cities", cancellationToken);
            if (citiesTask is null)
            {
                throw new InvalidOperationException("The server returned null cities");
            }
            var cities = await citiesTask;
            if (cities is null)
            {
                throw new InvalidOperationException("The server returned null cities");
            }
            return cities;
        }
    }
}
