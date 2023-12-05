namespace GreenHouse.Domain.Entities
{
    public class Appartment : IEntity
    {
        private readonly Guid _id;
        private Guid _cityId;
        private string _location;
        private int _numberOfGuests;
        private int _numberOfRooms;
        private int _numberOfSlippingPlaces;
        private double _square;
        private decimal _bail;
        private decimal _price;
        public List<string> Photos { get; set; }

        public RulesList Rules { get; init; }        
        public List<Convenience> Conveniences { get; set; }
        public List<Order> Orders { get; init; }

        public Guid Id { get => _id; init => _id = value; }
        public Guid CityId { get => _cityId; set => _cityId = value; }
        public string Location { get => _location; set => _location = value; }
        public int NumberOfGuests
        {
            get => _numberOfGuests;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("The value cannot be less or equal to zero", nameof(value));
                }
                _numberOfGuests = value;
            }
        }
        public int NumberOfRooms
        {
            get => _numberOfRooms;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("The value cannot be less or equal to zero", nameof(value));
                }
                _numberOfRooms = value;
            }
        }
        public int NumberOfSlippingPlaces
        {
            get => _numberOfSlippingPlaces;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("The value cannot be less or equal to zero", nameof(value));
                }
                _numberOfSlippingPlaces = value;
            }
        }
        public double Square
        {
            get => _square;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("The value cannot be less or equal to zero", nameof(value));
                }
                _square = value;
            }
        }
        public decimal Bail
        {
            get => _bail;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("The value cannot be less to zero", nameof(value));
                }
                _bail = value;
            }
        }
        public decimal Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("The value cannot be less or equal to zero", nameof(value));
                }
                _price = value;
            }
        }
    }
}
