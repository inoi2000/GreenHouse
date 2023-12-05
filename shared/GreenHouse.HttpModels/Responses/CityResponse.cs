namespace GreenHouse.HttpModels.Responses
{
    public class CityResponse
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; }
        public int AppartmentCount { get; set; }

        public IReadOnlyList<AppartmentResponse>? Appartments { get; set; }

        public CityResponse() { }
    }
}
