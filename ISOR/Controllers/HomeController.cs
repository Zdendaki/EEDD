using ISOR.Database;
using ISOR.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ISOR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _database;

        public HomeController(ILogger<HomeController> logger, Context database)
        {
            _logger = logger;
            _database = database;
        }

        public IActionResult Index()
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