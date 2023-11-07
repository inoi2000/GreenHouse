namespace GreenHouse.Domain.Entities
{
    public class ImageUri : IEntity
    {
        private readonly Guid _id;
        public Guid Id { get => _id; init => _id = value; }
        public string Uri { get; set; } = null!;
    }
}
