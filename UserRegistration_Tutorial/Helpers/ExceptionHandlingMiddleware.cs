﻿using System.Net;
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
            
            var errorService = new ErrorService
            {
                Success = false
            };
            var result = JsonSerializer.Serialize(errorService);
            await context.Response.WriteAsync(result);

            switch (error)
            {
                case AppException e:
                   response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorService.Message = e.Message;
                    //_ = result;
                    
                    
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

            
            
        }

    }
}
