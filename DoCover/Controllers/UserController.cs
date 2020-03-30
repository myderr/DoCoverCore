using DoCover.Entitys;
using DoCover.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DoCover.Controllers
{
    public class UserController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DoOptions _options;
        private readonly IConfiguration _configuration;

        public UserController(ILogger<HomeController> logger, IOptionsSnapshot<DoOptions> options, IConfiguration configuration) : base(options)
        {
            _logger = logger;
            _options = options.Value;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string backurl = "")
        {
            ViewBag.ReturnUrl = string.IsNullOrEmpty(backurl) ? Url.Action("Index","Home") : backurl;
            return View();
        }
    }
}