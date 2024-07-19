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
        private readonly IUserService _userService;
        private readonly AuthenticationResponse user;

        public PaymentController(ITransactionService transactionService, IHttpContextAccessor httpContextAccessor, IProductService productService, IUserService userService)
        {
            _transactionService = transactionService;
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
            _userService = userService;
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
            transaction.DestinationProduct = new();
            transaction.OriginProductList = await _productService.GetProductTypebyUserId(user.Id, ProductType.SavingAccount);
            ViewBag.TransactionType = ProductType.SavingAccount;
            return View("Create", transaction);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Express(SaveTransactionViewModel transaction)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TransactionType = ProductType.SavingAccount;
                return View("Create", transaction);
            }

            transaction.OriginProduct = await _productService.GetVMById(transaction.OriginProductId);
            transaction.DestinationProduct = await _productService.GetProductByAccount(transaction.DestinationAccount);

            SaveTransactionViewModel response = await _transactionService.ValidateExpressPayment(transaction);

            if (response.HasError)
            {
                transaction.HasError = response.HasError;
                transaction.Error = response.Error;
                return View("Create", transaction);
            }

            return PartialView(transaction);
        }

        public async Task<IActionResult> CreditCard()
        {
            SaveTransactionViewModel transaction = new();
            transaction.OriginProductList = await _productService.GetProductTypebyUserId(user.Id, ProductType.SavingAccount);
            transaction.DestinationProductList = await _productService.GetProductTypebyUserId(user.Id, ProductType.CreditCard);
            ViewBag.TransactionType = ProductType.CreditCard;
            return View("Create", transaction);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreditCard(SaveTransactionViewModel transaction)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TransactionType = ProductType.CreditCard;
                return View("Create", transaction);
            }

            SaveTransactionViewModel response = await _transactionService.CreditCardPayment(transaction);

            if (response.HasError)
            {
                transaction.HasError = response.HasError;
                transaction.Error = response.Error;
                return View("Create", transaction);
            }

            return View("Index");
        }

        public async Task<IActionResult> Loan()
        {
            SaveTransactionViewModel transaction = new();
            transaction.OriginProductList = await _productService.GetProductTypebyUserId(user.Id, ProductType.SavingAccount);
            transaction.DestinationProductList = await _productService.GetProductTypebyUserId(user.Id, ProductType.Loan);
            ViewBag.TransactionType = ProductType.Loan;
            return View("Create", transaction);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Loan(SaveTransactionViewModel transaction)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TransactionType = ProductType.Loan;
                return View("Create", transaction);
            }

            SaveTransactionViewModel response = await _transactionService.LoanPayment(transaction);

            if (response.HasError)
            {
                transaction.HasError = response.HasError;
                transaction.Error = response.Error;
                return View("Create", transaction);
            }

            return View("Index");
        }

        public async Task<IActionResult> Beneficiary()
        {
            SaveTransactionViewModel transaction = new();
            transaction.OriginProductList = await _productService.GetProductTypebyUserId(user.Id, ProductType.SavingAccount);
            transaction.DestinationProductList = await _productService.GetProductTypebyUserId(user.Id, ProductType.Loan);
            ViewBag.TransactionType = "Beneficiary";
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
