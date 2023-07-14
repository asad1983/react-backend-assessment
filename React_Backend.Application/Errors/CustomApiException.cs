using System.Net;

namespace React_Backend.Application.Errors
{
    public class CustomApiException : Exception
    {
        public CustomApiException(string message, Exception ex, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message, ex)
        {
            StatusCode = statusCode;
        }

        public CustomApiException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public CustomApiException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }



        public HttpStatusCode StatusCode { get; }
    }
}
