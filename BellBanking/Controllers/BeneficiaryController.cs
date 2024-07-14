using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Core.Application.ViewModels.Beneficiary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApp.BellBankingApp.Controllers
{
    public class BeneficiaryController : Controller
    {
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IProductService _productService;

        public BeneficiaryController(IBeneficiaryService beneficiaryService, IProductService productService)
        {
            _beneficiaryService = beneficiaryService;
            _productService = productService;
        }
        // GET: BeneficiaryController
        public async Task<IActionResult> Index()
        {
            var beneficiariesList = await _beneficiaryService.GetAll();
            return View(beneficiariesList);
        }

        // GET: BeneficiaryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BeneficiaryController/Create
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
