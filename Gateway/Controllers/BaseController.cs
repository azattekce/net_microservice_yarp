using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected async Task<IActionResult> LogAndExecuteAsync(Func<Task<IActionResult>> action)
        {
            // Request log
            Log.Information("Request: {Method} {Path} {Body}",
                Request.Method,
                Request.Path,
                await new StreamReader(Request.Body).ReadToEndAsync());

            var result = await action();

            // Response log
            Log.Information("Response: {StatusCode} {Result}",
                Response.StatusCode,
                result);

            return result;
        }
    }
}
