namespace GreenHouse.Domain.Entities
{
    public class City : IEntity
    {
        private readonly Guid _id;
        private string _name;
        private string _imagePath;
        public List<Appartment> Appartments { get; set; }

        public City(Guid id, string name, string imagePath)
        {
            _id = id;
            _name = name;
            _imagePath = imagePath;
            Appartments = new List<Appartment>();
        }

        public City(string name, string imagePath) : this(Guid.NewGuid(), name, imagePath) { }

        public Guid Id { get => _id; init => _id = value; }
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Value cannot be null or whitespace", nameof(value));
                }
                _name = value;
            }
        }
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Value cannot be null or whitespace", nameof(value));
                }
                _imagePath = value;
            }
        }

    }
}
