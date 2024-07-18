using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Core.Application.DTOs.Account;
using BellBankingApp.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BellBankingApp.Core.Application.Enums;
using BellBankingApp.Core.Application.ViewModels.Transaction;

namespace WebApp.BellBankingApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductService _productService;
        private readonly AuthenticationResponse user;

        public PaymentController(ITransactionService transactionService, IHttpContextAccessor httpContextAccessor, IProductService productService)
        {
            _transactionService = transactionService;
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>(Roles.Customer.ToString());
        }
        // GET: PaymentController
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Express()
        {
            SaveTransactionViewModel transaction = new();
            transaction.OriginProduct = await _productService.GetProductTypebyUserId(user.Id, ProductType.SavingAccount);
            ViewBag.TransactionType = ProductType.SavingAccount;
            return View("Create", transaction);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Express(SaveTransactionViewModel express)
        {
            //ToDo
            return View("Index");
        }

        public async Task<IActionResult> CreditCard()
        {
            SaveTransactionViewModel transaction = new();
            transaction.OriginProduct = await _productService.GetProductTypebyUserId(user.Id, ProductType.SavingAccount);
            transaction.DestinationProduct = await _productService.GetProductTypebyUserId(user.Id, ProductType.CreditCard);
            ViewBag.TransactionType = ProductType.CreditCard;
            return View("Create", transaction);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreditCard(SaveTransactionViewModel creditCard)
        {
            //ToDo
            return View("Index");
        }

        public async Task<IActionResult> Loan()
        {
            SaveTransactionViewModel transaction = new();
            transaction.OriginProduct = await _productService.GetProductTypebyUserId(user.Id, ProductType.SavingAccount);
            transaction.DestinationProduct = await _productService.GetProductTypebyUserId(user.Id, ProductType.Loan);
            ViewBag.TransactionType = ProductType.Loan;
            return View("Create", transaction);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Loan(SaveTransactionViewModel Loan)
        {
            //ToDo
            return View("Index");
        }

        public async Task<IActionResult> Beneficiary()
        {
            SaveTransactionViewModel transaction = new();
            transaction.OriginProduct = await _productService.GetProductTypebyUserId(user.Id, ProductType.SavingAccount);
            transaction.DestinationProduct = await _productService.GetProductTypebyUserId(user.Id, ProductType.Loan);
            ViewBag.TransactionType = ProductType.Loan;
            return View("Create", transaction);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Beneficiary(SaveTransactionViewModel Loan)
        {
            //ToDo
            return View("Index");
        }
    }
}
