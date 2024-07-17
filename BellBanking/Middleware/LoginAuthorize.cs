using BellBanking.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BellBanking.Middleware
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private readonly ValidateUserSession _validateUserSession;

        public LoginAuthorize(ValidateUserSession validateUserSession)
        {
            _validateUserSession = validateUserSession;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controllerName = context.RouteData.Values["controller"]?.ToString();

            // Exclude Login controller to avoid redirection loop
            if (controllerName != "Login")
            {
                if (!_validateUserSession.HasSession())
                {
                    context.Result = new RedirectToActionResult("Index", "Login", null);
                    return;
                }
            }

            await next();
        }
    }
}
