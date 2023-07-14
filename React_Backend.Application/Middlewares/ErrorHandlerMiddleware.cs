using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using React_Backend.Application.Errors;
using System.Net;
using System.Text.Json;

namespace React_Backend.Application.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly IHostEnvironment _hostEnvironment;
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger, IHostEnvironment hostEnvironment)
        {
            this._next = next;
            this._logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var httpResponse = context.Response;
                httpResponse.ContentType = "application/json";
                var response = _hostEnvironment.IsDevelopment() ?
                    new ApiException((int)HttpStatusCode.InternalServerError, error.Message, error.StackTrace.ToString())
                    : new ApiException((int)HttpStatusCode.InternalServerError, error.Message);
                switch (error)
                {
                    case DbUpdateException e:
                        httpResponse.StatusCode = (int)HttpStatusCode.Conflict;
                        response = new ApiException((int)HttpStatusCode.Conflict, "Conflict");
                        break;

                    case KeyNotFoundException e:
                        // not found error
                        httpResponse.StatusCode = (int)HttpStatusCode.NotFound;
                        response = new ApiException((int)HttpStatusCode.NotFound, "Not found");
                        break;

                    case CustomApiException e:
                        // not found error
                        httpResponse.StatusCode = (int)e.StatusCode;
                        response = new ApiException((int)e.StatusCode, e.Message);
                        break;
                    default:
                        // unhandled error
                        httpResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                _logger.LogError(error, $"Global error middleware has catched the error");
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var result = JsonSerializer.Serialize(response, options);
                await httpResponse.WriteAsync(result);
            }
        }
    }
}
