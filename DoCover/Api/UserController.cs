using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DoCover.Enum;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using DoCover.Common;
using DoCover.Models.User;

namespace DoCover.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
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
                ClaimsIdentity identity = new ClaimsIdentity("Forms");
                identity.AddClaim(new Claim(ClaimTypes.Name, req.Account));
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