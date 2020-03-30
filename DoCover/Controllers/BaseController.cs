using DoCover.Entitys;
using DoCover.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace DoCover.Controllers
{
    /// <summary>
    /// 主体窗体，有全局资源就放这儿
    /// </summary>
    public class BaseController : Controller
    {
        private readonly DoOptions _options;

        public BaseController(IOptionsSnapshot<DoOptions> options)
        {
            _options = options.Value;
            WebsiteName ??= new DbContext(_options).Setting.GetValueByKeyId(EnumSet.WebsiteName).Value;
            PublicKey ??= new DbContext(_options).Setting.GetValueByKeyId(EnumSet.PublicKey).Value;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.WebsiteName = WebsiteName;
            ViewBag.PublicKey = PublicKey;
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// 站点名称
        /// </summary>
        public static string WebsiteName { get; private set; }

        /// <summary>
        /// 公钥
        /// </summary>
        public static string PublicKey { get; private set; }
    }
}