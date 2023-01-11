using ApniDukan.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ApniDukan.Common;
using Microsoft.AspNetCore.Authorization;

namespace ApniDukan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Shop()
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