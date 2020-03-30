using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DoCover.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DoCover.Controllers
{
    public class HomeController : AuthController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IOptionsSnapshot<DoOptions> options, IConfiguration configuration) : base(options)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogWarning(ThisUser.Name);
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
