namespace GreenHouse.Domain.Entities
{
    public class Rule : IEntity
    {
        private readonly Guid _id;
        public Guid Id { get => _id; init => _id = value; }
        public bool IsChildrenAllowed { get; set; }
        public bool IsPetsAllowed { get; set; }
        public bool IsSmokingAllowed { get; set; }
        public bool IsPartyAllowed { get; set; }
    }
}
