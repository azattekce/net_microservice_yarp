using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        // Ortak controller fonksiyonları burada olabilir.
        // Loglama veya filter eklenmeyecek!
    }

}
