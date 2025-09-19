using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            var client = _httpClientFactory.CreateClient();

            // Docker network içinden catalog-api servisine erişim
            var products = await client.GetFromJsonAsync<List<dynamic>>("http://localhost:5001/api/Catalog");
            var firstProduct = products?.FirstOrDefault();

            var result = new
            {
                OrderId = Guid.NewGuid(),
                Product = firstProduct,
                Status = "Created"
            };

            return Ok(result);
        }
    }
}
