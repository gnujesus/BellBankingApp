using Azure;
using BellBanking.Middleware;
using BellBankingApp.Core.Application.DTOs.User;
using BellBankingApp.Core.Application.Enums;
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
    [ServiceFilter(typeof(LoginAuthorize))]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: ProductController
        public async Task<IActionResult> Index(string userId)
        {
            List<ProductViewModel> productList = await _productService.GetAllbyUserId(userId);

            if (string.IsNullOrEmpty(userId))
            {
                // Handle the case where userId is not found (e.g., user not logged in)
                return RedirectToAction("Login", "Account");
            }

            ViewBag.UserId = userId;
            return View(productList);

        }

        public async Task<IActionResult> ProductsHistory()
        {
            List<ProductViewModel> productList = await _productService.GetAll();
            return View(productList);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public IActionResult Create(string id)
        {
            SaveProductViewModel saveProduct = new(){ UserId = id, IsMainAccount = false };
            ViewBag.UserId = id;
            return View(saveProduct);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveProductViewModel saveProduct)
        {
            

            SaveProductViewModel response = await _productService.Create(saveProduct);
            if (response.HasError)
            {
                saveProduct.HasError = response.HasError;
                saveProduct.Error = response.Error;
                return View(saveProduct);
            }

            return RedirectToRoute(new { controller = "User", action = "Index" });

        }

        public IActionResult CreateMainAccount(string userId, double amount)
        {
            SaveProductViewModel newProduct = new()
            {
                Amount = amount,
                UserId = userId,
                IsMainAccount = true,
                Type = ProductType.SavingAccount.ToString(),
            };
            return View(newProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMainAccountPost(SaveProductViewModel product)
        {

            SaveProductViewModel response = await _productService.Create(product);
            if (response.HasError)
            {
                product.HasError = response.HasError;
                product.Error = response.Error;
                return View(product);
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
