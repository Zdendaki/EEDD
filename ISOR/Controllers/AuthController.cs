using ISOR.Database;
using Microsoft.AspNetCore.Mvc;

namespace ISOR.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _database;

        public AuthController(ILogger<HomeController> logger, Context database)
        {
            _logger = logger;
            _database = database;
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
