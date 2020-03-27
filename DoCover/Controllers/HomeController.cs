using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DoCover.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DoCover.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DoOptions _options;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IOptionsSnapshot<DoOptions> options, IConfiguration configuration)
        {
            _logger = logger;
            _options = options.Value;
            _configuration = configuration;
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
