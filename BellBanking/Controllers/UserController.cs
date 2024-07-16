using BellBanking.Middleware;
using BellBankingApp.Core.Application.DTOs.Account;
using BellBankingApp.Core.Application.DTOs.User;
using BellBankingApp.Core.Application.Enums;
using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.BellBankingApp.Controllers
{
    [ServiceFilter(typeof(LoginAuthorize))]
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
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userService.GetById(id);
            return View(user);
        }

        // GET: UserController/Create
        public IActionResult Create()
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

            saveUserViewModel.IsActive = true;

            CreateUserResponse response = await _userService.CreateUser(saveUserViewModel);
            if (response.HasError)
            {
                saveUserViewModel.HasError = response.HasError;
                saveUserViewModel.Error = response.Error;
                return View(saveUserViewModel);
            }

            if (saveUserViewModel.Rol == Roles.Customer)
            {
                return RedirectToRoute(new { controller = "Product", action = "Create", userId=saveUserViewModel.Id });
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userService.GetById(id);
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SaveUserViewModel saveUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(saveUserViewModel);
            }

            UpdateUserResponse response = await _userService.UpdateUser(saveUserViewModel);
            if (response.HasError)
            {
                saveUserViewModel.HasError = response.HasError;
                saveUserViewModel.Error = response.Error;
                return View(saveUserViewModel);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        // GET: UserController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetById(id);
            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(string id)
        {
            await _userService.DeleteUser(id);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}
