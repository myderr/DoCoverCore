using DoCover.Entitys;
using DoCover.Filter;
using DoCover.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DoCover.Controllers
{
    /// <summary>
    /// 需要登录的窗体
    /// </summary>
    [Authorize]
    public class BaseController : Controller
    {
        public readonly ILogger<HomeController> _logger;
        public readonly IConfiguration _configuration;
        public readonly DoOptions _options;

        public BaseController(ILogger<HomeController> logger, IOptionsSnapshot<DoOptions> options, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _options = options.Value;
        }

        protected BaseController()
        {
            
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var db = new DbContext(_options);
            ViewBag.WebsiteName = db.Setting.GetValueByKeyId(EnumSet.WebsiteName);
            ViewBag.PublicKey = db.Setting.GetValueByKeyId(EnumSet.PublicKey);
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// 登录人ID
        /// </summary>
        public string UserId => User.Identity?.Name;
    }
}