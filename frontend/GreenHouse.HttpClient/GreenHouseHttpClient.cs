using GreenHouse.HttpModels.Requests;
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

        public async Task<IReadOnlyList<CityResponse>> GetAllCitiesAsync(CancellationToken cancellationToken)
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

        public async Task<CityResponse> GetCityByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            var city = await _httpClient.GetFromJsonAsync<CityResponse>($"cities/get_city?Id={Id}", cancellationToken);
            if (city is null) { throw new InvalidOperationException("The server returned null city"); }
            return city;
        }

        public async Task AddCity(CityRequest cityRequest, CancellationToken cancellationToken)
        {
            using var response = await _httpClient.PostAsJsonAsync("cities/add_city", cityRequest, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCity(Guid Id, CancellationToken cancellationToken)
        {
            using var response = await _httpClient.DeleteAsync("cities/delete_city?Id={Id}", cancellationToken);
            response.EnsureSuccessStatusCode();
        }
    }
}
