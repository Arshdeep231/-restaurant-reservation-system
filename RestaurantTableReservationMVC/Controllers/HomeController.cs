using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantTableReservationMVC.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace RestaurantTableReservationMVC.Controllers
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
            if (User.Identity?.IsAuthenticated == true)
            {
                var role = User.FindFirstValue(ClaimTypes.Role);
                if (role == "admin")
                    return RedirectToAction("Index", "Admin");
                return RedirectToAction("Index", "Customer");
            }
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
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
