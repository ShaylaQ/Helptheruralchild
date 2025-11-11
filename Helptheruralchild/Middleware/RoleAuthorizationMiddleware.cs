using Microsoft.AspNetCore.Http;

namespace Helptheruralchild.Middleware
{
    public class RoleAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value;
            var role = context.Session.GetString("UserRole");

            if (path.Contains("AdminDashboard") && role != "Admin")
            {
                context.Response.Redirect("/Account/Login");
                return;
            }

            if (path.Contains("DriverDashboard") && role != "Driver")
            {
                context.Response.Redirect("/Account/Login");
                return;
            }

            if (path.Contains("DonorDashboard") && role != "Donor")
            {
                context.Response.Redirect("/Account/Login");
                return;
            }


            await _next(context);
        }
    }
}
