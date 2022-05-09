using Newtonsoft.Json;
using System.Net;
using System.Text.Json;
using UserRegistration_Tutorial.Services;

namespace UserRegistration_Tutorial.Helpers
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception error)
            {
               await HandleException(context, error);

               

            }
        }
       private async Task HandleException(HttpContext context, Exception error)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            var errorService = new ErrorService
            {
                Success = false
            };
           
            switch (error)
            {
                case AppException e:
                   response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorService.Message = e.Message;
                    break;

                case KeyNotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorService.Message = e.Message;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorService.Message = "Internal Server errors";
                    break;

            }

            _logger.LogError(error.Message);
            var result = System.Text.Json.JsonSerializer.Serialize(errorService);
            await context.Response.WriteAsync(result);
        }

    }
}
