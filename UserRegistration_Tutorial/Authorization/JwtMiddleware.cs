using FirebaseAdmin.Auth;
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

        //public async Task Invoke(HttpContext context)
        //{
        //    var token = context.Request.Cookies["auth._token.local"]?.Split(" ").Last();

        //    //var userId = jwtUtils.ValidateToken(token);
        //    //if (userId != null)
        //    //{
        //    //    context.Items["User"] = userService.GetById(token);
        //    //}
        //    FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
        //      string uid = decodedToken.Uid;
        //    await _next(context);
        //}
    }
}
