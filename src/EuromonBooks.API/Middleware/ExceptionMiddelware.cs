using EuromonBooks.Abstractions.Exceptions;
using EuromonBooks.Domain.Abstractions.Exceptions;
using EuromonBooks.Domain.Abstractions.Models;
using EuromonBooks.Domain.Abstractions.Models.ExceptionErrors;
using Newtonsoft.Json;
using System.IO.Pipelines;
using System.Net;
using System.Text;

namespace EuromonBooks.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (ApiException aex)
            {
                await HandleExceptionAsync(httpContext, aex, (HttpStatusCode)aex.StatusCode);
            }
            catch (ArgumentException aex)
            {
                await HandleExceptionAsync(httpContext, aex, HttpStatusCode.BadRequest);
            }
            catch (ValidationException vex)
            {
                await HandleValidationExceptionAsync(httpContext, vex, HttpStatusCode.BadRequest);
            }
            catch (UnauthorizedAccessException uaex)
            {
                await HandleExceptionAsync(httpContext, uaex, HttpStatusCode.Unauthorized);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string type = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new ErrorResponse
            {
                Error = new ExceptionDetails()
                {
                    Code = context.Response.StatusCode,
                    Message = exception.Message,
                    Type = type
                }
            };

            await WriteResponseBody(context, exception, errorResponse);
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new ErrorResponse
            {
                Error = new ExceptionDetails()
                {
                    Code = context.Response.StatusCode,
                    Message = exception.Message,
                    Type = exception.ErrorType,
                    Errors = exception.Errors
                }
            };

            await WriteResponseBody(context, exception, errorResponse);
        }

        private async Task WriteResponseBody(HttpContext context, Exception exception, ErrorResponse error)
        {
            var logObj = new RequestLogger
            {
                EndTimestamp = DateTime.UtcNow
            };

            _logger.LogError(exception, JsonConvert.SerializeObject(logObj.LogFailure(context.TraceIdentifier)));

            var response = context.Response;
            var textToWrite = JsonConvert.SerializeObject(error);
            PipeWriter bodyWriter = response.BodyWriter;
            await bodyWriter.WriteAsync(Encoding.UTF8.GetBytes(textToWrite));
        }
    }
}