using Microsoft.AspNetCore.Mvc;
using BellBankingApp.Core.Application.DTOs.Account;
using BellBankingApp.Core.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Core.Application.ViewModels.Beneficiary;
using BellBankingApp.Core.Domain.Entities;

namespace BellBankingApp.Web.Controllers
{

    public class TransactionController : Controller
    {

        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public TransactionController(IUserService userService, IProductService productService)
        {
            _userService = userService;
            _productService = productService;
        }


        public async Task<IActionResult> Index()
        {
            var beneficiaries = new List<BeneficiaryViewModel>
            {
                new BeneficiaryViewModel { Id = 1, UserId = "1", ProductId = 1 },
                new BeneficiaryViewModel { Id = 2, UserId = "2", ProductId = 2 },
                // Add more beneficiaries as needed
            };

            return View(beneficiaries);

        }

        [HttpPost]
        public async Task<IActionResult> Transfer(int selectedBeneficiaryId)
        {
            var beneficiary = await GetBeneficiaryById(selectedBeneficiaryId);
            if (beneficiary == null)
            {
                return NotFound();
            }

            var user = await _userService.GetById(beneficiary.UserId);
            var product = await _productService.GetById(beneficiary.ProductId.Value);

            ViewBag.User = user;
            ViewBag.Product = product;

            return View();
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

        public IActionResult AllTransactionsHistory()
        {
            return View();
        }

    }

}
