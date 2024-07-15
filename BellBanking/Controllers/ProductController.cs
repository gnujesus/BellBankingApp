using BellBankingApp.Core.Application.DTOs.User;
using BellBankingApp.Core.Application.Helpers;
using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Core.Application.Services;
using BellBankingApp.Core.Application.ViewModels.Product;
using BellBankingApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace WebApp.BellBankingApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: ProductController
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Index(string userId)
        {
            List<ProductViewModel> productList = await _productService.GetAllbyUserId(userId);
            return View(productList);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public IActionResult Create(string userId)
        {
            SaveProductViewModel saveProduct = new(){ UserId = userId };            
            return View(saveProduct);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveProductViewModel saveProduct)
        {
            if (!ModelState.IsValid)
            {
                return View(saveProduct);
            }

            SaveProductViewModel response = await _productService.Create(saveProduct);
            if (response.HasError)
            {
                saveProduct.HasError = response.HasError;
                saveProduct.Error = response.Error;
                return View(saveProduct);
            }

            return RedirectToRoute(new { controller = "User", action = "Index" });

        }

        // GET: ProductController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            SaveProductViewModel product = await _productService.GetById(id);
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SaveProductViewModel saveProductViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(saveProductViewModel);
            }

            await _productService.Update(saveProductViewModel, saveProductViewModel.Id);

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        // GET: ProductController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            SaveProductViewModel product = await _productService.GetById(id);
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _productService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
