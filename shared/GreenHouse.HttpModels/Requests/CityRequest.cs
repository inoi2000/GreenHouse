namespace GreenHouse.HttpModels.Requests
{
    public class CityRequest
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; }

        public CityRequest(Guid id, string name, string imagePath)
        {
            Id = id;
            Name = name;
            ImagePath = imagePath;
        }

        public CityRequest()
        {
            Id = Guid.NewGuid();
        }
    }
}
