using GreenHouse.HttpModels.DataTransferObjects;

namespace GreenHouse.HttpModels.Responses
{
    public class AppartmentResponse : AppartmentDto
    {
        public List<OrderDto>? Orders { get; set; }
        public Guid CityId { get; set; }
    }
}
