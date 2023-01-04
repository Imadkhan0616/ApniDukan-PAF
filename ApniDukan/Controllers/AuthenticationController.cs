using ApniDukan.Common;
using ApniDukan.DatabaseIntegration;
using ApniDukan.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace ApniDukan.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ApniDukanContext _apniDukanContext;

        public AuthenticationController(ApniDukanContext apniDukanContext)
        {
            _apniDukanContext = apniDukanContext;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(SignInViewModel signInModel)
        {
            if (!ModelState.IsValid) return View(signInModel);

            User user = _apniDukanContext.User.FirstOrDefault(u => u.Email == signInModel.Email);

            if (user == null)
            {
                ModelState.AddModelError("Model", "Email does not exist.");
                return View(signInModel);
            }

            if (!BCrypt.Net.BCrypt.Verify(signInModel.Password, user.Password))
            {
                ModelState.AddModelError("Model", "Your password is incorrect.");
                return View(signInModel);
            }

            TempData["Session"] = new SessionViewModel
            {
                IsAuthenticated = true,
                Type = user.Type,
                UserName = $"{user.FirstName} {user.LastName}"
            }
            .ToJson();

            if (user.Type is "Customer")
                return RedirectToAction(actionName: "Shop", controllerName: "Home");

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (!ModelState.IsValid) return View(user);

            try
            {
                Customer customer = new Customer
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _apniDukanContext.User.Add(user);
                _apniDukanContext.Customer.Add(customer);

                await _apniDukanContext.CommitChangesAsync();

                TempData["Session"] = new SessionViewModel
                {
                    IsAuthenticated = true,
                    Type = user.Type,
                    UserName = $"{user.FirstName} {user.LastName}"
                }
                .ToJson();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Model", ex.Message);
                return View(user);
            }

            if (user.Type is "Customer")
                return RedirectToAction(actionName: "Shop", controllerName: "Home");

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        public IActionResult Logout()
        {
            TempData["Session"] = new SessionViewModel
            {
                IsAuthenticated = false
            }
            .ToJson();

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}