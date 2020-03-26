using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoCover.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        /// <summary>
        /// 登录人ID
        /// </summary>
        public string UserId => User.Identity?.Name;
    }
}