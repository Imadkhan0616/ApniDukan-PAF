using System.Security.Claims;
using ApniDukan.DatabaseIntegration;
using ApniDukan.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

            User user = _apniDukanContext.User.Include(r => r.Role).FirstOrDefault(u => u.Email == signInModel.Email);

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

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
                );

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties { IsPersistent = true } // configure remember me option here
                );

            if (user.Role.Name is "Customer")
                return RedirectToAction(actionName: "Shop", controllerName: "Home");

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        public IActionResult Register()
        {
            ViewData["RoleID"] = new SelectList(_apniDukanContext.Role, "RoleID", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (!ModelState.IsValid) return View(user);

            string rawPassword;
            try
            {
                Customer customer = new Customer
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };

                rawPassword = user.Password;
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

            return await SignIn(new SignInViewModel { Email = user.Email, Password = rawPassword });
        }

        public async Task<IActionResult> Logout()
        {
            // Clear the existing external cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}