using ItemStore.WebApi.Exceptions;
using ItemStore.WebApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace ItemStore.WebApi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                ErrorModel error = await GetExceptionResponseAsync(ex);

                await HandleExceptionAsync(context, error);

                return;
            }
        }

        private Task<ErrorModel> GetExceptionResponseAsync(Exception exception)
        {
            int statusCode;

            switch (exception)
            {
                case ItemNotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            string? message = exception.Message;

            return Task.FromResult(new ErrorModel()
            {
                Status = statusCode,
                Message = message,
            });
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, ErrorModel error)
        {
            _logger.Log(LogLevel.Error, $"----------------------------------------");
            _logger.Log(LogLevel.Error, "Status Code: {status}", error.Status);
            _logger.Log(LogLevel.Error, "Error: {message}", error.Message);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = error.Status;

            await httpContext.Response.WriteAsJsonAsync<ErrorModel>(error);
        }
    }
}
