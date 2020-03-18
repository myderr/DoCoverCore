// ReSharper disable NotAccessedField.Local
#pragma warning disable 169
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoCover.Common;
using DoCover.Entitys;
using DoCover.Enum;
using DoCover.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DoCover.Controllers
{
    public class InstallController : Controller
    {
        private readonly ILogger<InstallController> _logger;
        private readonly IOptionsSnapshot<DoOptions> _options;
        private readonly IWebHostEnvironment _evn;

        private static int _progress = 2;

        public InstallController(ILogger<InstallController> logger,IOptionsSnapshot<DoOptions> options, IWebHostEnvironment evn)
        {
            _logger = logger;
            _options = options;
            _evn = evn;
        }
        public IActionResult Index(int progress = 1)
        {
            ViewBag.Progress = progress;
            if (_progress >= progress)
                return View();
            else
                return RedirectToAction("Index", new { progress = _progress });
        }

        public IActionResult Setup()
        {
            if (_progress >= 2)
                return View();
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> LinkDb(int db,string ip, string port, string database, string userId, string pwd,string bef)
        {
            if (db <= 0 || db >= 4) return Json(new Response(ErrorCode.E2_21104));
            try
            {
                _options.Value.Conn = db switch
                {
                    1 => Global.GetMysqlConn(ip, port, database, userId, pwd),
                    2 => Global.GetSqlServerConn(ip, port, database, userId, pwd),
                    3 => Global.GetPgSqlConn(ip, port, database, userId, pwd),
                    _ => _options.Value.Conn
                };

                _options.Value.DbType = db;
                _options.Value.TablePrefix = bef;
                if (_evn.IsDevelopment())
                    _options.Value.SetAppSettingValue("C:\\Users\\myder\\Source\\Repos\\DoCover\\DoCover\\appsettings.json");
                else
                    _options.Value.SetAppSettingValue();

                var dbContext = new MysqlContext(_options);
                await dbContext.InitDatabase();
                _progress = 2;
                return Json(new Response(200,"创建数据库成功"));
            }
            catch (Exception ex)
            {
                return Json(new Response(ErrorCode.E1_15100) { message = ex.Message });
            }
        }

        public async Task<IActionResult> SetInfo(string title)
        {
            try
            {
                var dbContext = new MysqlContext(_options);
                List<Setting> sets = new List<Setting>()
                {
                    new Setting(EnumSet.WebsiteName,title)
                };
                await dbContext.Setting.AsUpdateable(sets)
                    .ExecuteCommandAsync();
                _progress = 3;
                return Json(new Response(200, "设置成功"));
            }
            catch (Exception ex)
            {
                return Json(new Response(ErrorCode.E1_15100) { message = ex.Message });
            }
        }
    }

}