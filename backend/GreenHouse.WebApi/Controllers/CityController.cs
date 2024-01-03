using GreenHouse.Domain.Entities;
using GreenHouse.Domain.Interfaces;
using GreenHouse.HttpModels.DataTransferObjects;
using GreenHouse.HttpModels.Requests;
using GreenHouse.HttpModels.Responses;
using GreenHouse.WebApi.Services.Extentions;
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
                    result.Add(new CityResponse() { Id=city.Id, Name= city.Name, ImagePath=city.ImagePath, AppartmentCount = city.Appartments.Count});
                }
                return Ok(result);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpGet("get_city")]
        public async Task<ActionResult<CityResponse>> GetCity([FromQuery] Guid Id, CancellationToken token)
        {
            try
            {
                var city = await _cityRepository.GetById(Id, token);
                var appartmentResponses = new List<AppartmentResponse>();

                foreach(var appartment in city.Appartments)
                {
                    appartmentResponses.Add(appartment.CreateResponce());
                };

                var result = new CityResponse()
                {
                    Id = city.Id,
                    Name = city.Name,
                    ImagePath = city.ImagePath,
                    AppartmentCount = city.Appartments.Count,
                    Appartments = appartmentResponses
                };
                
                return Ok(result);
            }
            catch (InvalidOperationException)
            {
                return NotFound(Id);
            }
        }

        [HttpPost("add_city")]
        public async Task<IResult> AddCity([FromBody] CityRequest request, CancellationToken cancellationToken)
        {
            var city = new City(request.Name, request.ImagePath);
            await _cityRepository.Add(city, cancellationToken);
            return Results.Ok();
        }

        [HttpDelete("delete_city")]
        public async Task<IResult> DeleteCity([FromQuery] Guid Id, CancellationToken cancellationToken)
        {
            await _cityRepository.Delete(Id, cancellationToken);
            return Results.Ok();
        }

        [HttpPut("edit_city")]
        public async Task<IResult> EditCity([FromBody] CityRequest request, CancellationToken token)
        {
            try
            {
                var city = new City(request.Id, request.Name, request.ImagePath);
                await _cityRepository.Update(city, token);
                return Results.Ok();
            }
            catch (InvalidOperationException)
            {
                return Results.NotFound(request.Id);
            }

        }

        [HttpPost("upload_file")]
        public async Task<ActionResult<string>> UploadCityImage(FileData file, CancellationToken cancellationToken)
        {
            try
            {
                string fileExtenstion = file.FileType.ToLower().Contains("png") ? "png" : "jpg";
                string tempGuid = Guid.NewGuid().ToString();
                string fileName = $@"wwwroot\Images\cities\{tempGuid}.{fileExtenstion}";
                using (var fileStream = System.IO.File.Create(fileName))
                {
                    await fileStream.WriteAsync(file.Data);
                }
                return $@"{tempGuid}.{fileExtenstion}";
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
