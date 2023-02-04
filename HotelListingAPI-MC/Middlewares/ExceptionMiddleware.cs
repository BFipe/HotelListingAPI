using HotelListingAPI_MC.Exceptions;
using HotelListingAPI_MC.Models.User;
using Newtonsoft.Json;
using System.Net;

namespace HotelListingAPI_MC.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
            }
            catch (Exception ex)
			{
                _logger.LogError(ex, $"Something went wrong while processing in {context.Request.Path}, User ({context.User?.Identity?.Name})");
                await HandleExceptionAsync(context, ex);
			}
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            var errorDetails = new ErrorDetails()
            {
                ErrorType = "Failure",
                ErrorMessage= ex.Message,
            };

            switch (ex)
            {
                case NotFoundException notFoundException:
                    status = HttpStatusCode.NotFound;
                    errorDetails.ErrorType = "NotFound";
                    break;

                default:
                    break;
            }

            string responce = JsonConvert.SerializeObject(errorDetails);
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(responce);
        }
    }

    public class ErrorDetails
    {
        public string ErrorType { get; set; }

        public string ErrorMessage { get; set; }
    }
}
