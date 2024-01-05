using System.Runtime.Serialization;

namespace GreenHouse.Domain.Exeptions
{
    public class AdminNotFoundExeption : AuthorisationExeption
    {
        public AdminNotFoundExeption() : base("Admin with given login not found") { }

        public AdminNotFoundExeption(string? message) : base(message)
        {
        }

        public AdminNotFoundExeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AdminNotFoundExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
