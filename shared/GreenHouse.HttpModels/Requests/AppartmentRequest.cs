using GreenHouse.HttpModels.DataTransferObjects;

namespace GreenHouse.HttpModels.Requests
{
    public class AppartmentRequest : AppartmentDto
    {
        public Guid CityId { get; set; }
    }
}
