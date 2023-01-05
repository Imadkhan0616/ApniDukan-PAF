using System.Security.Claims;
using ApniDukan.Common;
using ApniDukan.DatabaseIntegration;
using ApniDukan.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApniDukan.Controllers
{
    [AllowAnonymous]
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
        public async Task<IActionResult> SignIn(SignInViewModel signInModel)
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

            //TempData["Session"] = new SessionViewModel
            //{
            //    IsAuthenticated = true,
            //    Type = user.Type,
            //    UserName = $"{user.FirstName} {user.LastName}"
            //}
            //.ToJson();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{user.FirstName} + {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Type)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
                );

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties { IsPersistent = true } // configure remember option here
                );

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
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Model", ex.Message);
                return View(user);
            }

            return await SignIn(new SignInViewModel() { Email = user.Email, Password = user.Password });
        }

        public async Task<IActionResult> Logout()
        {
            //TempData["Session"] = new SessionViewModel
            //{
            //    IsAuthenticated = false
            //}
            //.ToJson();

            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}