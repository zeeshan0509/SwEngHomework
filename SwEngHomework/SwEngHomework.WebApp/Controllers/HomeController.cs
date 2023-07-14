using Microsoft.AspNetCore.Mvc;
using SwEngHomework.WebApp.Models;
using System.Diagnostics;
using System.Globalization;

namespace SwEngHomework.WebApp.Controllers
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
            DateTime dt = DateTime.UtcNow;
            ViewBag.ControllerDateTime = dt;
            ViewBag.dateTimeInISO = dt.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz", CultureInfo.InvariantCulture);

            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.PrivacyText = "We take your privacy very seriously.";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}