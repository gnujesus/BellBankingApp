using Microsoft.AspNetCore.Mvc;
using BellBankingApp.Core.Application.DTOs.Account;
using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Core.Application.ViewModels.Beneficiary;
using BellBankingApp.Core.Application.Services;
using BellBankingApp.Core.Application.Enums;
using BellBankingApp.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace BellBankingApp.Web.Controllers
{

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

            var allBeneficiaries = await _beneficiaryService.GetAll();
            var userBeneficiaries = allBeneficiaries
                .Where(b => b.UserId == user.Id)
                .ToList();

            return View(userBeneficiaries);
        }

        public async Task<IActionResult> Transfer(int selectedBeneficiaryId)
        {
            var beneficiary = await _beneficiaryService.GetById(selectedBeneficiaryId);
            if (beneficiary == null)
            {
                return NotFound();
            }

            var product = await _productService.GetById(beneficiary.ProductId.Value);
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
