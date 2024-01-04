using System.Runtime.Serialization;

namespace GreenHouse.Domain.Exeptions
{
    public class InvalidPasswordException : AuthorisationExeption
    {
        public InvalidPasswordException() : base("Invalid password") { }

        public InvalidPasswordException(string? message) : base(message)
        {
        }

        public InvalidPasswordException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
