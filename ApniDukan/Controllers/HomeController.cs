using ApniDukan.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ApniDukan.Common;
using ApniDukan.DatabaseIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ApniDukan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApniDukanContext _context;

        public HomeController(ApniDukanContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Shop()
        {
            IIncludableQueryable<Product, Category> products = _context.Product.Include(p => p.Category);
            return View(await products.ToListAsync());
        }

        public IActionResult Cart()
        {
            return View(SessionStorage.CartProducts[User.GetEmailAddress()] ?? new List<Product>());
        }
        public IActionResult OurTeam()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View(SessionStorage.CartProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}