using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouse.Domain.Exeptions
{
    [Serializable]
    public class DomainExeption : Exception
    {
        public DomainExeption()
        {
        }

        public DomainExeption(string? message) : base(message)
        {
        }

        public DomainExeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DomainExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
