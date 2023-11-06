using System.Collections.Concurrent;

namespace GreenHouse.Domain.Entities
{
    public class Appartment : IEntity
    {
        private readonly Guid _id;
        private readonly City _city;
        private readonly string _location;
        private int _numberOfGuests;
        private int _numberOfRooms;
        private int _numberOfSlippingPlaces;
        private double _square;
        private decimal _bail;
        private decimal _price;
        public ConcurrentBag<DateTime> BookedDays { get; init; }
        public ConcurrentBag<Uri> Photos { get; init; }
        public ConcurrentBag<Rule> Rules { get; init; }
        public ConcurrentBag<Convenience> Conveniences { get; init; }

        public Appartment()
        {
            Id = Guid.NewGuid();
            BookedDays = new ConcurrentBag<DateTime>();
            Photos = new ConcurrentBag<Uri>();
            Rules = new ConcurrentBag<Rule>();
            Conveniences = new ConcurrentBag<Convenience>();
        }

        public Guid Id { get => _id; init => _id = value; }
        public City City { get => _city; init => _city = value; }
        public string Location { get => _location; init => _location = value; }
        public int NumberOfGuests
        {
            get => _numberOfGuests;
            set
            {
                if (_numberOfGuests <= 0)
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
                if (_numberOfRooms <= 0)
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
                if (_numberOfSlippingPlaces <= 0)
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
                if (_square <= 0)
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
                if (_bail <= 0)
                {
                    throw new ArgumentException("The value cannot be less or equal to zero", nameof(value));
                }
                _bail = value;
            }
        }
        public decimal Price
        {
            get => _price;
            set
            {
                if (_price <= 0)
                {
                    throw new ArgumentException("The value cannot be less or equal to zero", nameof(value));
                }
                _price = value;
            }
        }
    }
}
