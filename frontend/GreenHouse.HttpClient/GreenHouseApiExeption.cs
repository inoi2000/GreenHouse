using GreenHouse.HttpModels.Responses;
using System.Net;
using System.Runtime.Serialization;

namespace GreenHouse.HttpApiClient
{
    [Serializable]
    public class GreenHouseApiExeption : Exception
    {
        private ErrorResponse? Error { get; }
        public HttpStatusCode StatusCode { get; }
        public ValidationProblemDetails Details { get; }

        public GreenHouseApiExeption(HttpStatusCode statusCode, ValidationProblemDetails details) : base(details.Title)
        {
            if (details is null) { throw new ArgumentNullException(nameof(details)); }

            StatusCode = statusCode;
            Details = details;
            Error = new ErrorResponse(details.Title!, statusCode);
        }

        public GreenHouseApiExeption(ErrorResponse error) : base(error.Message)
        {
            Error = error;
            StatusCode = error.HttpStatusCode;
            Details = new ValidationProblemDetails() { Title = error.Message, Status = (int)StatusCode };
        }

        public GreenHouseApiExeption(string? message) : base(message)
        {
        }

        public GreenHouseApiExeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected GreenHouseApiExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
