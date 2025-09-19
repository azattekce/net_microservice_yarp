using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

     /*    [HttpPost]
        public async Task<IActionResult> CreateOrderDebug([FromBody] object order)
        {
            return await LogAndExecuteAsync(async () =>
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsync("http://localhost:5002/api/order", new StringContent(order.ToString(), System.Text.Encoding.UTF8, "application/json"));
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            });
        } */
    }
}
