using BellBanking.Middleware;
using BellBankingApp.Core.Application.DTOs.Account;
using BellBankingApp.Core.Application.Enums;
using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Core.Application.Services;
using BellBankingApp.Core.Application.ViewModels.Beneficiary;
using BellBankingApp.Core.Application.ViewModels.Product;
using BellBankingApp.Core.Application.ViewModels.User;
using BellBankingApp.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApp.BellBankingApp.Controllers
{
    [ServiceFilter(typeof(LoginAuthorize))]
    public class BeneficiaryController : Controller
    {
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BeneficiaryController(IBeneficiaryService beneficiaryService, IProductService productService, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _beneficiaryService = beneficiaryService;
            _productService = productService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        // GET: BeneficiaryController
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

            ViewBag.Products = products;

            return View(userViewModels);

        }

        // GET: BeneficiaryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BeneficiaryController/Create
        /*
        public IActionResult Create()
        {
            SaveBeneficiaryViewModel beneficiary = new();
            return View(beneficiary);
        }

        // POST: BeneficiaryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveBeneficiaryViewModel beneficiaryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(beneficiaryViewModel);
            }
            if (!await _productService.ExistAccountNumber(beneficiaryViewModel.AccountNumber)) 
            {
                beneficiaryViewModel.HasError = true;
                beneficiaryViewModel.Error = "Account does not exist.";
                return View(beneficiaryViewModel);
            }
            SaveBeneficiaryViewModel response = await _beneficiaryService.Create(beneficiaryViewModel);
            if (response.HasError)
            {
                beneficiaryViewModel.HasError = response.HasError;
                beneficiaryViewModel.Error = response.Error;
                return View(beneficiaryViewModel);
            }

            return View("Index");
        }
        */


        [HttpPost]
        public async Task<IActionResult> CreateBeneficiary(string productNumber)
        {
            var products = await _productService.GetAll();
            ProductViewModel product = new();
            SaveBeneficiaryViewModel result = new();

            foreach(var p in products)
            {
                
                if(p.AccountNumber == productNumber)
                {
                    var beneficiary = new SaveBeneficiaryViewModel
                    {
                        ProductId = p.Id,
                        AccountNumber = productNumber,
                        UserId = p.UserId
                    };

                    result = await _beneficiaryService.Create(beneficiary);
                }

                
            }

            if (result.HasError)
            {
                return Json(new { success = false, message = result.Error });
            }

            return Json(new { success = true });
        }

        // GET: BeneficiaryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BeneficiaryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BeneficiaryController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            SaveBeneficiaryViewModel beneficiary = await _beneficiaryService.GetById(id);
            return View(beneficiary);
        }

        // POST: BeneficiaryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _beneficiaryService.Delete(id);
            return View("Index");
        }
    }
}
