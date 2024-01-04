namespace GreenHouse.Domain.Entities
{
    public class Admin : IEntity
    {
        private readonly Guid _id;
        private string _login;
        private string _passwordHash;

        public Admin(Guid id, string login, string passwordHash)
        {
            _id = id;
            _login = login;
            _passwordHash = passwordHash;
        }

        public Admin(string login, string passwordHash)
        {
            _id = Guid.NewGuid();
            _login = login;
            _passwordHash = passwordHash;
        }

        public Guid Id { get => _id; init => _id = value; }
        public string Login
        {
            get => _login;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Value cannot be null or whitespace", nameof(value));
                }
                _login = value;
            }
        }

        public string PasswordHash
        {
            get => _passwordHash;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Value cannot be null or whitespace", nameof(value));
                }
                _passwordHash = value;
            }
        }
    }
}
