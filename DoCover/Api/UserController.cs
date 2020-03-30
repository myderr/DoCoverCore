using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DoCover.Enum;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using DoCover.Common;
using DoCover.Entitys;
using DoCover.Models;
using DoCover.Models.User;
using Microsoft.Extensions.Options;

namespace DoCover.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly DoOptions _options;

        public UserController(IOptionsSnapshot<DoOptions> options) : base(options)
        {
            _options = options.Value;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SignInAsync(ReqSignIn req)
        {
            try
            {
                var db = new DbContext(_options);
                var user = db.User.AsQueryable()
                    .Where(m => m.Name == req.Account)
                    .ToList()
                    .FirstOrDefault(m=> 
                        RsaForJava.DecryptByPrivateKey(m.Pwd, PrivateKey) ==
                        RsaForJava.DecryptByPrivateKey(req.Password, PrivateKey));
                if (user == null) return Ok(new Response(ErrorCode.E2_21105));
                ClaimsIdentity identity = new ClaimsIdentity("DoCover");
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserId.ToString()));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
                {
                    //过期时间：一天
                    ExpiresUtc = DateTime.Now.AddDays(1),
                    IsPersistent = false,
                    AllowRefresh = false
                });
                return Ok(new Response());
            }
            catch (Exception ex)
            {
                return Ok(new Response(ErrorCode.E1_15100,ex.Message));
            }
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SignOutAsync()
        {
            try
            {
                await HttpContext.SignOutAsync();
                return Ok(new Response());
            }
            catch (Exception ex)
            {
                return Ok(new Response(ErrorCode.E1_15100, ex.Message));
            }
        }
    }
}