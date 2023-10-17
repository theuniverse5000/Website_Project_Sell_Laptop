namespace AdminApp
{
    public class CookieCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Cookies.ContainsKey("account"))
            {
                context.Response.Redirect("/Login/Login");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
