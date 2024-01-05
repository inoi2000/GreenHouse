using System.Runtime.Serialization;

namespace GreenHouse.Domain.Exeptions
{
    public class LoginAlreadyExistsExeption : DomainExeption
    {
        public LoginAlreadyExistsExeption() : base("Admin with this login already exists") { }

        public LoginAlreadyExistsExeption(string? message) : base(message)
        {
        }

        public LoginAlreadyExistsExeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected LoginAlreadyExistsExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
