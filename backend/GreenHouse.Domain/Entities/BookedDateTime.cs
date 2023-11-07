namespace GreenHouse.Domain.Entities
{
    public class BookedDateTime : IEntity
    {
        private readonly Guid _id;
        public Guid Id { get => _id; init => _id = value; }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get => _dateTime;
            set
            {
                if (_dateTime < DateTime.Now)
                {
                    throw new ArgumentException("The value cannot be less than the current time", nameof(value));
                }
                _dateTime = value;
            }
        }
    }
}
