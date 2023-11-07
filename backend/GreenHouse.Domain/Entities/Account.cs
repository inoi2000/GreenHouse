using System.ComponentModel.DataAnnotations;

namespace GreenHouse.Domain.Entities
{
    public class Account : IEntity
    {
        private readonly Guid _id;
        private string _name;
        private string _email;
        public string _phoneNumber;
        public List<Order> Orders { get; set; }

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
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Value cannot be null or whitespace", nameof(value));
                }
                if (new EmailAddressAttribute().IsValid(value))
                {
                    throw new ArgumentException("Value is not a valid email address", nameof(value));
                }
                _email = value;
            }
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Value cannot be null or whitespace", nameof(value));
                }
                if(new PhoneAttribute().IsValid(value))
                {
                    throw new ArgumentException("Value is not a valid phone number", nameof(value));
                }
                _phoneNumber = value;
            }
        }

    }
}
