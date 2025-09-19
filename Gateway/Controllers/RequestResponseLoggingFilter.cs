using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.IO;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    public class RequestResponseLoggingFilter : IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var request = context.HttpContext.Request;
            string body = string.Empty;
            if (request.ContentLength > 0 && request.Body.CanRead)
            {
                request.EnableBuffering();
                using (var reader = new StreamReader(request.Body, leaveOpen: true))
                {
                    body = await reader.ReadToEndAsync();
                    request.Body.Position = 0;
                }
            }
            Log.Information("Request: {Method} {Path} {Body}", request.Method, request.Path, body);
            Console.WriteLine($"TEST LOG: {request.Method} {request.Path} {body}");

            ActionExecutedContext resultContext = null;
            try
            {
                resultContext = await next();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception occurred during request: {Method} {Path}", request.Method, request.Path);
                Console.WriteLine($"TEST ERROR: {ex.Message}");
                throw;
            }

            var result = resultContext?.Result;
            if (resultContext?.Exception != null)
            {
                Log.Error(resultContext.Exception, "Exception occurred during request: {Method} {Path}", request.Method, request.Path);
                Console.WriteLine($"TEST ERROR: {resultContext.Exception.Message}");
            }
            Log.Information("Response: {StatusCode} {Result}", context.HttpContext.Response.StatusCode, result);
            Console.WriteLine($"TEST LOG: Response {context.HttpContext.Response.StatusCode} {result}");
        }
    }
}
