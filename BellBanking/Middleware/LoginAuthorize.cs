using BellBanking.Controllers;
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
            if (_validateUserSession.HasSession())
            {
                var controller = (LoginController)context.Controller;
                context.Result = controller.RedirectToAction("index", "login");
            }
            else
            {
                await next();
            }
        }
    }
}
