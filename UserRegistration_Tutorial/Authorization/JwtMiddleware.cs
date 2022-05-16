using UserRegistration_Tutorial.Interfaces;

namespace UserRegistration_Tutorial.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context,IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();   
            var userId = jwtUtils.ValidateToken(token);
            if(userId != null)
            {
                context.Items["User"] = userService.GetById(token);
            }
            await _next(context);
        }
    }
}
