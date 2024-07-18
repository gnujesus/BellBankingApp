using BellBankingApp.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BellBankingApp.Core.Application.Helpers;
using BellBankingApp.Core.Application.DTOs.Account;
using BellBankingApp.Core.Application.Enums;
using BellBanking.Middleware;
using System.Security.Claims;

namespace BellBanking.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(LoginAuthorize))]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionService _transactionService;

        public HomeController(ITransactionService transactionService, IHttpContextAccessor httpContextAccessor, IUserService userService, IProductService productService, IBeneficiaryService beneficiaryService)
        {
            _userService = userService;
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index()
        {
            var user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>(Roles.Customer.ToString()) ??
                       _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>(Roles.Admin.ToString());

            
            var allTransactions = await _transactionService.GetAll();
            var userTransactions = allTransactions
                .Where(t => t.UserId == user.Id)
                .OrderByDescending(t => t.DateCreated)
                .Take(4)
                .ToList();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var products = await _productService.GetAllbyUserId(userId);
            ViewBag.UserProducts = products.Take(4); // Take only the first 4 products
            ViewBag.UserTransactions = userTransactions;

            return View();
        }
    }
}
