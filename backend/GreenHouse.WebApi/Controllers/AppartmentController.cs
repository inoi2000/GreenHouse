using GreenHouse.Domain.Entities;
using GreenHouse.Domain.Interfaces;
using GreenHouse.HttpModels.Requests;
using GreenHouse.HttpModels.Responses;
using GreenHouse.WebApi.Services.Extentions;
using Microsoft.AspNetCore.Mvc;

namespace GreenHouse.WebApi.Controllers
{
    [ApiController]
    [Route("appartments")]
    public class AppartmentController : ControllerBase
    {
        private readonly IAppartmentRepository _appartmentRepository;
        public AppartmentController(IAppartmentRepository appartmentRepository)
        {
            _appartmentRepository = appartmentRepository ?? throw new ArgumentNullException(nameof(appartmentRepository));    
        }

        [HttpGet("get_appartments")]
        public async Task<ActionResult<IReadOnlyList<AppartmentResponse>>> GetAppartments([FromQuery] Guid cityId, CancellationToken cancellationToken)
        {
            try
            {
                IReadOnlyList<Appartment> appartments = await _appartmentRepository.GetByCityId(cityId, cancellationToken);
                var result = new List<AppartmentResponse>();
                foreach (Appartment appartment in appartments)
                {
                    var temp = appartment.CreateResponce();
                    result.Add(temp);
                }
                return Ok(result);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpGet("get_appartment")]
        public async Task<ActionResult<AppartmentResponse>> GetAppartment([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var appartment = await _appartmentRepository.GetById(id, cancellationToken);
                var res = appartment.CreateResponce();
                return Ok(res);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPost("add_appartment")]
        public async Task<IResult> AddAppartment([FromBody] AppartmentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var appartment = request.CreateAppartment();

                await _appartmentRepository.Add(appartment, cancellationToken);
                return Results.Created($"/appartments/{appartment.Id}", appartment);
            }
            catch(Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [HttpPut("edit_appartment")]
        public async Task<IResult> EditCity([FromBody] AppartmentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var appartment = request.CreateAppartment();
                await _appartmentRepository.Update(appartment, cancellationToken);
                return Results.Ok();
            }
            catch (InvalidOperationException)
            {
                return Results.NotFound(request.Id);
            }
        }
    }
}
