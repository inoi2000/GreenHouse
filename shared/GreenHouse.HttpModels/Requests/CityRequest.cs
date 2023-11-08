namespace GreenHouse.HttpModels.Requests
{
    public class CityRequest
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;

        public CityRequest(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
