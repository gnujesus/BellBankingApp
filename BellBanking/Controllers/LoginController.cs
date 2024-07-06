using BellBanking.Middleware;
using BellBankingApp.Core.Application.Helpers;
using BellBankingApp.Core.Application.DTOs.Account;
using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BellBankingApp.Core.Application.Enums;

namespace BellBanking.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [ServiceFilter(typeof(LoginAuthorize))]
        // GET: LoginController
        public IActionResult Index()
        {
            LoginViewModel login = new();
            return View(login);
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            AuthenticationResponse userVm = await _loginService.LoginAsync(loginVm);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>(userVm.Role, userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                loginVm.HasError = userVm.HasError;
                loginVm.Error = userVm.Error;
                return View(loginVm);
            }

        }
        public async Task<IActionResult> LogOut()
        {
            await _loginService.SignOutAsync();
            HttpContext.Session.Remove(Roles.Admin.ToString());
            HttpContext.Session.Remove(Roles.Customer.ToString());
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
    }
}
