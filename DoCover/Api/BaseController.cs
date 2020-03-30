using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using DoCover.Entitys;
using DoCover.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DoCover.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly DoOptions _options;

        public static string PrivateKey { get; private set; }

        public BaseController(IOptionsSnapshot<DoOptions> options)
        {
            _options = options.Value;
            PrivateKey ??= new DbContext(_options).Setting.GetValueByKeyId(EnumSet.PrivateKey).Value;
        }
    }
}