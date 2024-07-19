using Microsoft.AspNetCore.Mvc;
using BellBankingApp.Core.Application.DTOs.Account;
using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Core.Application.ViewModels.Beneficiary;
using BellBankingApp.Core.Application.Services;
using BellBankingApp.Core.Application.Enums;
using BellBankingApp.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;
using BellBanking.Middleware;
using BellBankingApp.Core.Application.ViewModels.User;
using System.Security.Claims;

namespace BellBankingApp.Web.Controllers
{
    [ServiceFilter(typeof(LoginAuthorize))]
    public class TransactionController : Controller
    {

        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionService _transactionService;
        private readonly IBeneficiaryService _beneficiaryService;


        public TransactionController(ITransactionService transactionService, IHttpContextAccessor httpContextAccessor, IUserService userService, IProductService productService, IBeneficiaryService beneficiaryService)
        {
            _userService = userService;
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;
            _transactionService = transactionService;
            _beneficiaryService = beneficiaryService;
        }

        public async Task<IActionResult> Index()
        {
            var user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>(Roles.Customer.ToString()) ??
                       _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>(Roles.Admin.ToString());

            if (user == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var userId = user.Id;

            var allBeneficiaries = await _beneficiaryService.GetAll();
            var userBeneficiaries = allBeneficiaries
                .Where(b => b.UserId == user.Id)
                .ToList();
            var products = await _productService.GetAllbyUserId(userId);

            var userViewModels = new List<UserViewModel>();

            foreach (var beneficiary in userBeneficiaries)
            {
                // Get the product associated with this beneficiary
                var product = await _productService.GetById(beneficiary.ProductId.Value);

                if (product != null)
                {
                    // Get the user associated with this product
                    var u = await _userService.GetById(product.UserId);

                    if (u != null)
                    {
                        userViewModels.Add(u);
                    }
                }
            }

            ViewBag.UserProducts = products;
            ViewBag.UserBeneficiaries = userBeneficiaries;

            return View(userBeneficiaries); 
        }

        public async Task<IActionResult> Transfer(int id)
        {

            var beneficiary = await _beneficiaryService.GetById(id);
            if (beneficiary == null)
            {
                return View();
            }

            //Quite .Value para que no cause error antes estaba var product = await _productService.GetById(beneficiary.ProductId.Value);
            var product = await _productService.GetById(beneficiary.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            var user = await _userService.GetById(product.UserId);
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.AccountNumber = product.AccountNumber;

            return View(user);
        }

        public async Task<IActionResult> Index2()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                // Handle the case where userId is not found (e.g., user not logged in)
                return RedirectToAction("Login", "Account");
            }

            var products = await _productService.GetAllbyUserId(userId);

            return View(products);
        }



        private async Task<BeneficiaryViewModel> GetBeneficiaryById(int id)
        {
            // Implement your logic to get the beneficiary by id
            // This is just a placeholder
            return new BeneficiaryViewModel { Id = id, UserId = "1", ProductId = 1 };
        }

        public IActionResult UserTransactionsHistory()
        {
            return View();
        }

        public async Task<IActionResult> AllTransactionsHistory()
        {
            var allTransactions = await _transactionService.GetAll();
            var sortedTransactions = allTransactions.OrderByDescending(t => t.DateCreated).ToList();
            return View(sortedTransactions);
        }


    }

}
