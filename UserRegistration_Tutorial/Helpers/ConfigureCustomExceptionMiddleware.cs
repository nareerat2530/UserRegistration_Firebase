namespace UserRegistration_Tutorial.Helpers
{
    public static class ConfigureCustomExceptionMiddleware
    {
        public static void ConfigureCustomException(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
