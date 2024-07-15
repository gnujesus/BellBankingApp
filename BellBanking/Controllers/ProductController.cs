using BellBankingApp.Core.Application.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.BellBankingApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View(new List<ProductViewModel>());
        }
        public IActionResult Create()
        {
            return View(new SaveProductViewModel());
        }
    }
}
