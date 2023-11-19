namespace GreenHouse.HttpModels.Responses
{
    public class CityResponse
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;

        public CityResponse(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public CityResponse(string name) : this(Guid.NewGuid(), name) { }

        public CityResponse() { }
    }
}
