namespace GreenHouse.Domain.Entities
{
    public class City : IEntity
    {
        private readonly Guid _id;
        private string _name;
        public List<Appartment> Appartments { get; set; }

        public City(Guid id, string name)
        {
            _id = id;
            _name = name;
            Appartments = new List<Appartment>();
        }

        public City(string name) : this(Guid.NewGuid(), name) { }

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
    }
}
