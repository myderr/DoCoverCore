// ReSharper disable NotAccessedField.Local
#pragma warning disable 169
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly DoOptions _options;
        private readonly IWebHostEnvironment _evn;

        public InstallController(ILogger<InstallController> logger,IOptionsSnapshot<DoOptions> options, IWebHostEnvironment evn)
        {
            _logger = logger;
            _options = options.Value;
            _evn = evn;
        }
        public IActionResult Index(int progress = 1)
        {
            ViewBag.Progress = progress;
            if (Api.InstallController._progress > 1)//大于第一步，有公钥了
            {
                try
                {
                    var dbContext = new DbContext(_options);
                    string publicKey = dbContext.Setting.GetValueByKeyId(EnumSet.PublicKey).Value;
                    ViewBag.PublicKey = publicKey;
                }
                catch { }
            }

            if (Api.InstallController._progress >= progress)
                return View();
            return RedirectToAction("Index", new { progress = Api.InstallController._progress });
        }
    }

}