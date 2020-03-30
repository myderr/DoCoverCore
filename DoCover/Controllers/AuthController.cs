using DoCover.Entitys;
using DoCover.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace DoCover.Controllers
{
    /// <summary>
    /// 需要登录的窗体，里面的资源为登录才能获取
    /// </summary>
    [Authorize]
    public class AuthController : BaseController
    {
        private readonly DoOptions _options;

        public AuthController(IOptionsSnapshot<DoOptions> options) : base(options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// 登录人ID
        /// </summary>
        public string UserId => User.Identity?.Name;

        /// <summary>
        /// 登录人
        /// </summary>
        public User ThisUser => new DbContext(_options).User.GetValueByKeyId(UserId);
    }
}