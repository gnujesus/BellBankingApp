using BellBankingApp.Core.Application.DTOs.Account;
using BellBankingApp.Core.Application.DTOs.User;
using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.BellBankingApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: UserController
        public async Task<IActionResult> Index()
        {
            List<UserViewModel> userList = await _userService.GetAll();
            return View(userList);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            SaveUserViewModel saveUser = new();
            return View(saveUser);
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveUserViewModel saveUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(saveUserViewModel);
            }

            CreateUserResponse response = await _userService.CreateUser(saveUserViewModel);
            if (response.HasError)
            {
                saveUserViewModel.HasError = response.HasError;
                saveUserViewModel.Error = response.Error;
                return View(saveUserViewModel);
            }

            if (!saveUserViewModel.IsAdmin)
            {
                return RedirectToRoute(new { controller = "Product", action = "Create" });
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
