using DoCover.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoCover.Controllers
{
    /// <summary>
    /// 需要登录的窗体
    /// </summary>
    [Authorize]
    public class BaseController : Controller
    {
        /// <summary>
        /// 登录人ID
        /// </summary>
        public string UserId => User.Identity?.Name;
    }
}