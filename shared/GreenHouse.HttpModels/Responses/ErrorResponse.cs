using System.Net;

namespace GreenHouse.HttpModels.Responses
{
    public record class ErrorResponse
    {
        public ErrorResponse(string message, HttpStatusCode httpStatusCode)
        {
            Message = message;
            HttpStatusCode = httpStatusCode;
        }

        public string Message { get; }
        public HttpStatusCode HttpStatusCode { get; }
    }
}
