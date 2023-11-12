namespace GreenHouse.Domain.Entities
{
    public class RulesList : IEntity
    {
        private readonly Guid _id;
        public Guid Id { get => _id; init => _id = value; }
        public bool IsChildrenAllowed { get; set; }
        public bool IsPetsAllowed { get; set; }
        public bool IsSmokingAllowed { get; set; }
        public bool IsPartyAllowed { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is RulesList rules)
            {
                if (IsChildrenAllowed == rules.IsChildrenAllowed &&
                    IsPetsAllowed == rules.IsPetsAllowed &&
                    IsSmokingAllowed == rules.IsSmokingAllowed &&
                    IsPartyAllowed == rules.IsPartyAllowed)
                {
                    return true;
                } else { return false; }
            }
            else { return false; }
        }
    }
}
