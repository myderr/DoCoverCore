using Microsoft.AspNetCore.Mvc;

namespace DoCover.Controllers
{
    public class UserController : Controller
    {
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