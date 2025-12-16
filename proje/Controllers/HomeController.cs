using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using proje.Models;

namespace proje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult AnaSayfa()
        {
            return View();
        }
        
        public IActionResult Yonlendir()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("GirisliSayfa", "Home");
            }

            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
