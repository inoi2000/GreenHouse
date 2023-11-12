using GreenHouse.Domain.Entities;
using GreenHouse.Domain.Interfaces;
using GreenHouse.HttpModels.Requests;
using GreenHouse.HttpModels.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GreenHouse.WebApi.Controllers
{
    [ApiController]
    [Route("cities")]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));    
        }

        [HttpGet("get_cities")]
        public async Task<ActionResult<IReadOnlyList<CityResponse>>> GetCities(CancellationToken cancellationToken)
        {
            try
            {
                IReadOnlyList<City> cities = await _cityRepository.GetAll(cancellationToken);
                var result = new List<CityResponse>();
                foreach (City city in cities)
                {
                    result.Add(new CityResponse(city.Id, city.Name));
                }
                return Ok(result);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPost("add_city")]
        public async Task<IResult> AddCity([FromBody] CityRequest request, CancellationToken cancellationToken)
        {
            var city = new City(request.Name);
            await _cityRepository.Add(city, cancellationToken);
            return Results.Created($"/cities/{city.Id}", city);
        }

        [HttpPut("edit_city")]
        public async Task<IResult> EditCity([FromBody] CityRequest request, CancellationToken token)
        {
            try
            {
                var city = new City(request.Id, request.Name);
                await _cityRepository.Update(city, token);
                return Results.Ok();
            }
            catch (InvalidOperationException)
            {
                return Results.NotFound(request.Id);
            }

        }
    }
}
