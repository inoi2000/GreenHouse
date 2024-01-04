using System.Runtime.Serialization;

namespace GreenHouse.Domain.Exeptions
{
    public class AuthorisationExeption : DomainExeption
    {
        public AuthorisationExeption() : base("Incorrect login or password") { }

        public AuthorisationExeption(string? message) : base(message)
        {
        }

        public AuthorisationExeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AuthorisationExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
