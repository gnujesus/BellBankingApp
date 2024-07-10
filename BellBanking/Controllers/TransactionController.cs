using BellBankingApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Transfer()
        {
            return View(new SaveUserViewModel());
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
