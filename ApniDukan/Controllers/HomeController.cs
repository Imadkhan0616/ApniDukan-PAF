using ApniDukan.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ApniDukan.Common;
using Microsoft.AspNetCore.Authorization;
using ApniDukan.DatabaseIntegration;
using Microsoft.EntityFrameworkCore;

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
            //if (TempData["Session"] != null)
            //{
            //    SessionStorage.Session = TempData["Session"].ToString();
            //    ViewData["Session"] = SessionStorage.Session.FromJsonToObject<SessionViewModel>();
            //}
            //else if (SessionStorage.Session != null)
            //    ViewData["Session"] = SessionStorage.Session.FromJsonToObject<SessionViewModel>();
            //else
            //    ViewData["Session"] = new SessionViewModel();

            var _apniDukanContext = _context.Product.Include(p => p.Category);

            return View(await _apniDukanContext.ToListAsync());
        }

        public IActionResult Cart()
        {
            //if (TempData["Session"] != null)
            //{
            //    SessionStorage.Session = TempData["Session"].ToString();
            //    ViewData["Session"] = SessionStorage.Session.FromJsonToObject<SessionViewModel>();
            //}
            //else if (SessionStorage.Session != null)
            //    ViewData["Session"] = SessionStorage.Session.FromJsonToObject<SessionViewModel>();
            //else
            //    ViewData["Session"] = new SessionViewModel();

            return View();
        }
        public IActionResult OurTeam()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
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