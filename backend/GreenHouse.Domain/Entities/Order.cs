namespace GreenHouse.Domain.Entities
{
    public class Order : IEntity
    {
        private readonly Guid _id;
        private DateTime _dateTimeEntry;
        private DateTime _dateTimeExit;
        private string _wishes;

        public Guid Id { get => _id; init => _id = value; }
        public DateTime DateTimeEntry
        {
            get => _dateTimeEntry;
            set
            {
                if (value < DateTime.Now)
                {
                    throw new ArgumentException("The value cannot be less than the current time", nameof(value));
                }
                _dateTimeEntry = value;
            }
        }
        public DateTime DateTimeExit
        {
            get => _dateTimeExit;
            set
            {
                if (value < DateTime.Now)
                {
                    throw new ArgumentException("The value cannot be less than the current time", nameof(value));
                }
                _dateTimeExit = value;
            }
        }
        public string Wishes
        {
            get => _wishes;
            set
            {
                _wishes = value;
            }
        }
        public Account Account { get; private set; }
        public Appartment Appartment { get; private set; }
    }
}
