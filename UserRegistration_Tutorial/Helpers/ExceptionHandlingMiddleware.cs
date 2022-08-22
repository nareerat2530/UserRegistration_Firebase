using FirebaseAdmin.Auth;
using System.Net;
using System.Text.Json;
using UserRegistration_Tutorial.Services;

namespace UserRegistration_Tutorial.Helpers
{
    public class ExceptionHandlingMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger  )
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
            catch (Exception error)
            {
               await HandleException(context, error);

               

            }
        }
       private static async Task HandleException(HttpContext context, Exception error)
        {

            var response = context.Response;
            response.ContentType = "application/json";

            var errorService = new Error
            {

                Success = false
            };



            switch (error)
            {
                case AppException e:
                   response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorService.Message = e.Message;
                    
                    
                    
                    break;
                case FirebaseAuthException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorService.Message = "Something went wrong!";
                    break;
                case KeyNotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorService.Message = e.Message;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorService.Message = "Something went wrong. Please try again!";
                    break;

            }

            var result = JsonSerializer.Serialize(errorService);
            await context.Response.WriteAsync(result);

        }

    }
}
