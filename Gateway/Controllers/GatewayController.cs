using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GatewayController : BaseController
    {

        private readonly IHttpClientFactory _httpClientFactory;
        public GatewayController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("ping")]
        public async Task<IActionResult> Ping()
        {
            await Task.CompletedTask;
            return Ok("Gateway is running.");
        }
    }
}
