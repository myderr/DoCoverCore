using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DoCover.Common;
using DoCover.Entitys;
using DoCover.Enum;
using DoCover.Models;
using DoCover.Models.Install;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace DoCover.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InstallController : ControllerBase
    {
        private readonly IWebHostEnvironment _evn;
        private readonly DoOptions _options;

        /// <summary>
        /// 安装步骤
        /// </summary>
        private static int _progress = 1;

        public InstallController(IOptionsSnapshot<DoOptions> options, IWebHostEnvironment evn)
        {
            _evn = evn;
            _options = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDbAsync(ReqCreateDb req)
        {
            if (req.DbType <= 0 || req.DbType >= 4) return Ok(new Response(ErrorCode.E2_21104));
            try
            {
                _options.Conn = req.DbType switch
                {
                    1 => Global.GetMysqlConn(req.Host, req.Port,req.Database, req.UserId, req.Password),
                    2 => Global.GetSqlServerConn(req.Host, req.Port, req.Database, req.UserId, req.Password),
                    3 => Global.GetPgSqlConn(req.Host, req.Port, req.Database, req.UserId, req.Password),
                    _ => _options.Conn
                };

                _options.DbType = req.DbType;
                _options.TablePrefix = req.TablePrefix;
                if (_evn.IsDevelopment())
                    _options.SetAppSettingValue(Directory.GetCurrentDirectory() + "\\appsettings.json");
                else
                    _options.SetAppSettingValue();

                var dbContext = new DbContext(_options);
                await dbContext.InitDatabase();
                _progress = 2;
                return Ok(new Response(200, "创建数据库成功"));
            }
            catch (Exception ex)
            {
                return Ok(new Response(ErrorCode.E1_15100) { message = ex.Message });
            }
        }

        /// <summary>
        /// 设置网站、站长信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<IActionResult> SetInfoAsync(ReqSetInfo req)
        {
            try
            {
                var dbContext = new DbContext(_options);
                List<Setting> sets = new List<Setting>()
                {
                    new Setting(EnumSet.WebsiteName,req.WebsiteName),
                    new Setting(EnumSet.WebsiteAdminEmail,req.AdminEmail)
                };
                await dbContext.Setting.AsUpdateable(sets)
                    .ExecuteCommandAsync();
                await dbContext.User.AsUpdateable(new User() { UserId = 1, Name = req.AdminName, Pwd = req.AdminPassword, Type = 0 })
                    .ExecuteCommandAsync();
                _progress = 3;
                return Ok(new Response(200, "设置成功"));
            }
            catch (Exception ex)
            {
                return Ok(new Response(ErrorCode.E1_15100, ex.Message));
            }
        }
    }
}